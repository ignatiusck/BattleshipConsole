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

        //Create game with player
        public GameController()
        {
            _arena = new();

            _arenaArray = new string[_arena!.ArenaSize.Height, _arena.ArenaSize.Width];
            _activePlayer = 1;
            _listPlayerInfo = new();

            _vPlayer = new();
            _vPreparation = new();
            _vHit = new();

            _logger = new();
        }

        public GameController(GameData Data)
        {
            _arena = Data.Arena;
            _activePlayer = Data.ActivePlayer;
            _listPlayerInfo = Data.ListPlayerInfo;

            _vPlayer = new();
            _vPreparation = new();
            _vHit = new();

            _logger = new();
        }

        public void SaveGame()
        {
            GameData Data = new()
            {
                ListPlayerInfo = _listPlayerInfo,
                Arena = _arena,
                ActivePlayer = _activePlayer,
            };

            using (StreamWriter Writer = new("Data.json"))
            using (JsonWriter JsonWriter = new JsonTextWriter(Writer))
            {
                JsonSerializer Serializer = new();
                Serializer.Serialize(JsonWriter, Data);
            }
        }

        //create ship packet
        private Dictionary<string, IShip> CreateShipPack()
        {
            return new Dictionary<string, IShip>()
            {
                ["S"] = new Submarine(),
                // ["B"] = new Battleship(),
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

        public IPlayerBattleship GetPlayerDataInGame()
        {
            return _listPlayerInfo!.Find(player => player.Id == _activePlayer)!;
        }

        public IPlayerBattleship GetPlayerDataInGame(int Opponent)
        {
            return _listPlayerInfo!.Find(player => player.Id == Opponent)!;
        }

        public IPlayer GetPlayerActive()
        {
            return _listPlayerInfo!.Find(player => player.Id == _activePlayer)!;
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
                string[,] ShipPosition = new string[_arena!.ArenaSize.Height, _arena.ArenaSize.Width];
                string[,] HitPosition = new string[_arena!.ArenaSize.Height, _arena.ArenaSize.Width];
                Array.Copy(_arenaArray, ShipPosition, _arenaArray.Length);
                Array.Copy(_arenaArray, HitPosition, _arenaArray.Length);
                player.ShipPlayerInArena = ShipPosition;
                player.HitInOpponentArena = HitPosition;
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

                _ = (Rotate.ToUpper()! == "V") ? XCoor++ : YCoor++;
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

        public IData ValidatorPreparation(string Input, IDictionary<string, IShip> ListShipMenu, string[,] ArenaMap)
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
                return new Rejected("coordinate hitted before.");
            return new Accepted();
        }

        public IData HitOpponent(string Input)
        {
            string[] Coor = Input.Split("¼");
            int X = int.Parse(Coor[0]) - 1;
            int Y = int.Parse(Coor[1]) - 1;

            int Opponent = _activePlayer == 1 ? 2 : 1;
            string DestroyShip = "";
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
                return new Data(DestroyShip, true);
            }
            Attacker.HitInOpponentArena[X, Y] = "*";
            Defender.ShipPlayerInArena[X, Y] = "*";
            return new Data("", false);
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
            foreach (KeyValuePair<string, IShip> Ship in Player.ListShip)
            {
                TotalShipCoor += Ship.Value.ShipCoordinates.Count;
            }
            return TotalShipCoor == 0;
        }
    }
}