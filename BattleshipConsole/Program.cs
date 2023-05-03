using Pages;
using MainGameController;
using MainLogger;
using Helpers;
public class Program
{
    private static readonly Page page = new();
    private static GameController Game = new();
    private static Logger<Program> Logger = new();

    public static void Main(string[] args)
    {
        Logger.Config(false);

        BattleshipStart();
        PreparationPhase();
        BattlePhase();
        BattleshipEnd();
    }

    private static void BattleshipStart()
    {
        //display home menu
        do
        {
            Console.Clear();
            Console.WriteLine(page.Home());
        } while ((int)Console.ReadKey().Key != 13);
        Logger.Message("Game started", LogLevel.Info);

        //Create new player
        int Count = 1;
        bool Status;
        do
        {
            string InputPlayer;
            bool DataPassed;
            do
            {
                Console.Clear();
                Console.Write(page.CreatePlayer(Count));
                InputPlayer = Console.ReadLine()!;
                IData Data = Game.ValidatorPlayer(InputPlayer);
                if (!Data.Status)
                {
                    DataNotCorrect(Data.Message, 1500);
                    DataPassed = Data.Status;
                    Logger.Message("fail to add name Player, retry to enter", LogLevel.Error);
                }
                else
                {
                    DataPassed = Data.Status;
                }
            } while (!DataPassed);
            Count++;
            Status = Game.AddPlayer(InputPlayer);
            DataCorrect("Player data saved", 1000);
        } while (Status);
    }

    private static void PreparationPhase()
    {
        //transition
        do
        {
            Console.Clear();
            Console.Write(page.Transition(true, Game.GetPlayerActive().Name));
        } while ((int)Console.ReadKey().Key != 13);

        //setup player ship
        int Count = 0;
        do
        {
            IDictionary<string, IShip> ListShipMenu = Game.GetListShipInGame();
            string PlayerName = Game.GetPlayerActive().Name;
            string[,] ArenaMap = Game.GetShipPlayerInArena(); //aarrgg

            while (ListShipMenu.Count != 0)
            {
                bool IsPassed = false;
                while (!IsPassed)
                {
                    Console.Clear();
                    Console.Write(page.PreparationMap(ListShipMenu, PlayerName, ArenaMap));
                    string? InputPlayer = Console.ReadLine();
                    IData Data = Game.ValidatorPreparation(InputPlayer!, ListShipMenu, ArenaMap);
                    if (!Data.Status)
                    {
                        DataNotCorrect(Data.Message, 1000);
                        Logger.Message(Data.Message, LogLevel.Error);
                        break;
                    }
                    Game.AddShipToArena(InputPlayer!, ListShipMenu);
                    Console.Clear();
                    Console.Write(page.PreparationMap(ListShipMenu, PlayerName, ArenaMap));
                    DataCorrect(" ship added.", 1000);
                    IsPassed = Data.Status;
                    Logger.Message("Ship added.", LogLevel.Info);
                }
            }
            DataCorrect("Ship Position saved.", 1000);
            Count += Game.TurnControl();
        } while (Count != 2);
    }

    private static void BattlePhase()
    {
        //transition
        do
        {
            Console.Clear();
            Console.Write(page.Transition(false, Game.GetPlayerActive().Name));
        } while ((int)Console.ReadKey().Key != 13);

        bool WinnerStatus = false;
        while (!WinnerStatus)
        {
            while (true)
            {
                string[,] ArenaMap = Game.GetPlayerDataInGame().HitInOpponentArena;
                string[,] ShipPosition = Game.GetShipPlayerInArena();
                string PlayerName = Game.GetPlayerActive().Name;

                Console.Clear();
                Console.Write(page.BattleMap(PlayerName, ArenaMap));
                string Input = ReadKeyCoor();
                if (Input == "HOME")
                {
                    Console.Clear();
                    Console.Write(page.ShipPosition(ShipPosition));
                    DataCorrect("", 3000);
                    break;
                }
                IData Data = Game.ValidateInputCoorHit(Input, ArenaMap)!;
                if (!Data.Status)
                {
                    DataNotCorrect("\n " + Data.Message, 1000);
                    break;
                }
                IData Result = Game.HitOpponent(Input);
                ArenaMap = Game.GetPlayerDataInGame().HitInOpponentArena;

                Console.Clear();
                Console.Write(page.HitResult(Result.Status, Input, ArenaMap));

                string AdditionalMessage = "";
                if (Result.Message.Length > 0)
                    AdditionalMessage = $"  You Destroyed {Result.Message} Opponent!";

                DataCorrect(AdditionalMessage, 2000);

                if (Game.GetWinnerStatus())
                {
                    DataCorrect("  All Ship Oppenet Distryed, YOU WIN!!", 2000);
                    WinnerStatus = true;
                    break;
                };

                Game.TurnControl();
            }
        }
    }

    private static async Task BattleshipEnd()
    {
        Data Data = new("", true);
        string PlayerName = Game.GetPlayerActive().Name;
        _ = Task.Run(() => BattleshipEndPage(Data, PlayerName));
        do
        {
            Console.Clear();
            Console.Write(page.PlayerWinner(Data.Status, PlayerName));
        } while ((int)Console.ReadKey().Key != 13);
    }

    private static async void BattleshipEndPage(IData Data, string PlayerName)
    {
        while (true)
        {
            await Task.Delay(1000);
            Console.Clear();
            Data.Status = !Data.Status;
            Console.Write(page.PlayerWinner(Data.Status, PlayerName));
        }
    }

    private static void DataCorrect(string Message, int Count)
    {
        Console.WriteLine(AddColor.Message(Message, ConsoleColor.Green));
        Thread.Sleep(Count);
    }

    private static void DataNotCorrect(string Message, int Count)
    {
        Console.WriteLine(AddColor.Message(Message, ConsoleColor.Red));
        Thread.Sleep(Count);
    }

    private static string ReadKeyCoor()
    {
        string InputCoor = "";
        while (true)
        {
            ConsoleKey key = Console.ReadKey().Key;
            if ((int)key == 36) return "HOME";
            if ((int)key == 13) return InputCoor;
            char Input = Convert.ToChar(key);
            InputCoor += Input.ToString();
        }
    }
}