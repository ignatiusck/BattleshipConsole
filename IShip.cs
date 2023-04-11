interface IShip
{
    public string ShipName { get; set; }
    public int ShipSize { get; set; }
    public List<Coordinate> ShipCoordinates { get; set; }
    public bool IsSink { get; set; }
}