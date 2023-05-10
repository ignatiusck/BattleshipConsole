using System.Xml.Serialization;
public class Coordinate : ICoordinate, IShipPack
{
    private int _x;
    private int _y;
    private readonly int _size = 1;

    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }

    public int CountHitPoints()
    {
        return _size;
    }
}