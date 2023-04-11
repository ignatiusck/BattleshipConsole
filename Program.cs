class Program
{
    public static void Main(string[] args)
    {
        GameController game = new();
        game.ResetShipCoor();
        game.DisplayArena();
        game.DisplayShip();

        Coordinate coordinate = new();
        coordinate.SetValue(2, 4);
        game.AddToMap("R", coordinate, "V");
        game.DisplayArena();
        game.DisplayShip();

        coordinate.SetValue(6, 5);
        game.AddToMap("D", coordinate, "H");
        game.DisplayArena();
        game.DisplayShip();
    }
}