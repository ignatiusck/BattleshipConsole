using System.Collections.Generic;
class GameController
{
    private List<Player>? _listPlayerInfo; // list player
    // private Dictionary<int, string[,]> _shipPlayerInArena = new(); // data ship player in arena
    // private Dictionary<int, string[,]> _hitPlayerInArena = new(); // data arena hit player
    // //private Dictionary<IPlayer, Dictionary<string, IShip>>? _listPlayerInfo = new(); //full data relation player and all ships
    //private Dictionary<string, string>? _listShipMenu = new();  // list ship menu for preparation phase
    // private Dictionary<string, IShip> _ships = new(); // list ship in game
    // private Dictionary<int, int> _totalHitPlayer = new(); // total hit every player

    private static Arena? _arena;
    private string[,] _arenaArray; //Array for displaying to console
    private int _activePlayer; // active player id
    private ValidatorCreatePlayer _vPlayer;
    private ValidatorPreparationPhase _vPreparation;
    private Logger<GameController> _logger;


    //Create game with player
    public GameController()
    {
        _arena = new();

        _arenaArray = new string[_arena!.ArenaSize.Height, _arena.ArenaSize.Width];
        _activePlayer = 1;
        _listPlayerInfo = new();

        _vPlayer = new();
        _vPreparation = new();

        _logger = new();
    }

    //create ship packet
    private Dictionary<string, IShip> CreateShipPack()
    {
        return new Dictionary<string, IShip>()
        {
            ["S"] = new Submarine(),
            ["B"] = new Battleship(),
            // ["C"] = new Cruiser(),
            // ["D"] = new Destroyer(),
            // ["R"] = new Carrier(),
        };
    }

    public bool AddPlayer(string PlayerName)
    {
        Player player = new()
        {
            Id = _listPlayerInfo!.Count + 1,
            Name = PlayerName,
            ListShip = CreateShipPack(),
        };

        _listPlayerInfo.Add(player);
        if (_listPlayerInfo.Count == 2) SetPreparationArenaPlayer();
        _logger.Message("Player added.", LogLevel.Info);

        if (_listPlayerInfo.Count == 2)
        {
            TurnControl();
        }

        return _listPlayerInfo.Count < 2;
    }

    private IDictionary<string, IShip> GetListPlayerShip()
    {
        IPlayerBattleship player =
            _listPlayerInfo!.Find(player => player.Id == _activePlayer)!;

        return player.ListShip;
    }

    public string[,] GetShipPlayerInArena()
    {
        IPlayerBattleship player =
            _listPlayerInfo!.Find(player => player.Id == _activePlayer)!;

        return player.ShipPlayerInArena;
    }

    public string GetPlayerActive()
    {
        IPlayer player =
            _listPlayerInfo!.FirstOrDefault(player => player.Id == _activePlayer)!;

        return player.Name;
    }

    public void ResetShipCoor()
    {

        for (int i = 0; i < _arena!.ArenaSize.Height; i++)
        {
            for (int j = 0; j < _arena.ArenaSize.Width; j++)
            {
                _arenaArray[i, j] = "_";
            }
        }
    }

    private void SetPreparationArenaPlayer()
    {
        ResetShipCoor();
        foreach (Player player in _listPlayerInfo!)
        {
            string[,] ArrayData = new string[_arena!.ArenaSize.Height, _arena.ArenaSize.Width];
            Array.Copy(_arenaArray, ArrayData, _arenaArray.Length);
            player.ShipPlayerInArena = ArrayData;
        }
    }

    public IDictionary<string, IShip> GetListShipInGame()
    {
        Dictionary<string, IShip> ListShipMenu = new();
        foreach (KeyValuePair<string, IShip> Ship in CreateShipPack())
        {
            ListShipMenu.Add(Ship.Key, Ship.Value);
        }
        return ListShipMenu;
    }

    public void AddShipToArena(string Inputposition, IDictionary<string, IShip> ListShipMenu)
    {
        IPlayerBattleship player =
            _listPlayerInfo!.Find(player => player.Id == _activePlayer)!;

        string[] Data = Inputposition.Split(" ");
        string[] Coordinate = Data[1].Split(",");
        string Rotate = Data[2].ToUpper();
        string KeyShip = Data[0].ToUpper();
        int XCoor = int.Parse(Coordinate[0]) - 1;
        int YCoor = int.Parse(Coordinate[1]) - 1;

        for (int i = 0; i < player.ListShip[KeyShip].ShipSize; i++)
        {
            Coordinate Coor = new()
            {
                X = XCoor,
                Y = YCoor,
            };
            player.ShipPlayerInArena[Coor.X, Coor.Y] = KeyShip;
            player.ListShip[KeyShip].ShipCoordinates.Add(Coor);

            _ = (Rotate.ToUpper()! == "H") ? XCoor++ : YCoor++;
        }
        ListShipMenu.Remove(KeyShip);
    }

    public IData ValidatorPlayer(string Input)
    {
        if (_vPlayer.IslengthUnderLimit(Input, 3))
            return new Rejected($"Name should be atleast {3} characters long.");
        if (_vPlayer.IsPlayerAvailable(Input, _listPlayerInfo!))
            return new Rejected($"{Input} have been taken.");
        return new Accepted();
    }

    public IData ValodatorPreparation(string Input, IDictionary<string, IShip> ListShipMenu, string[,] ArenaMap)
    {
        if (_vPreparation.IsInputValid(Input))
            return new Rejected("Invalid input.");
        if (_vPreparation.IsShipNotValid(Input, ListShipMenu))
            return new Rejected("Ship not valid.");
        if (_vPreparation.IsCoordinateValid(Input))
            return new Rejected("Coordinate not valid.");
        if (_vPreparation.IsOutOfRange(Input, ListShipMenu))
            return new Rejected("Out of range");
        if (_vPreparation.IsRotateNotValid(Input))
            return new Rejected("Invalid rotation.");

        IData Data = _vPreparation.IsAnyShipHere(Input, ArenaMap);
        if (!Data.Status)
        {
            return new Rejected(
                $"{GetListPlayerShip()[Data.Message]} here, Reposition your ship!"
                );
        };

        return new Accepted();
    }

    public void TurnControl()
    {
        _activePlayer = _activePlayer == 1 ? 2 : 1;
    }

    // public List<IPlayer> GetListPlayers()
    // {
    //     return _listPlayerInfo;
    // }
}
//add total hit points
// public void AddTotalHitPoints()
// {
//     int count = 0;
//     foreach (KeyValuePair<string, IShip> ship in _ships)
//     {
//         count += ship.Value._shipSize;
//     }
//     _totalHitPlayer.Add(_activePlayer, count);
// }

//     // //reset the ship list
//     // public void ResetShipListMenu()
//     // {
//     //     foreach (KeyValuePair<string, IShip> ship in _ships)
//     //     {
//     //         _listShipMenu[ship.Key] = "  " + ship.Value._shipSize + "   " + ship.Value._shipName;
//     //     }
//     // }

//     // //get active player
//     // public Data GetDataCreatePlayer()
//     // {
//     //     return new Data()
//     //     {
//     //         ActivePlayer = _activePlayer,
//     //         PlayerList = _listPlayer,
//     //     };
//     // }

//     // //set Player to the list
//     // public void SetPlayerList(List<Player> _listPlayer)
//     // {
//     //     foreach (Player player in _listPlayer)
//     //     {
//     //         Dictionary<string, IShip> ships = CreateShipPack();
//     //         _listShipsPlayer.Add(player._id, ships);
//     //     }
//     // }

//     // //get player name
//     // public string GetPlayerName()
//     // {
//     //     return _listPlayer[_activePlayer - 1]._name;
//     // }

//     // //Set default arena
//     // public void SetArenaClear()
//     // {
//     //     for (int i = 0; i < 10; i++)
//     //     {
//     //         for (int j = 0; j < 10; j++)
//     //         {
//     //             _arenaArray[i, j] = "_";
//     //         }
//     //     }

//     //     string[,] ArrayData = new string[10, 10];
//     //     string[,] ArrayData2 = new string[10, 10];

//     //     Array.Copy(_arenaArray, ArrayData, _arenaArray.Length);
//     //     Array.Copy(_arenaArray, ArrayData2, _arenaArray.Length);

//     //     _hitPlayerInArena.Add(1, ArrayData);
//     //     _hitPlayerInArena.Add(2, ArrayData2);
//     // }

//     // //display hit arena information
//     // public void UpdateHitArena()
//     // {
//     //     Array.Copy(_hitPlayerInArena[_activePlayer], _arenaArray, _hitPlayerInArena[_activePlayer].Length);
//     // }

//     // //Coontrolling player turn
//     // public int TurnControl()
//     // {
//     //     if (_activePlayer == 1) { return _activePlayer = 2; }
//     //     else { return _activePlayer = 1; };
//     // }

//     // //Add ship to arena
//     public Data ValidationInShip(string DataInput)
//     {
//         Coordinate coordinate = new();
//         Data data = new();

//         string[] Data = DataInput.Split(" ");
//         if (Data.Count() != 3 || Data[1].Split(",").Count() != 2)
//         {
//             data.Message = "Data Invalid";
//             data.State = false;
//             return data;
//         }
//         if (ValidateListShipMenu(Data[0]))
//         {
//             data.Message = "Ship not found";
//             data.State = false;
//             return data;
//         };

//         string[] Coor = Data[1].Split(",");
//         if (!int.TryParse(Coor[0], out int x) || !int.TryParse(Coor[1], out int y))
//         {
//             data.Message = "Coordinate Invalid";
//             data.State = false;
//             return data;
//         }
//         if (ValidationOutShip(Data[0], Coor[0], Coor[1], Data[2]))
//         {
//             data.Message = "Out of range";
//             data.State = false;
//             return data;
//         }

//         coordinate.SetValue(int.Parse(Coor[0]), int.Parse(Coor[1]));
//         Data validShip = ValidateAddToMap(Data[0].ToUpper(), coordinate, Data[2].ToUpper());
//         if (validShip.State)
//         {
//             data.Message = validShip.Message;
//             data.State = false;
//             return data;
//         }

//         bool CheckRotate = AddToMap(Data[0].ToUpper(), coordinate, Data[2].ToUpper());
//         if (CheckRotate)
//         {
//             data.Message = "Data rotate Invalid";
//             data.State = false;
//             return data;
//         }

//         return new Data()
//         {
//             Message = "succed.",
//             State = true
//         };
//     }

//     // //Check out of arena or not
//     public bool ValidationOutShip(string key, string x, string y, string rotate)
//     {
//         Dictionary<string, IShip> Ship = _ships;
//         int ShipSize = Ship[key.ToUpper()]._shipSize;

//         if (rotate.ToUpper() == "H")
//         {
//             return ShipSize + int.Parse(y) - 1 > 10;
//         }
//         else if (rotate.ToUpper() == "V")
//         {
//             return ShipSize + int.Parse(x) - 1 > 10;
//         }
//         else
//         {
//             return false;
//         }
//     }

//     //Check list ship
//     public bool ValidateListShipMenu(string data)
//     {
//         bool state = true;
//         foreach (KeyValuePair<string, string> ship in _listShipMenu)
//         {
//             if (ship.Key == data.ToUpper())
//             {
//                 state = false;
//             }
//         }
//         return state;
//     }

//     // //Validation for input ship to arena
//     public Data ValidateAddToMap(string key, Coordinate coor, string rotate)
//     {
//         Data data = new();
//         Dictionary<string, IShip> listShip = _listShipsPlayer[_activePlayer];
//         int x = coor.GetValueX();
//         int y = coor.GetValueY();

//         if (rotate == "V")
//         {
//             for (int i = 0; i < listShip[key]._shipSize; i++)
//             {
//                 if (_arenaArray[i + x - 1, y - 1] != "_")
//                 {
//                     data.Message = $"{listShip[_arenaArray[i + x - 1, y - 1]]} here, Reposition your ship!";
//                     data.State = true;
//                     break;
//                 }
//             }
//             return data;
//         }
//         else if (rotate == "H")
//         {
//             for (int i = 0; i < listShip[key]._shipSize; i++)
//             {
//                 if (_arenaArray[x - 1, i + y - 1] != "_")
//                 {
//                     data.Message = $"{listShip[_arenaArray[x - 1, i + y - 1]]} here, Reposition your ship!";
//                     data.State = true;
//                     break;
//                 }

//             }
//             return data;
//         }
//         else
//         {
//             return new Data()
//             {
//                 Message = "Invalid value rotate",
//                 State = true,
//             };
//         }
//     }

//     // //add ship to arena player


//     // //Display player ship position
//     // public void CopyShipPositionToMap()
//     // {
//     //     Array.Copy(_shipPlayerInArena[_activePlayer], _arenaArray, _arenaArray.Length);
//     // }

//     // //Displaying arena information in preparation mode
//     // public string[,] GetArenaArray()
//     // {
//     //     return _arenaArray;
//     // }

//     // //For display ship that should placed player on the arena
//     // public Data GetListShipData()
//     // {
//     //     return new Data()
//     //     {
//     //         PlayerList = _listPlayer,
//     //         ListShipMenu = _listShipMenu,
//     //         ActivePlayer = _activePlayer,
//     //     };
//     // }

//     // //Hit Enemy 
//     // public Data HitEnemy(int x, int y)
//     // {
//     //     int EnemyId;
//     //     Data data = new Data();
//     //     data.Message = "succed.";

//     //     if (_activePlayer == 1) EnemyId = 2;
//     //     else EnemyId = 1;

//     //     Dictionary<string, IShip> listShip = _listShipsPlayer[EnemyId];

//     //     string[,] shipCoor = _shipPlayerInArena[EnemyId];
//     //     string[,] blankCoor = _hitPlayerInArena[_activePlayer];
//     //     if (shipCoor[x - 1, y - 1] != "_" && shipCoor[x - 1, y - 1] != "H" && shipCoor[x - 1, y - 1] != "*")
//     //     {
//     //         Coordinate c = new();
//     //         c.SetValue(x - 1, y - 1);
//     //         listShip[shipCoor[x - 1, y - 1]]._shipCoordinates.RemoveAll(coor => coor.GetValueX() == x - 1 && coor.GetValueY() == y - 1);

//     //         if (listShip[shipCoor[x - 1, y - 1]]._shipCoordinates.Count() == 0)
//     //         {
//     //             data.Message = $" Opponent {listShip[shipCoor[x - 1, y - 1]]._shipName} ship Destroyed";
//     //         }

//     //         blankCoor[x - 1, y - 1] = "H";
//     //         shipCoor[x - 1, y - 1] = "H";
//     //         data.State = true;
//     //         _totalHitPlayer[EnemyId]--;

//     //         if (_totalHitPlayer[EnemyId] == 0)
//     //         {
//     //             data.Message += " \n  You Win!";
//     //             return data;
//     //         }
//     //     }
//     //     else if (shipCoor[x - 1, y - 1] == "_")
//     //     {
//     //         blankCoor[x - 1, y - 1] = "*";
//     //         shipCoor[x - 1, y - 1] = "*";
//     //         data.State = false;
//     //     }
//     //     else
//     //     {
//     //         data.Message = "Worng data";
//     //         return data;
//     //     }
//     //     _shipPlayerInArena[EnemyId] = shipCoor;
//     //     _hitPlayerInArena[_activePlayer] = blankCoor;

//     //     return data;
//     // }

//     // //save coordinates ship player
//     // public Data SaveCoordinates()
//     // {
//     //     string[,] ArrayData = new string[10, 10];
//     //     Array.Copy(_arenaArray, ArrayData, _arenaArray.Length);
//     //     _shipPlayerInArena.Add(_activePlayer, ArrayData);

//     //     AddTotalHitPoints();

//     //     return new Data()
//     //     {
//     //         Message = "Data Saved!",
//     //     };
//     // }
//     // //Validate Hit input
//     // public bool ValidateHitInput(string input)
//     // {
//     //     string[] DataInput = input.Split("¼");
//     //     if (input.Length != 3 || DataInput.Length != 2)
//     //     {
//     //         return true;
//     //     }
//     //     else if (!int.TryParse(DataInput[0], out int data))
//     //     {
//     //         return true;
//     //     }
//     //     else
//     //     { return false; }
//     // }
// }