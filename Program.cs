static class Program
{
    public static void Main(string[] args)
    {
        Player player_1 = new();
        Player player_2 = new();
        List<Player> ListPlayer = new() { player_1, player_2 };
        GameController game = new(ListPlayer);
        Coordinate coordinate = new();

        // Console.SetWindowSize(40, 40);
        // Console.SetBufferSize(80, 80);

        string DataInput = "";
        string[] Data;
        string[] Coor;
        bool turn = true;
        bool start = true;
        int count = 1;

        game.GameHome();
        game.CreatePlayer();
        while (start == true)
        {
            game.ResetShipCoor();
            while (turn == true)
            {
                Console.Clear();
                game.DisplayName();
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
            game.SaveCoordinates();
            game.TurnControl();
            turn = true;
            if (count >= 2) start = false;
            count++;
        }
        // turn = true;
        // game.DisplayHitArena();
        // game.SaveCoordinates();
        // while (turn)
        // {
        //     game.DisplayShipPosition();
        //     Console.Write($"\n Hit enemy : ");
        //     string input = Console.ReadLine();
        //     string[] data = input.Split(",");
        //     game.HitEnemy(int.Parse(data[0]), int.Parse(data[1]));

        // }

        //Console.Clear();
        //Console.ReadKey();

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