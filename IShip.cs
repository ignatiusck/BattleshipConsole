public interface IShip
{
    string ShipName { get; set; }
    int ShipSize { get; set; }
    List<Coordinate> ShipCoordinates { get; set; }
    bool IsSink { get; set; }
}

