static class Program
{
    public static void Main(string[] args)
    {
        Player player_1 = new();
        Player player_2 = new();
        List<Player> ListPlayer = new() { player_1, player_2 };
        GameController game = new(ListPlayer);

        Coordinate coordinate = new();
        string DataInput = "";
        string[] Data;
        string[] Coor;
        bool turn = true;

        game.GameTitle();
        Console.ReadKey();
        while (true)
        {
            game.ResetShipCoor();
            while (turn == true)
            {
                Console.Clear();
                game.DisplayArena();
                turn = game.DisplayShip();
                if (turn == false) break;

                Console.Write("\n" + "Place your ship : ");
                DataInput = Console.ReadLine();
                Data = DataInput.Split(" ");
                Coor = Data[1].Split(",");
                coordinate.SetValue(int.Parse(Coor[0]), int.Parse(Coor[1]));
                game.AddToMap(Data[0].ToUpper(), coordinate, Data[2].ToUpper());

            }
            game.TurnControl();
            turn = true;
            Console.ReadKey();
        }


        //Console.Clear();
        Console.ReadKey();

        // Coordinate coordinate = new();
        // coordinate.SetValue(2, 4);
        // game.AddToMap("R", coordinate, "V");
        // game.DisplayArena();
        // game.DisplayShip();

        // coordinate.SetValue(6, 5);
        // game.AddToMap("D", coordinate, "H");
        // game.DisplayArena();
        // game.DisplayShip();
    }
}