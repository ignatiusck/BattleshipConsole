public interface IPlayerBattleship
{
    Dictionary<string, IShip> ListShip { get; set; }
    string[,] HitInOpponentArena { get; set; }
    string[,] ShipPlayerInArena { get; set; }
}