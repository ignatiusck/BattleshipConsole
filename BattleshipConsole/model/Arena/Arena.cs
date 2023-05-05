using System.Drawing;
using Microsoft.EntityFrameworkCore;

[Keyless]
public class Arena : IArena
{
    private Size _arenaSize;

    public Size ArenaSize { get => _arenaSize; }

    public Arena()
    {
        _arenaSize = new Size()
        {
            Height = 10,
            Width = 10,
        };
    }
}