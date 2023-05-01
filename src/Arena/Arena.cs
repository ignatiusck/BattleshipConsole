using System.Drawing;

class Arena : IArena
{
    private Size _arenaSize;

    public Size ArenaSize
    {
        get => _arenaSize;
        init
        {
            _arenaSize = new Size()
            {
                Height = 10,
                Width = 10,
            };
        }
    }
}