using System.Xml.Serialization;
class Coordinate
{
    private int _x;
    private int _y;
    private bool? _isGotHit;

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