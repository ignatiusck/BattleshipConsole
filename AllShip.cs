class Battleship : IShip
{
    public string ShipName { get; set; }
    public int ShipSize { get; set; }
    public List<Coordinate> ShipCoordinates { get; set; }


    public Battleship()
    {
        ShipName = "Battleship";
        ShipSize = 4;
        Coordinate coordinate = new();
        List<Coordinate> ListCoors = new();
        ShipCoordinates = ListCoors;

    }
}


class Submarine : IShip
{
    public string ShipName { get; set; }
    public int ShipSize { get; set; }
    public List<Coordinate> ShipCoordinates { get; set; }


    public Submarine()
    {
        ShipName = "Submarine";
        ShipSize = 3;
        Coordinate coordinate = new();
        List<Coordinate> ListCoors = new();
        ShipCoordinates = ListCoors;

    }
}

class Carrier : IShip
{
    public string ShipName { get; set; }
    public int ShipSize { get; set; }
    public List<Coordinate> ShipCoordinates { get; set; }


    public Carrier()
    {
        ShipName = "Carrier";
        ShipSize = 5;
        Coordinate coordinate = new();
        List<Coordinate> ListCoors = new();
        ShipCoordinates = ListCoors;

    }
}

class Destroyer : IShip
{
    public string ShipName { get; set; }
    public int ShipSize { get; set; }
    public List<Coordinate> ShipCoordinates { get; set; }
    public bool IsSink { get; set; }

    public Destroyer()
    {
        ShipName = "Destroyer";
        ShipSize = 2;
        Coordinate coordinate = new();
        List<Coordinate> ListCoors = new();
        ShipCoordinates = ListCoors;

    }
}

class Cruiser : IShip
{
    public string ShipName { get; set; }
    public int ShipSize { get; set; }
    public List<Coordinate> ShipCoordinates { get; set; }


    public Cruiser()
    {
        ShipName = "Cruise";
        ShipSize = 3;
        Coordinate coordinate = new();
        List<Coordinate> ListCoors = new();
        ShipCoordinates = ListCoors;

    }
}