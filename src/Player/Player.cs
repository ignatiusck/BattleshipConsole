public class Player : IPlayer, IPlayerBattleship
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string[,] HitInOpponentArena { get; set; }
    public Dictionary<string, IShip> ListShip { get; set; }
}