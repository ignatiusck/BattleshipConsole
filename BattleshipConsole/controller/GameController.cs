using Newtonsoft.Json;
using Helpers;
using Validators;
using MainLogger;

namespace MainGameController
{
    public class GameController
    {
        private List<Player>? _listPlayerInfo;
        private Arena? _arena;
        private string[,] _arenaArray;
        private int _activePlayer;
        private ValidatorCreatePlayer _vPlayer;
        private ValidatorPreparationPhase _vPreparation;
        private ValidatorHit _vHit;
        private Logger<GameController> _logger;
        private List<ShipPack> _countHP;
        private int _gameId;

        //Create game with player
        public GameController()
        {
            _arena = new();
            _activePlayer = 1;
            _listPlayerInfo = new();
            _countHP = new() { new ShipPack(), new ShipPack() };

            _arenaArray = new string[_arena!.ArenaSize.Height, _arena.ArenaSize.Width];
            _vPlayer = new();
            _vPreparation = new();
            _vHit = new();

            _logger = new();
        }

        public GameController(List<Player> ListPlayerInfo, int ActivePlayer, int GameId, string HitData)
        {
            _arena = new();
            _activePlayer = ActivePlayer;
            _listPlayerInfo = ListPlayerInfo;
            _gameId = GameId;

            string[] DataHit = HitData.Split(" ");
            _countHP = new() {
                new ShipPack() { TotalHP =  int.Parse(DataHit[0]), InGameHp = int.Parse(DataHit[1])},
                new ShipPack() { TotalHP =  int.Parse(DataHit[2]), InGameHp = int.Parse(DataHit[3])}};

            _arenaArray = new string[_arena!.ArenaSize.Height, _arena.ArenaSize.Width];
            _vPlayer = new();
            _vPreparation = new();
            _vHit = new();

            _logger = new();
        }

        public IData SaveGame()
        {
            using (GameDbContext db = new())
            {
                SaveGame GetData = db.SaveGames!.Find(_gameId)!;
                if (GetData == null)
                {
                    SaveGame Data = new()
                    {
                        ActivePlayer = _activePlayer,
                        CountHP = $"{_countHP[0].TotalHP} {_countHP[0].InGameHp} {_countHP[1].TotalHP} {_countHP[1].InGameHp}",
                        Time = DateTime.Now.ToString(),
                    };
                    db.SaveGames.Add(Data);

                    foreach (Player P in _listPlayerInfo!)
                    {
                        Player player = new()
                        {
                            Name = P.Name,
                            IdInGame = P.IdInGame,
                            SerializedHit = JsonConvert.SerializeObject(P.HitInOpponentArena),
                            SerializedShip = JsonConvert.SerializeObject(P.HitInOpponentArena),
                        };
                        db.Players!.Add(player);

                        GamesPlayers GP = new()
                        {
                            SaveGame = Data,
                            Player = player,
                        };
                        db.GamesPlayers!.Add(GP);

                        foreach (KeyValuePair<string, Ship> ship in P.ListShip!)
                        {
                            ship.Value.Player = player;
                            ship.Value.Key = ship.Key;
                            ship.Value.SerializedCoor = JsonConvert.SerializeObject(ship.Value.ShipCoordinates);
                            db.Ships!.Add(ship.Value);

                        }
                    }
                    IData Result = db.SaveData();
                    _gameId = Data.Id;

                    return Result;
                }

                GetData.ActivePlayer = _activePlayer;
                GetData.CountHP = $"{_countHP[0].TotalHP} {_countHP[0].InGameHp} {_countHP[1].TotalHP} {_countHP[1].InGameHp}";
                GetData.Time = DateTime.Now.ToString();

                foreach (GamesPlayers GP in db.GamesPlayers!.Where(gp => gp.SaveGameId == GetData.Id).ToList())
                {
                    Player? GetPlayer = db.Players!.Find(GP.PlayerId);
                    GetPlayer!.SerializedHit = JsonConvert.SerializeObject(_listPlayerInfo![GetPlayer.IdInGame - 1].HitInOpponentArena);
                    GetPlayer.SerializedShip = JsonConvert.SerializeObject(_listPlayerInfo[GetPlayer.IdInGame - 1].ShipPlayerInArena);

                    foreach (Ship ship in db.Ships!.Where(Ship => Ship.PlayerId == GetPlayer.Id))
                    {
                        ship.SerializedCoor = JsonConvert.SerializeObject(_listPlayerInfo[GetPlayer.IdInGame - 1].ListShip![ship.Key].ShipCoordinates);
                    }
                }
                return db.SaveData();
            }
        }

        public void ClearGameData()
        {
            using (GameDbContext db = new())
            {
                SaveGame GetData = db.SaveGames!.Find(_gameId)!;
                foreach (GamesPlayers GP in db.GamesPlayers!.Where(gp => gp.SaveGameId == GetData.Id).ToList())
                {
                    Player? GetPlayer = db.Players!.Find(GP.PlayerId);
                    foreach (Ship ship in db.Ships!.Where(Ship => Ship.PlayerId == GetPlayer!.Id))
                    {
                        db.Ships!.Remove(ship);
                    }
                    db.Players.Remove(GetPlayer!);
                    db.GamesPlayers!.Remove(GP);
                }
                db.SaveGames.Remove(GetData);
                db.SaveChanges();
            }
        }

        //create ship packet
        private Dictionary<string, Ship> CreateShipPack()
        {
            return new Dictionary<string, Ship>()
            {
                ["S"] = new Ship("Submarine", 3),
                ["B"] = new Ship("Battleship", 4),
                // ["C"] = new Ship("Cruiser", 3),
                // ["D"] = new Ship("Destroyer", 3),
                // ["R"] = new new Ship("Carrier", 4),
            };
        }

        public bool AddPlayer(string PlayerName)
        {
            Player player = new(_listPlayerInfo!.Count + 1, PlayerName)
            {
                ListShip = CreateShipPack(),
            };

            _listPlayerInfo.Add(player);
            if (_listPlayerInfo.Count == 2)
            {
                SetPreparationArenaPlayer();
            }
            _logger.Message("Player added.", LogLevel.Info);

            return _listPlayerInfo.Count < 2;
        }

        private IDictionary<string, Ship> GetListPlayerShip()
        {
            IPlayerBattleship player =
                _listPlayerInfo!.Find(player => player.IdInGame == _activePlayer)!;

            return player.ListShip;
        }

        public string[,] GetShipPlayerInArena()
        {
            IPlayerBattleship player =
                _listPlayerInfo!.Find(player => player.IdInGame == _activePlayer)!;

            return player.ShipPlayerInArena;
        }

        public IPlayerBattleship GetPlayerDataInGame()
        {
            return _listPlayerInfo!.Find(player => player.IdInGame == _activePlayer)!;
        }

        public IPlayerBattleship GetPlayerDataInGame(int Opponent)
        {
            return _listPlayerInfo!.Find(player => player.IdInGame == Opponent)!;
        }

        public IPlayer GetPlayerActive()
        {
            return _listPlayerInfo!.Find(player => player.IdInGame == _activePlayer)!;
        }

        public void ResetShipCoor()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
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
                string[,] ShipPosition = new string[_arena!.ArenaSize.Height, _arena.ArenaSize.Width];
                string[,] HitPosition = new string[_arena!.ArenaSize.Height, _arena.ArenaSize.Width];
                Array.Copy(_arenaArray, ShipPosition, _arenaArray.Length);
                Array.Copy(_arenaArray, HitPosition, _arenaArray.Length);
                player.ShipPlayerInArena = ShipPosition;
                player.HitInOpponentArena = HitPosition;
            }
        }

        public IData GetHPPlayer()
        {
            int Opponent = (_activePlayer == 1 ? 2 : 1) - 1;
            float BarInt = _countHP[Opponent].InGameHp / (float)_countHP[Opponent].TotalHP * 10;
            int BarSpaceInt = 10 - (int)BarInt;

            string BarString = "";
            string BarSpace = "";
            for (int i = 0; i < BarInt; i++) BarString += "=";
            for (int i = 0; i < BarSpaceInt; i++) BarSpace += " ";

            return new Data($"Opponent HP: ({_countHP[Opponent].InGameHp}) {BarString}{BarSpace}", true);
        }

        public IDictionary<string, Ship> GetListShipInGame()
        {
            Dictionary<string, Ship> ListShipMenu = new();
            foreach (KeyValuePair<string, Ship> Ship in CreateShipPack())
            {
                ListShipMenu.Add(Ship.Key, Ship.Value);
            }
            return ListShipMenu;
        }

        public void AddShipToArena(string Inputposition, IDictionary<string, Ship> ListShipMenu)
        {
            IPlayerBattleship player =
                _listPlayerInfo!.Find(player => player.IdInGame == _activePlayer)!;

            string[] Data = Inputposition.Split(" ");
            string[] Coordinate = Data[1].Split(",");
            string Rotate = Data[2].ToUpper();
            string KeyShip = Data[0].ToUpper();
            int XCoor = int.Parse(Coordinate[0]) - 1;
            int YCoor = int.Parse(Coordinate[1]) - 1;

            ShipPack Part = new() { Name = KeyShip };
            for (int i = 0; i < player.ListShip[KeyShip].ShipSize; i++)
            {
                Coordinate Coor = new()
                {
                    X = XCoor,
                    Y = YCoor,
                };
                player.ShipPlayerInArena[Coor.X, Coor.Y] = KeyShip;
                player.ListShip[KeyShip].ShipCoordinates.Add(Coor);

                //Implement Composite Design Pattern
                Part.AddData(Coor);

                _ = (Rotate.ToUpper()! == "V") ? XCoor++ : YCoor++;
            }
            _countHP[_activePlayer - 1].AddData(Part);
            _countHP[_activePlayer - 1].InGameHp = _countHP[_activePlayer - 1].CountHitPoints();
            _countHP[_activePlayer - 1].TotalHP = _countHP[_activePlayer - 1].CountHitPoints();
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

        public IData ValidatorPreparation(string Input, IDictionary<string, Ship> ListShipMenu, string[,] ArenaMap)
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
            IData Data = _vPreparation.IsAnyShipHere(Input, ArenaMap, ListShipMenu);
            if (!Data.Status)
            {
                return new Rejected(
                    $"{GetListPlayerShip()[Data.Message]} here, Reposition your ship!"
                    );
            };
            return new Accepted();
        }

        public IData? ValidateInputCoorHit(string Input, string[,] ArenaMap)
        {
            if (!_vHit.IsInputValid(Input))
                return new Rejected("Invalid input.");
            if (_vHit.IsOutOfRange(Input, _arena!))
                return new Rejected("Coordinate out of range.");
            if (_vHit.IsHitedBefore(Input, ArenaMap))
                return new Rejected("Coordinate have been hit.");
            return new Accepted();
        }

        public IData HitOpponent(string Input)
        {
            string[] Coor = Input.Split("¼");
            int X = int.Parse(Coor[0]) - 1;
            int Y = int.Parse(Coor[1]) - 1;

            int Opponent = _activePlayer == 1 ? 2 : 1;
            string DestroyShip = "none";
            IPlayerBattleship Attacker = GetPlayerDataInGame();
            IPlayerBattleship Defender = GetPlayerDataInGame(Opponent);

            if (Defender.ShipPlayerInArena[X, Y] != "_")
            {
                Attacker.HitInOpponentArena[X, Y] = "H";

                string KeyShip = Defender.ShipPlayerInArena[X, Y];
                Defender.ListShip[KeyShip].ShipCoordinates
                    .RemoveAll(Coor => Coor.X == X && Coor.Y == Y);

                if (Defender.ListShip[KeyShip].ShipCoordinates.Count == 0)
                    DestroyShip = Defender.ListShip[KeyShip].ShipName;

                Defender.ShipPlayerInArena[X, Y] = "H";
                _countHP[Opponent - 1].InGameHp--;

                return new Data(DestroyShip, true);
            }
            Attacker.HitInOpponentArena[X, Y] = "*";
            Defender.ShipPlayerInArena[X, Y] = "*";
            return new Data("none", false);
        }

        public int TurnControl()
        {
            _activePlayer = _activePlayer == 1 ? 2 : 1;
            return 1;
        }

        public bool GetWinnerStatus()
        {
            int Opponent = _activePlayer == 1 ? 2 : 1;
            IPlayerBattleship Player = GetPlayerDataInGame(Opponent);
            int TotalShipCoor = 0;
            foreach (KeyValuePair<string, Ship> Ship in Player.ListShip)
            {
                TotalShipCoor += Ship.Value.ShipCoordinates.Count;
            }
            return TotalShipCoor == 0;
        }
    }
}