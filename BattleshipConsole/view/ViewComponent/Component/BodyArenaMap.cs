namespace Components
{
    public class BodyArenaMap : IComponent
    {
        public string ComponentName { get => "Body Arena Map"; }
        private readonly Arena _arena = new();
        private string _bodyArenaMap;
        public BodyArenaMap(string[,] ArenaMap)
        {
            string Map = "";

            Map += "\n";
            Map += "  0  1";
            for (int k = 2; k <= _arena!.ArenaSize.Width; k++)
            {
                if (k <= 10) Map += $"   {k}";
                else Map += $"  {k}";
            }
            Map += "\n";
            for (int i = 1; i <= _arena.ArenaSize.Height; i++)
            {
                if (i >= 10) Map += $" {i}";
                else Map += $"  {i}";

                for (int j = 0; j < _arena.ArenaSize.Width; j++)
                {
                    Map += ArenaMap[i - 1, j] == "_" ?
                        $" [{ArenaMap[i - 1, j]}]" :
                        $" [{AddColor.Message(ArenaMap[i - 1, j], ConsoleColor.Yellow)}]";
                }
                Map += "\n";
            }
            _bodyArenaMap = Map;
        }

        public string Invoke()
        {
            return _bodyArenaMap;
        }

    }
}