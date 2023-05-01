using System.Xml.Serialization;
public class Coordinate : ICoordinate
{
    private int _x;
    private int _y;

    public int x { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }
}