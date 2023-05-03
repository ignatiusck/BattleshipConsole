namespace Validators
{
    class ValidatorHit
    {
        public bool IsInputValid(string Input)
        {
            string[] Coor = Input.Split("¼");
            return
                Coor.Length == 2 &&
                int.TryParse(Coor[0], out _) &&
                int.TryParse(Coor[1], out _);
        }

        public bool IsOutOfRange(string Input, IArena arena)
        {
            string[] Coor = Input.Split("¼");
            return
                int.Parse(Coor[0]) > arena.ArenaSize.Height ||
                int.Parse(Coor[1]) > arena.ArenaSize.Width;
        }

        public bool IsHitedBefore(string Input, string[,] ArenaMap)
        {
            string[] Coor = Input.Split("¼");
            int X = int.Parse(Coor[0]) - 1;
            int Y = int.Parse(Coor[1]) - 1;
            return ArenaMap[X, Y] != "_";
        }
    }
}