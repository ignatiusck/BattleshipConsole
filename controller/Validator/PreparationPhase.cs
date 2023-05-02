class ValidatorPreparationPhase
{
    public bool IsInputValid(string Input)
    {
        string[] Data = Input.Split(" ");
        return Data.Count() != 3 || Data[1].Split(",").Count() != 2;
    }

    public bool IsShipNotValid(string Input, IDictionary<string, IShip> ListShipMenu)
    {
        string[] Data = Input.Split(" ");
        return !ListShipMenu.Any(ship => ship.Key == Data[0].ToUpper());
    }

    public bool IsCoordinateValid(string Input)
    {
        string[] Data = Input.Split(" ");
        string[] Coor = Data[1].Split(",");
        return !int.TryParse(Coor[0], out int x) && !int.TryParse(Coor[1], out int y);
    }

    public bool IsOutOfRange(string Input, IDictionary<string, IShip> ListMenuShip)
    {
        string[] Data = Input.Split(" ");
        string[] Coor = Data[1].Split(",");
        string KeyShip = Data[0].ToUpper();
        int X = int.Parse(Coor[0]) + ListMenuShip[KeyShip].ShipSize;
        int Y = int.Parse(Coor[1]) + ListMenuShip[KeyShip].ShipSize;
        return X > 10 || Y > 10;
    }

    public bool IsRotateNotValid(string Input)
    {
        string[] Data = Input.Split(" ");
        string Rotate = Data[2].ToUpper();
        return !(Rotate == "H" || Rotate == "V");
    }

    public IData IsAnyShipHere(string Input, string[,] ArenaMap)
    {
        string[] Data = Input.Split(" ");
        string[] Coor = Data[1].Split(",");
        string Rotate = Data[2].ToUpper();
        int X = int.Parse(Coor[0]) - 1;
        int Y = int.Parse(Coor[1]) - 1;

        int Length = (Rotate.ToUpper()! == "V") ? ArenaMap.GetLength(0) : ArenaMap.GetLength(1);
        Console.WriteLine(ArenaMap.GetLength(0) + ArenaMap.GetLength(1));
        Console.ReadKey();

        for (int i = 0; i < Length; i++)
        {
            if (ArenaMap[X, Y] != "_")
            {
                return new Rejected(ArenaMap[X, Y]);
            }
            _ = (Rotate.ToUpper()! == "H") ? X++ : Y++;
        }

        return new Accepted();
    }
}