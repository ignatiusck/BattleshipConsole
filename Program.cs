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
        while (_playerTurn)
        {
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
                    DisplayMap = (inputCoor == "false") ? true : false;
                }
                string[] coor = inputCoor.Split("¼");
                string result = game.HitEnemy(int.Parse(coor[0]), int.Parse(coor[1]));
                if (result == "false")
                {
                    Console.WriteLine($"Input Invalid. try again.");
                    Thread.Sleep(1000);
                    break;
                }
                DisplayClear();
                DisplayPlayerTurn();
                game.UpdateHitArena();
                DisplayArena();
                DisplayHitResult(coor, result);
                game.TurnControl();
                TryInput = false;
            }
        }
    }

    private static void BattleEnd()
    {

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
        while (Index < 2)
        {
            Player player = new();

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("**                 BATTLESHIP               **");
            Console.WriteLine("==============================================");
            Console.WriteLine();

            Console.Write($"Enter your name (Player {_activePlayer}) : ");
            _listPlayer.Add(player);
            _listPlayer[_activePlayer - 1].Name = Console.ReadLine();
            _listPlayer[_activePlayer - 1].Id = _activePlayer;
            Console.WriteLine("Player name saved!");
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

                Console.Write("\n" + "Place your ship : ");
                string DataInput = Console.ReadLine();
                Data data = game.ValidationInShip(DataInput);
                if (data.State == false)
                {
                    DataValid = true;
                    Console.WriteLine($"{data.Message}");
                    Thread.Sleep(2000);
                    _playerAddShip = true;
                }
                else { DataValid = false; }

            }
        }
        Data isSaved = game.SaveCoordinates();
        Console.WriteLine($"{isSaved.Message}");
        Thread.Sleep(1000);
    }
    //display player name turn
    private static void DisplayPlayerTurn()
    {
        string name = game.GetPlayerName();
        Console.WriteLine($"             \n Your turn, {name}");
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
        Console.WriteLine($"List Ship :               Your Turn, {name}");
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
                game.DisplayShipPosition();
                DisplayArena();
                Console.WriteLine("\n Your Ship Position        Will close in 3s. ");
                Console.WriteLine(" ");
                Thread.Sleep(3000);
                inputCoor = "false";
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
    private static void DisplayHitResult(string[] coor, string result)
    {
        Console.WriteLine($"\n Coordinates {coor[0]},{coor[1]}         result : {result}");
        Thread.Sleep(1000);
    }
    //clearing console
    private static void DisplayClear()
    {
        Console.Clear();
    }

    
}