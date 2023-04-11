using System.Drawing;
class Arena
{
    private Size _arenaSize = new();
    private List<Coordinate> _arenaCoordinates;

    public Arena()
    {
        _arenaSize.Height = 10;
        _arenaSize.Width = 10;
    }

    public Size GetArenaSize()
    {
        return _arenaSize;
    }
}