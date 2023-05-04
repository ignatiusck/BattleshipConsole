interface IShip
{
    public string ShipName { get ; }
    public int ShipSize { get ; }
    public List<Coordinate> ShipCoordinates { get; set; }
}