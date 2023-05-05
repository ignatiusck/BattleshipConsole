using System.Security.Cryptography.X509Certificates;
public class Player : IPlayer, IPlayerBattleship
{
    private int _id;
    private string _name;

    public int Id { get => _id; }
    public string? Name { get => _name; }
    public string[,]? HitInOpponentArena { get; set; }
    public string[,]? ShipPlayerInArena { get; set; }
    public Dictionary<string, Ship>? ListShip { get; set; }

    public Player()
    {
    }
    public Player(int id, string name)
    {
        _id = id;
        _name = name;
    }
}