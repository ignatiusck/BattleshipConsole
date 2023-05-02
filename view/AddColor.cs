static class AddColor
{
    private static readonly Dictionary<ConsoleColor, string> _colorMap = new()
    {
        {ConsoleColor.Black, "\u001b[30m"},
        {ConsoleColor.Red, "\u001b[31m"},
        {ConsoleColor.Green, "\u001b[32m"},
        {ConsoleColor.Yellow, "\u001b[33m"},
        {ConsoleColor.Blue, "\u001b[34m"},
        {ConsoleColor.Magenta, "\u001b[35m"},
        {ConsoleColor.Cyan, "\u001b[36m"},
        {ConsoleColor.White, "\u001b[37m"},
    };

    public static string Message(string Message, ConsoleColor Color)
    {
        return
            $"{_colorMap[Color]}" + Message + "\u001b[0m";
    }

    public static void Succed(string Message, int Delay)
    {

    }
}