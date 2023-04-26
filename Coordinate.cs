using System.Xml.Serialization;
public class Coordinate
{
    private int _x;
    private int _y;

    public void SetValue(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public int GetValueX()
    {
        return _x;
    }

    public int GetValueY()
    {
        return _y;
    }

}