class Battleship : IShip
{
    public string ShipName { get; set; }
    public int ShipSize { get; set; }
    public List<Coordinate> ShipCoordinates { get; set; }
    public bool IsSink { get; set; }

    public Battleship(List<Coordinate> ListCoordinate)
    {
        ShipName = "Battleship";
        ShipSize = 4;
        ShipCoordinates = ListCoordinate;
        IsSink = false;
    }
}


class Submarine : IShip
{
    public string ShipName { get; set; }
    public int ShipSize { get; set; }
    public List<Coordinate> ShipCoordinates { get; set; }
    public bool IsSink { get; set; }

    public Submarine(List<Coordinate> ListCoordinate)
    {
        ShipName = "Submarine";
        ShipSize = 3;
        ShipCoordinates = ListCoordinate;
        IsSink = false;
    }
}

class Carrier : IShip
{
    public string ShipName { get; set; }
    public int ShipSize { get; set; }
    public List<Coordinate> ShipCoordinates { get; set; }
    public bool IsSink { get; set; }

    public Carrier(List<Coordinate> ListCoordinate)
    {
        ShipName = "Carrier";
        ShipSize = 5;
        ShipCoordinates = ListCoordinate;
        IsSink = false;
    }
}

class Destroyer : IShip
{
    public string ShipName { get; set; }
    public int ShipSize { get; set; }
    public List<Coordinate> ShipCoordinates { get; set; }
    public bool IsSink { get; set; }

    public Destroyer(List<Coordinate> ListCoordinate)
    {
        ShipName = "Destroyer";
        ShipSize = 2;
        ShipCoordinates = ListCoordinate;
        IsSink = false;
    }
}

class Cruiser : IShip
{
    public string ShipName { get; set; }
    public int ShipSize { get; set; }
    public List<Coordinate> ShipCoordinates { get; set; }
    public bool IsSink { get; set; }

    public Cruiser(List<Coordinate> ListCoordinate)
    {
        ShipName = "Cruise";
        ShipSize = 3;
        ShipCoordinates = ListCoordinate;
        IsSink = false;
    }
}