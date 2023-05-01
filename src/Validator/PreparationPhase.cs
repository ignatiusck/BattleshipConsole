class ValidatorPreparationPhase
{
    public bool IsInputValid(string Input)
    {
        string[] Data = Input.Split(" ");
        return Data.Count() != 3 || Data[1].Split(",").Count() != 2;
    }

    public bool IsShipNotValid(string Input, Dictionary<string, string> ListShipMenu)
    {

        foreach (KeyValuePair<string, string> ship in ListShipMenu)
        {
            if (ship.Key == Input.ToUpper())
            {
                return false;
            }
        }
        return true;
    }

    public bool IsCoordinateValid(string Input)
    {
        string[] Data = Input.Split(" ");
        string[] Coor = Data[1].Split(",");
        return !int.TryParse(Coor[0], out int x) && !int.TryParse(Coor[1], out int y);
    }

    public bool IsOutOfRange(string Input, Dictionary<string, IShip> ListMenuShip)
    {
        string[] Data = Input.Split(" ");
        string[] Coor = Data[1].Split(",");
        int X = int.Parse(Coor[0]) + ListMenuShip[Data[0]].ShipSize;
        int Y = int.Parse(Coor[1]) + ListMenuShip[Data[0]].ShipSize;
        return X > 10 || Y > 10;
    }

    public bool IsRotateNotValid(string Input)
    {
        string[] Data = Input.Split(" ");
        return
            !string.Equals(Data[2], "H", StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(Data[2], "V", StringComparison.OrdinalIgnoreCase)
        ;
    }

    public bool IsAnyShipHere(string Input, string[,] ArenaArray)
    {
        return default;
    }
}