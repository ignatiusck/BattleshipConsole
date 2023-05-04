public interface IPlayerBattleship
{
    Dictionary<string, Ship> ListShip { get; set; }
    string[,] HitInOpponentArena { get; set; }
    string[,] ShipPlayerInArena { get; set; }
}