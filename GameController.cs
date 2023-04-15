using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Drawing;
using System.Threading.Tasks;

class GameController
{
    private List<Player>? _listPlayer;
    private bool _isPlayingMode = false;
    private int _activePlayer;
    private Dictionary<int, string[,]> _shipPlayerInArena = new();
    private Dictionary<int, string[,]> _arenaHitPlayer = new();
    private Dictionary<int, Dictionary<string, IShip>>? _listShipsPlayer = new();
    private Dictionary<string, string>? _listShip = new();  // New property
    private Arena _arena = new();   // New property
    private string[,] ArenaArray = new string[10, 10]; //new property

    //Create game with player
    public GameController(List<Player> players)
    {
        _listPlayer = players;
        _listPlayer[0].Id = 1;
        _listPlayer[1].Id = 2;

        _activePlayer = 1;

        Submarine submarine = new();
        Battleship battleship = new();
        Cruiser cruiser = new();
        Destroyer destroyer = new();
        Carrier carrier = new();

        Dictionary<string, IShip> listShip = new();
        listShip.Add("B", battleship);
        listShip.Add("C", cruiser);
        listShip.Add("R", carrier);
        listShip.Add("S", submarine);
        listShip.Add("D", destroyer);

        foreach (var player in _listPlayer)
        {
            _listShipsPlayer.Add(player.Id, listShip);
        }

        _listShip.Add("B", "Battleship");
        _listShip.Add("R", "Carrier");
        _listShip.Add("C", "Cruiser");
        _listShip.Add("S", "Submarine");
        _listShip.Add("D", "Destroyer");
    }

    //home Game
    public async void GameHome()
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

        Console.WriteLine("Loading...");
        for (int i = 0; i < 44; i++)
        {
            Console.Write("=");
            Thread.Sleep(50);
        }

    }

    //create player
    public void CreatePlayer()
    {
        int count = 0;
        string name;
        while (count < 2)
        {
            name = "";
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("**                 BATTLESHIP               **");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.Write($"Enter your name (Player {_activePlayer}) : ");
            name = Console.ReadLine();
            _listPlayer[_activePlayer - 1].Name = name;
            Console.WriteLine("Player name saved!");
            Thread.Sleep(1000);

            TurnControl();
            count++;
        }
    }

    //add ship to arena player
    public void AddToMap(string key, Coordinate coor, string rotate)
    {
        Dictionary<string, IShip> listShip = _listShipsPlayer[_activePlayer];
        _listShip.Remove(key);
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
        }
        else
        {
            Console.WriteLine("Input invalid");
        }
    }

    //display player name turn
    public string DisplayName()
    {
        return _listPlayer[_activePlayer - 1].Name;
    }

    public void SetDefaultArena()
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

    //for controlling player turn
    public void TurnControl()
    {
        if (_activePlayer == 1) { _activePlayer = 2; }
        else { _activePlayer = 1; };
    }

    // For reset setting mode
    public void ResetShipCoor()
    {
        _listShip["B"] = "Battleship";
        _listShip["R"] = "Carrier";
        _listShip["C"] = "Cruiser";
        _listShip["S"] = "Submarine";
        _listShip["D"] = "Destroyer";

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

    //displaying arena information in preparation mode
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

    // For display ship that should placed player on the arena
    public bool DisplayShip()
    {
        int cout = 0;
        Console.WriteLine();
        Console.WriteLine($"List Ship :               Your Turn, {DisplayName()}");
        foreach (var item in _listShip)
        {

            Console.WriteLine($"[{item.Key}] {item.Value}");

        }
        int Count = _listShip.Count;
        return Count != 0;
    }


    //save coordinates ship player
    public void SaveCoordinates()
    {
        string[,] ArrayData = new string[10, 10];
        Array.Copy(ArenaArray, ArrayData, ArenaArray.Length);
        _shipPlayerInArena.Add(_activePlayer, ArrayData);
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
        if (shipCoor[x - 1, y - 1] != "_")
        {
            blankCoor[x - 1, y - 1] = "H";
            shipCoor[x - 1, y - 1] = "H";
            result = "Hit!!";
        }
        else
        {
            blankCoor[x - 1, y - 1] = "*";
            shipCoor[x - 1, y - 1] = "*";
            result = "Miss";
        }
        _shipPlayerInArena[EnemyId] = shipCoor;
        _arenaHitPlayer[_activePlayer] = blankCoor;

        return result;
    }
}