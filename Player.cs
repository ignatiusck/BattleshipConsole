class Player : IPlayer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, IShip> ListShip { get; set; }
}