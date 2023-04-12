using System.Runtime.Serialization;
using System.Xml;
using System.Drawing;

class GameController
{
    private List<Player>? _listPlayer;
    private bool _isPlayingMode = false;
    private int _activePlayer = 1;
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

    //for controlling player turn
    public void TurnControl()
    {
        if (_activePlayer == 1) _activePlayer = 2;
        else _activePlayer = 1;
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
    //Title Game
    public void GameTitle()
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("**                 BATTLESHIP               **");
        Console.WriteLine("==============================================");
    }

    //For displaying arena information in preparation mode
    public void DisplayArena()
    {

        string? input = " ";
        Console.WriteLine();
        Console.Write("  0  1");
        Size arena = _arena.GetArenaSize();
        for (int k = 2; k <= arena.Width; k++)
        {
            Console.Write($"   {k}");
        }
        Console.WriteLine();
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
            Console.WriteLine();
        }
    }

    // For display ship that should placed player on the arena
    public bool DisplayShip()
    {
        Console.WriteLine();
        Console.WriteLine("List Ship : ");
        foreach (var item in _listShip)
        {
            Console.WriteLine($"[{item.Key}] {item.Value}");
        }
        int Count = _listShip.Count;
        return Count != 0;
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

    //add ship to palyer map
}