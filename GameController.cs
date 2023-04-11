using System.Xml;
using System.Drawing;

class GameController
{
    private List<Player> _listPlayer;
    private bool _isPlayingMode = false;
    private Player Player;
    private Dictionary<Player, List<IShip>> _listShipsCoors;


    private Dictionary<string, string[,]> _listShipPlayer;
    private Dictionary<string, string> _listShip = new();  // New property
    private Arena _arena = new();   // New property
    private string[,] ArenaArray = new string[10, 10]; //new property
    private Player player_1 = new();
    private Player player_2 = new();

    public void ResetShipCoor()
    {
        _listShip.Add("B", "Battleship");
        _listShip.Add("R", "Carrier");
        _listShip.Add("C", "Cruiser");
        _listShip.Add("S", "Submarine");
        _listShip.Add("D", "Destroyer");

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                ArenaArray[i, j] = "_";
            }
        }
    }

    public void CreatePlayer(string name_1, string name_2)
    {
        player_1.Name = name_1;
        player_2.Name = name_2;
        _listShipPlayer.Add(player_1.Name, ArenaArray);
        _listShipPlayer.Add(player_2.Name, ArenaArray);
    }

    public void AddPlayer(string name)
    {
        _listShipPlayer[name] = ArenaArray;
    }
    public void DisplayArena()
    {

        string input = "";
        Console.WriteLine("Arena : ");
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

    public void DisplayShip()
    {
        Console.WriteLine();
        Console.WriteLine("List Ship : ");
        foreach (var item in _listShip)
        {
            Console.WriteLine($"[{item.Key}] {item.Value}");
        }
    }

    public void AddToMap(string key, Coordinate coor, string rotate)
    {
        _listShip.Remove(key);
        int x = coor.GetValueX();
        int y = coor.GetValueY();
        if (rotate == "V")
        {
            for (int i = 0; i < 3; i++)
            {
                ArenaArray[i + x, y - 1] = key;
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                ArenaArray[x - 1, i + y] = key;
            }
        }

    }
}