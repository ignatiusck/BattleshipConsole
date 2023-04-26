using System.Drawing;

class Program
{
    private static GameController game = new();
    private static bool _playerAddShip = true;
    private static bool _playerTurn = true;
    private static int _index = 1;

    public static void Main(string[] args)
    {
        BattleshipStart();
        Preparation();
        BattleStart();
        BattleEnd();
    }

    private static void BattleshipStart()
    {
        DisplayGameHome();
        DisplayCreatePlayer();
    }
    private static void Preparation()
    {
        DisplayPreparationStart();
        while (_playerTurn)
        {
            game.ResetShipListMenu();
            game.ResetShipCoor();
            AddShipToArena(_playerAddShip);
            game.TurnControl();
            _playerAddShip = (_index < 2) ? _playerTurn = true : _playerTurn = false;
            _index++;
        }
        _playerTurn = true;
    }
    private static void BattleStart()
    {
        DisplayBattleStart();
        while (_playerTurn)
        {
            string inputCoor = "";
            bool DisplayMap = true;
            bool TryInput = true;
            while (TryInput)
            {
                while (DisplayMap)
                {
                    DisplayClear();
                    game.UpdateHitArena();
                    DisplayArena();
                    DisplayPlayerTurn();

                    inputCoor = ReadKeyCoor();
                    DisplayMap = inputCoor == "getmap";
                    if (!DisplayMap)
                    {
                        DisplayMap = game.ValidateHitInput(inputCoor);
                        if (DisplayMap)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine($" Input invalid.");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                        }
                    }
                }
                string[] coor = inputCoor.Split("¼");
                Data result = game.HitEnemy(int.Parse(coor[0]), int.Parse(coor[1]));
                if (result.Message == "Worng data")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Input Invalid. try again.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    break;
                }
                DisplayClear();
                DisplayPlayerTurn();
                game.UpdateHitArena();
                DisplayArena();
                DisplayHitResult(coor, result);
                if (result.Message.Last() == '!')
                {
                    _playerTurn = false;
                    break;
                }
                game.TurnControl();
                TryInput = false;
            }
        }
    }

    private static void BattleEnd()
    {
        DisplayGameEnd();
    }
    //End Display
    private static async Task DisplayWinnerAsync()
    {
        bool state = false;
        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("**                 BATTLESHIP               **");
            Console.WriteLine("==============================================");
            Console.WriteLine("\n \n");
            Console.ForegroundColor = state ? ConsoleColor.Yellow : ConsoleColor.Black;
            state = state ? false : true;
            Console.WriteLine(" WINNER");
            Console.ResetColor();
            Console.WriteLine($" {game.GetPlayerName()}");
            Console.WriteLine("\n \n");
            Console.Write(" Press Enter to Exit...");
            await Task.Delay(1000);
        }
    }
    //Battle end
    private static void DisplayGameEnd()
    {
        ConsoleKey Input;
        Task.Run(() => DisplayWinnerAsync());
        do
        {
            Input = Console.ReadKey().Key;
            //Console.ReadKey();

        } while (((int)Input) != 13);
    }

    //display preparation phase
    private static void DisplayPreparationStart()
    {
        ConsoleKey Input;
        string name = game.GetPlayerName();
        do
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("**            PREPARATION PHASE             **");
            Console.WriteLine("==============================================");
            Console.WriteLine("\n");
            Console.WriteLine(" Place all your ship in the Arena.");
            Console.Write($" You play first, ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);
            Console.ResetColor();
            Console.WriteLine("\n \n");
            Console.Write(" Press Enter to continue...");
            Input = Console.ReadKey().Key;

        } while (((int)Input) != 13);
    }

    //display preparation phase
    private static void DisplayBattleStart()
    {
        ConsoleKey Input;
        string name = game.GetPlayerName();
        do
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("**               BATTLE START               **");
            Console.WriteLine("==============================================");
            Console.WriteLine("\n");
            Console.WriteLine(" Attack your Opponent!! ");
            Console.Write($" You play first, ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);
            Console.ResetColor();
            Console.WriteLine("\n \n");
            Console.Write(" Press Enter to continue...");
            Input = Console.ReadKey().Key;

        } while (((int)Input) != 13);
    }

    //home Game
    private static void DisplayGameHome()
    {
        ConsoleKey Input;
        do
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("**                 BATTLESHIP               **");
            Console.WriteLine("==============================================");
            Console.WriteLine("\n \n");
            Console.WriteLine("                 MULTIPLAYER");
            Console.WriteLine("          Press Enter to continue...");
            Console.WriteLine("\n \n");
            Input = Console.ReadKey().Key;

        } while (((int)Input) != 13);
    }

    //create player
    private static void DisplayCreatePlayer()
    {
        Data data = game.GetDataCreatePlayer();

        int _activePlayer = data.ActivePlayer;
        List<Player> _listPlayer = data.PlayerList;

        int Index = 0;
        string Name;
        while (Index < 2)
        {
            Player player = new();
            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("==============================================");
                Console.WriteLine("**                 BATTLESHIP               **");
                Console.WriteLine("==============================================");
                Console.WriteLine();

                Console.Write($"Enter your name (Player {_activePlayer}) : ");

                Name = Console.ReadLine();

                if (Name.Length < 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Name should be atleast 3 characters long");
                    Thread.Sleep(2000);
                    Console.ResetColor();

                };
            } while (Name.Length < 3);
            _listPlayer.Add(player);
            _listPlayer[_activePlayer - 1].Name = Name;
            _listPlayer[_activePlayer - 1].Id = _activePlayer;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Player name saved!");
            Console.ResetColor();
            Thread.Sleep(1000);

            _activePlayer = game.TurnControl();
            Index++;
        }
        game.SetPlayerList(_listPlayer);
    }
    //add ship to arena
    private static void AddShipToArena(bool _playerAddShip)
    {
        bool DataValid = true;
        while (DataValid)
        {
            while (_playerAddShip == true)
            {
                DisplayArena();
                _playerAddShip = DisplayListShip();
                if (_playerAddShip == false) break;

                Console.WriteLine();
                Console.WriteLine("Input : 'KEY Coordinat H/V' Example : 'B 1,1 H' ");
                Console.Write("Place your ship : ");
                string DataInput = Console.ReadLine();
                Data data = game.ValidationInShip(DataInput);
                if (data.State == false)
                {
                    DataValid = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{data.Message}");
                    Console.ResetColor();
                    Thread.Sleep(2000);
                    _playerAddShip = true;
                }
                else { DataValid = false; }

            }
        }
        Data isSaved = game.SaveCoordinates();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{isSaved.Message}");
        Console.ResetColor();
        Thread.Sleep(1000);
    }
    //display player name turn
    private static void DisplayPlayerTurn()
    {
        string name = game.GetPlayerName();
        Console.Write($"\n                         Your turn, ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{name}");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine(" Press 'Home' to see your ship positions ");
        Console.WriteLine(" Input : 'y,x' Example : '3,2' ");
        Console.Write($" Hit Enemy : ");
    }

    //display arena
    private static void DisplayArena()
    {
        Arena _arena = new();
        string[,] ArenaArray = game.GetArenaArray();

        Console.Clear();
        Console.Write("\n");
        Console.Write("  0  1");
        Size arena = _arena.GetArenaSize();
        for (int k = 2; k <= arena.Width; k++)
        {
            Console.Write($"   {k}");
        }
        Console.Write("\n");
        for (int i = 1; i <= arena.Height; i++)
        {
            if (i == 10)
            {
                Console.Write($" {i}");
            }
            else
            {
                Console.Write($"  {i}");
            }

            for (int j = 0; j < arena.Width; j++)
            {
                Console.Write($" [{ArenaArray[i - 1, j]}]");
            }
            Console.Write("\n");
        }
    }

    //displaying list ship
    private static bool DisplayListShip()
    {
        Data data = game.GetListShipData();
        Dictionary<string, string> _listShipMenu = data.ListShipMenu;
        List<Player> _listPlayer = data.PlayerList;
        int _activePlayer = data.ActivePlayer;

        string name = _listPlayer[_activePlayer - 1].Name;
        Console.WriteLine();
        Console.Write($"List Ship :               Your Turn, ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{name}");
        Console.ResetColor();
        Console.WriteLine($"KEY  Size    NAME");
        foreach (var item in _listShipMenu)
        {
            Console.WriteLine($"[{item.Key}] {item.Value}");
        }
        int Count = _listShipMenu.Count;
        return Count != 0;
    }

    //Method input for coordinate
    private static string ReadKeyCoor()
    {
        ConsoleKey key;
        string inputCoor = "";
        int btn = 0;
        bool state = true;
        while (btn != 13)
        {
            key = Console.ReadKey().Key;
            btn = (int)key;
            if (btn == 36)
            {
                Console.Clear();
                game.CopyShipPositionToMap();
                DisplayArena();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n Your Ship Position        Will close in 3s. ");
                Console.ResetColor();
                Console.WriteLine(" ");
                Thread.Sleep(3000);
                inputCoor = "getmap";
                break;
            }
            else if (btn == 13)
            {
                break;
            }
            char c = Convert.ToChar(btn);
            inputCoor += c.ToString();
        }
        return inputCoor;
    }
    //Display hit result
    private static void DisplayHitResult(string[] coor, Data result)
    {
        string Result = result.State ? "Hit" : "Miss";
        ConsoleColor Color = Result == "Hit" ? ConsoleColor.Green : ConsoleColor.Red;
        Console.Write($"\n  Coordinates {coor[0]},{coor[1]}           result : ");
        Console.ForegroundColor = Color;
        Console.WriteLine(Result);
        if (result.Message.Length >= 12)
        {
            Console.WriteLine();
            Console.WriteLine(" " + result.Message);
            Thread.Sleep(1500);
        }
        Thread.Sleep(1000);
        Console.ResetColor();

    }
    //clearing console
    private static void DisplayClear()
    {
        Console.Clear();
    }
}