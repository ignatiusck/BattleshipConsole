class Battleship : IShip
{
    private string _shipName;
    private int _shipSize;
    private List<Coordinate> _shipCoordinates;

    public string ShipName { get => _shipName; set => _shipName = value; }
    public int ShipSize { get => _shipSize; set => _shipSize = value; }
    public List<Coordinate> ShipCoordinates { get => _shipCoordinates; set => _shipCoordinates = value; }

    public Battleship()
    {
        _shipName = "Battleship";
        _shipSize = 4;
        _shipCoordinates = new List<Coordinate>();

    }
}