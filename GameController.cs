using System.Drawing;

class GameController
{
    private List<Player>? _listPlayer = new();
    private Dictionary<int, string[,]> _shipPlayerInArena = new();
    private static Coordinate coordinate = new();
    private Dictionary<int, string[,]> _arenaHitPlayer = new();
    private Dictionary<int, Dictionary<string, IShip>>? _listShipsPlayer = new();
    private Dictionary<string, string>? _listShipMenu = new();  // New property
    private Dictionary<string, IShip> _Ships = new();
    private Arena _arena = new();   // New property
    private string[,] ArenaArray = new string[10, 10]; //new property
    private int _activePlayer = 1;

    //Create game with player
    public GameController()
    {

        Submarine submarine = new();
        Battleship battleship = new();
        Cruiser cruiser = new();
        Destroyer destroyer = new();
        Carrier carrier = new();

        _Ships.Add("B", battleship);
        _Ships.Add("C", cruiser);
        _Ships.Add("R", carrier);
        _Ships.Add("S", submarine);
        _Ships.Add("D", destroyer);

        _listShipMenu.Add("B", "Battleship");
        _listShipMenu.Add("R", "Carrier");
        _listShipMenu.Add("C", "Cruiser");
        _listShipMenu.Add("S", "Submarine");
        _listShipMenu.Add("D", "Destroyer");

        SetArenaClear();
    }

    //home Game
    public void GameHome()
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
    public void CreatePlayer()
    {
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
            string name = Console.ReadLine();
            _listPlayer[_activePlayer - 1].Name = name;
            Console.WriteLine("Player name saved!");
            Thread.Sleep(1000);

            TurnControl();
            Index++;
        }
        foreach (var player in _listPlayer)
        {
            _listShipsPlayer.Add(player.Id, _Ships);
        }
    }


    //display player name turn
    public void DisplayPlayerTurn()
    {
        string name = _listPlayer[_activePlayer - 1].Name;
        Console.WriteLine($"             \n Your turn, {name}");
        Console.Write($" Hit Enemy : ");
    }

    //Set default arena
    public void SetArenaClear()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                ArenaArray[i, j] = "_";
            }
        }

        string[,] ArrayData = new string[10, 10];
        string[,] ArrayData2 = new string[10, 10];

        Array.Copy(ArenaArray, ArrayData, ArenaArray.Length);
        Array.Copy(ArenaArray, ArrayData2, ArenaArray.Length);

        _arenaHitPlayer.Add(1, ArrayData);
        _arenaHitPlayer.Add(2, ArrayData2);
    }

    //display hit arena information
    public void DisplayHitArena()
    {
        Array.Copy(_arenaHitPlayer[_activePlayer], ArenaArray, _arenaHitPlayer[_activePlayer].Length);
        DisplayArena();
    }

    //Coontrolling player turn
    public void TurnControl()
    {
        if (_activePlayer == 1) { _activePlayer = 2; }
        else { _activePlayer = 1; };
    }

    //Add ship to arena
    public void AddShipToArena(bool _playerAddShip)
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
                string[] Data = DataInput.Split(" ");

                if (Data.Count() != 3 || Data[1].Split(",").Count() != 2)
                {
                    Console.WriteLine("Data Invalid");
                    Thread.Sleep(1000);
                    DataValid = true;
                    break;
                }
                else if (ValidateListShipMenu(Data[0]))
                {
                    Console.WriteLine("Ship not found");
                    Thread.Sleep(1000);
                    DataValid = true;
                    break;
                };

                string[] Coor = Data[1].Split(",");
                if (!int.TryParse(Coor[0], out int x) || !int.TryParse(Coor[1], out int y))
                {
                    Console.WriteLine("Coordinate Invalid");
                    Thread.Sleep(1000);
                    DataValid = true;
                    break;
                }
                coordinate.SetValue(int.Parse(Coor[0]), int.Parse(Coor[1]));

                bool AddMap = ValidateAddToMap(Data[0].ToUpper(), coordinate, Data[2].ToUpper());
                if (AddMap)
                {
                    DataValid = true;
                    break;
                }

                bool CheckRotate = AddToMap(Data[0].ToUpper(), coordinate, Data[2].ToUpper());
                if (!CheckRotate)
                {
                    Console.WriteLine("Data rotate Invalid");
                    Thread.Sleep(1000);
                    DataValid = true;
                    break;
                }
                DataValid = false;
            }
        }
        SaveCoordinates();
    }

    //Check list ship
    public bool ValidateListShipMenu(string data)
    {
        bool state = true;
        foreach (KeyValuePair<string, string> ship in _listShipMenu)
        {
            if (ship.Key == data.ToUpper())
            {
                state = false;
            }
        }
        return state;
    }

    //Validation for input ship to arena
    public bool ValidateAddToMap(string key, Coordinate coor, string rotate)
    {
        Dictionary<string, IShip> listShip = _listShipsPlayer[_activePlayer];
        int x = coor.GetValueX();
        int y = coor.GetValueY();
        bool result = false;

        List<Coordinate> listCoor = new();
        if (rotate == "V")
        {
            for (int i = 0; i < listShip[key].ShipSize; i++)
            {
                if (ArenaArray[i + x - 1, y - 1] != "_")
                {
                    Console.WriteLine($"Ship {listShip[ArenaArray[i + x - 1, y - 1]]} here, Reposition your ship!");
                    Thread.Sleep(2000);
                    result = true;
                    break;
                }
            }
            return result;
        }
        else if (rotate == "H")
        {
            for (int i = 0; i < listShip[key].ShipSize; i++)
            {
                if (ArenaArray[x - 1, i + y - 1] != "_")
                {
                    Console.WriteLine($"Ship {listShip[ArenaArray[x - 1, i + y - 1]]} here, Reposition your ship!");
                    Thread.Sleep(2000);
                    result = true;
                    break;
                }

            }
            return result;
        }
        else
        {
            return result;
        }
    }

    //add ship to arena player
    public bool AddToMap(string key, Coordinate coor, string rotate)
    {
        Dictionary<string, IShip> listShip = _listShipsPlayer[_activePlayer];
        int x = coor.GetValueX();
        int y = coor.GetValueY();
        List<Coordinate> listCoor = new();
        if (rotate == "V")
        {
            for (int i = 0; i < listShip[key].ShipSize; i++)
            {
                ArenaArray[i + x - 1, y - 1] = key;
                Coordinate c = new();
                c.SetValue(i + x - 1, y - 1);
                listCoor.Add(c);
                listCoor[i] = c;
            }
            _listShipMenu.Remove(key);
            return true;
        }
        else if (rotate == "H")
        {
            for (int i = 0; i < listShip[key].ShipSize; i++)
            {
                ArenaArray[x - 1, i + y - 1] = key;
                Coordinate c = new();
                c.SetValue(x - 1, i + y - 1);
                listCoor.Add(c);
                listCoor[i] = c;
            }
            _listShipMenu.Remove(key);
            return true;
        }
        else
        {
            return false;
        }
    }


    //For reset setting mode
    public void ResetShipCoor()
    {
        _listShipMenu["B"] = "Battleship";
        _listShipMenu["R"] = "Carrier";
        _listShipMenu["C"] = "Cruiser";
        _listShipMenu["S"] = "Submarine";
        _listShipMenu["D"] = "Destroyer";

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                ArenaArray[i, j] = "_";
            }
        }
    }

    //Display player ship position
    public void DisplayShipPosition()
    {
        Array.Copy(_shipPlayerInArena[_activePlayer], ArenaArray, ArenaArray.Length);
        DisplayArena();
    }

    //Displaying arena information in preparation mode
    public void DisplayArena()
    {
        Console.Clear();
        string? input = " ";
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

    //For display ship that should placed player on the arena
    public bool DisplayListShip()
    {
        int cout = 0;
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

    //Hit Enemy 
    public string HitEnemy(int x, int y)
    {
        int EnemyId;
        string result;

        if (_activePlayer == 1) EnemyId = 2;
        else EnemyId = 1;

        string[,] shipCoor = _shipPlayerInArena[EnemyId];
        string[,] blankCoor = _arenaHitPlayer[_activePlayer];
        if (shipCoor[x - 1, y - 1] != "_" && shipCoor[x - 1, y - 1] != "H" && shipCoor[x - 1, y - 1] != "*")
        {
            blankCoor[x - 1, y - 1] = "H";
            shipCoor[x - 1, y - 1] = "H";
            result = "Hit!!";
        }
        else if (shipCoor[x - 1, y - 1] == "_")
        {
            blankCoor[x - 1, y - 1] = "*";
            shipCoor[x - 1, y - 1] = "*";
            result = "Miss";
        }
        else
        {
            return "false";
        }
        _shipPlayerInArena[EnemyId] = shipCoor;
        _arenaHitPlayer[_activePlayer] = blankCoor;

        return result;
    }


    //Display hit result
    public void DisplayHitResult(string[] coor, string result)
    {
        Console.WriteLine($"\n Coordinates {coor[0]},{coor[1]}         result : {result}");
        Thread.Sleep(1000);
    }

    //clearing console
    public void DisplayClear()
    {
        Console.Clear();
    }

    //Method input for coordinate
    public string ReadKeyCoor()
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
                DisplayShipPosition();
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

    //save coordinates ship player
    public void SaveCoordinates()
    {
        string[,] ArrayData = new string[10, 10];
        Array.Copy(ArenaArray, ArrayData, ArenaArray.Length);
        _shipPlayerInArena.Add(_activePlayer, ArrayData);
        Console.WriteLine("Data Saved!");
        Thread.Sleep(1000);
    }
}