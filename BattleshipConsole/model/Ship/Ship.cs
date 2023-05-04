public class Ship : IShip
{
    private string _shipName;
    private int _shipSize;
    public string ShipName { get => _shipName; set => _shipName = value; }
    public int ShipSize { get => _shipSize; set => _shipSize = value; }
    public List<Coordinate> ShipCoordinates { get; set; }

    public Ship(string name, int size)
    {
        _shipName = name;
        _shipSize = size;
        ShipCoordinates = new List<Coordinate>();
    }
}