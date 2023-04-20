using System.Data.Common;
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
    private Dictionary<int, int> _totalHitPlayer = new();
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

        ResetShipListMenu();
        SetArenaClear();
    }
    //add total hit points
    public void AddTotalHitPoints()
    {
        int count = 0;
        foreach (KeyValuePair<string, IShip> ship in _Ships)
        {
            count += ship.Value.ShipSize;
        }
        _totalHitPlayer.Add(_activePlayer, count);
    }

    //reset the ship list
    public void ResetShipListMenu()
    {
        foreach (KeyValuePair<string, IShip> ship in _Ships)
        {
            _listShipMenu[ship.Key] = "  " + ship.Value.ShipSize + "   " + ship.Value.ShipName;
        }
    }
    //For reset setting mode
    public void ResetShipCoor()
    {

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                ArenaArray[i, j] = "_";
            }
        }
    }
    //get active player
    public Data GetDataCreatePlayer()
    {
        return new Data()
        {
            ActivePlayer = _activePlayer,
            PlayerList = _listPlayer,
        };
    }

    //set Player to the list
    public void SetPlayerList(List<Player> _listPlayer)
    {
        foreach (Player player in _listPlayer)
        {
            _listShipsPlayer.Add(player.Id, _Ships);
        }
    }

    //get player name
    public string GetPlayerName()
    {
        return _listPlayer[_activePlayer - 1].Name;
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
    public void UpdateHitArena()
    {
        Array.Copy(_arenaHitPlayer[_activePlayer], ArenaArray, _arenaHitPlayer[_activePlayer].Length);
    }

    //Coontrolling player turn
    public int TurnControl()
    {
        if (_activePlayer == 1) { return _activePlayer = 2; }
        else { return _activePlayer = 1; };
    }

    //Add ship to arena
    public Data ValidationInShip(string DataInput)
    {
        Data data = new();
        string[] Data = DataInput.Split(" ");
        if (Data.Count() != 3 || Data[1].Split(",").Count() != 2)
        {
            data.Message = "Data Invalid";
            data.State = false;
            return data;
        }
        else if (ValidateListShipMenu(Data[0]))
        {
            data.Message = "Ship not found";
            data.State = false;
            return data;
        };

        string[] Coor = Data[1].Split(",");
        if (!int.TryParse(Coor[0], out int x) || !int.TryParse(Coor[1], out int y))
        {
            data.Message = "Coordinate Invalid";
            data.State = false;
            return data;
        }
        else if (ValidationOutShip(Data[0], Coor[0], Coor[1], Data[2]))
        {
            data.Message = "Out of range";
            data.State = false;
            return data;
        }
        coordinate.SetValue(int.Parse(Coor[0]), int.Parse(Coor[1]));

        Data validShip = ValidateAddToMap(Data[0].ToUpper(), coordinate, Data[2].ToUpper());
        if (validShip.State)
        {
            data.Message = validShip.Message;
            data.State = false;
            return data;
        }

        bool CheckRotate = AddToMap(Data[0].ToUpper(), coordinate, Data[2].ToUpper());
        if (CheckRotate)
        {
            data.Message = "Data rotate Invalid";
            data.State = false;
            return data;
        }
        return new Data()
        {
            Message = "succed.",
            State = true
        };
    }

    //Check out of arena or not
    public bool ValidationOutShip(string key, string x, string y, string rotate)
    {
        Dictionary<string, IShip> Ship = _Ships;
        int ShipSize = Ship[key.ToUpper()].ShipSize;

        if (rotate.ToUpper() == "H")
        {
            return ShipSize + int.Parse(x) - 1 > 10;
        }
        else if (rotate.ToUpper() == "V")
        {
            return ShipSize + int.Parse(y) - 1 > 10;
        }
        else
        {
            return false;
        }
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
    public Data ValidateAddToMap(string key, Coordinate coor, string rotate)
    {
        Data data = new();
        Dictionary<string, IShip> listShip = _listShipsPlayer[_activePlayer];
        int x = coor.GetValueX();
        int y = coor.GetValueY();

        List<Coordinate> listCoor = new();
        if (rotate == "V")
        {
            for (int i = 0; i < listShip[key].ShipSize; i++)
            {
                if (ArenaArray[i + x - 1, y - 1] != "_")
                {
                    data.Message = $"{listShip[ArenaArray[i + x - 1, y - 1]]} here, Reposition your ship!";
                    data.State = true;
                    break;
                }
            }
            return data;
        }
        else if (rotate == "H")
        {
            for (int i = 0; i < listShip[key].ShipSize; i++)
            {
                if (ArenaArray[x - 1, i + y - 1] != "_")
                {
                    data.Message = $"{listShip[ArenaArray[x - 1, i + y - 1]]} here, Reposition your ship!";
                    data.State = true;
                    break;
                }

            }
            return data;
        }
        else
        {
            return new Data()
            {
                Message = "Invalid value rotate",
                State = true,
            };
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
            return false;
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
            return false;
        }
        else
        {
            return true;
        }
    }

    //Display player ship position
    public void DisplayShipPosition()
    {
        Array.Copy(_shipPlayerInArena[_activePlayer], ArenaArray, ArenaArray.Length);
    }

    //Displaying arena information in preparation mode
    public string[,] GetArenaArray()
    {
        return ArenaArray;
    }

    //For display ship that should placed player on the arena
    public Data GetListShipData()
    {
        return new Data()
        {
            PlayerList = _listPlayer,
            ListShipMenu = _listShipMenu,
            ActivePlayer = _activePlayer,
        };
    }

    //Hit Enemy 
    public string HitEnemy(int x, int y)
    {
        int EnemyId;
        string result;
        Dictionary<string, IShip> listShip = _listShipsPlayer[_activePlayer];

        if (_activePlayer == 1) EnemyId = 2;
        else EnemyId = 1;

        string[,] shipCoor = _shipPlayerInArena[EnemyId];
        string[,] blankCoor = _arenaHitPlayer[_activePlayer];
        if (shipCoor[x - 1, y - 1] != "_" && shipCoor[x - 1, y - 1] != "H" && shipCoor[x - 1, y - 1] != "*")
        {
            blankCoor[x - 1, y - 1] = "H";
            shipCoor[x - 1, y - 1] = "H";
            result = "Hit!!";
            _totalHitPlayer[EnemyId]--;
            if (_totalHitPlayer[EnemyId] == 0)
            {
                return "end";
            }
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

    //save coordinates ship player
    public Data SaveCoordinates()
    {
        string[,] ArrayData = new string[10, 10];
        Array.Copy(ArenaArray, ArrayData, ArenaArray.Length);
        _shipPlayerInArena.Add(_activePlayer, ArrayData);

        AddTotalHitPoints();

        return new Data()
        {
            Message = "Data Saved!",
        };
    }
    //Validate Hit input
    public bool ValidateHitInput(string input)
    {
        string[] DataInput = input.Split("¼");
        if (input.Length != 3 || DataInput.Length != 2)
        {
            return true;
        }
        else if (!int.TryParse(DataInput[0], out int data))
        {
            return true;
        }
        else
        { return false; }

    }
    public Data GetWinnerName()
    {
        return new Data()
        {
            Message = _listPlayer[_activePlayer - 1].Name,
        };
    }
}