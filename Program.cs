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
        int _activePlayer = game.GetActivePlayer();
        List<Player> _listPlayer = game.GetPlayerList();
        Dictionary<int, Dictionary<string, IShip>> _listShipsPlayer = game.GetPlayerShipList();
        Dictionary<string, IShip> _Ships = game.GetShipList();

        int Index = 0;
        while (Index < 2)
        {
            Player player = new();
            _listPlayer.Add(player);
            _listPlayer[_activePlayer - 1].Id = _activePlayer;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("**                 BATTLESHIP               **");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.Write($"Enter your name (Player {_activePlayer}) : ");
            _listPlayer[_activePlayer - 1].Name = Console.ReadLine();
            Console.WriteLine("Player name saved!");
            Thread.Sleep(1000);

            game.TurnControl();
            Index++;
        }
        foreach (var player in _listPlayer)
        {
            _listShipsPlayer.Add(player.Id, _Ships);
        }
    }

    //display player name turn
    private static void DisplayPlayerTurn()
    {
        string name = game.GetPlayerName();
        Console.WriteLine($"             \n Your turn, {name}");
        Console.Write($" Hit Enemy : ");
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
            game.AddShipToArena(_playerAddShip);
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
                    game.DisplayClear();
                    game.DisplayHitArena();
                    DisplayPlayerTurn();

                    inputCoor = game.ReadKeyCoor();
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
                game.DisplayClear();
                DisplayPlayerTurn();
                game.DisplayHitArena();
                game.DisplayHitResult(coor, result);
                game.TurnControl();
                TryInput = false;
            }
        }
    }

    private static void BattleEnd()
    {

    }

}