using System;
namespace Components
{
    class Component
    {
        private static readonly Arena? _arena = new();

        public string Header(string Title)
        {
            return
                "=============================================\n" +
                $"**            {Title}           **\n" +
                "=============================================\n";
        }

        public string BodyHome()
        {
            return
                "                 MULTIPLAYER \n" +
                "  Game Menu : \n \n" +
                "    > Start New Game --- " +
                AddColor.Message("[ENTER]", ConsoleColor.Yellow) +
                " \n    > Continue Game  --- " +
                AddColor.Message("[HOME]", ConsoleColor.Yellow) + " \n \n \n \n \n" +
                "Press 'Enter' or 'Home' button to continue...";
        }

        public string BodyInputName(int ActivePlayer)
        {
            return
                $"Enter your name (Player {ActivePlayer}) : ";
        }

        public string BodyTransition(bool Preparation, string PlayerName)
        {
            string View = !Preparation ? "Attack your Opponent!!" : "Place all your ship in the Arena.";
            PlayerName = AddColor.Message(PlayerName, ConsoleColor.Yellow);
            return
                $" {View} \n" +
                " You play first, " +
                PlayerName + "\n \n \n \n" +
                "Press Enter to continue...";
        }

        public string BodyArenaMap(string[,] ArenaMap)
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
            return Map;
        }

        public string BodyListShipMenu(IDictionary<string, Ship> ListShipMenu)
        {
            string ShipMenu = "";
            foreach (KeyValuePair<string, Ship> ship in ListShipMenu)
            {
                ShipMenu += $"[{ship.Key}]   {ship.Value.ShipSize}   {ship.Value.ShipName}\n";
            }

            return
                "\nKEY  SIZE    NAME \n" + ShipMenu;
        }

        public string BodyTurnControl(bool Preparation, string PlayerName)
        {
            Arena arena = new();
            string View = Preparation ? "List Ship : " : "            ";
            PlayerName = AddColor.Message(PlayerName, ConsoleColor.Yellow);
            string Space = "";

            if (arena.ArenaSize.Width >= 10)
                for (int i = 10; i <= arena.ArenaSize.Width; i++) Space += "    ";

            return
                View + "            " + Space + "Your Turn, " + PlayerName;
        }

        public string BodyInputShip()
        {
            return
                "Input : " +
                AddColor.Message("'KEY Coordinat H/V'", ConsoleColor.Yellow) +
                " Example : " +
                AddColor.Message("'B 1,1 H' \n", ConsoleColor.Yellow) +
                "Place your ship : ";
        }

        public string BodyInputHit()
        {
            return
                " \n \n Press " +
                AddColor.Message("'Home'", ConsoleColor.Yellow) +
                " to see your ship positions \n" +
                " Input : " +
                AddColor.Message("'y,x'", ConsoleColor.Yellow) +
                " Example : " +
                AddColor.Message("'3,2'", ConsoleColor.Yellow) + " \n" +
                " Hit Enemy : ";
        }

        public string BodyShipPosition()
        {
            return
                AddColor.Message(" Your Ship Position        Will close in 3s. ", ConsoleColor.Yellow);
        }

        public string BodyResult(bool Status, string Coor)
        {
            Coor = AddColor.Message(Coor, ConsoleColor.Yellow);
            string Result = Status ?
                AddColor.Message("Hit!!", ConsoleColor.Green) :
                AddColor.Message("Miss", ConsoleColor.Red);
            return
                $"  Coordinate : {Coor}            Result: {Result}";

        }

        public string BodyWinner(bool state, string PlayerName)
        {
            string Win = state ? AddColor.Message("WINNER", ConsoleColor.Black) : AddColor.Message("WINNER", ConsoleColor.Yellow);
            return
                Win + "\n" +
                PlayerName + "\n \n \n" +
                "Press Enter to exit...";
        }

        public string WriteSpace(int count)
        {
            string Space = "";
            for (int i = 0; i < count; i++)
            {
                Space += "\n";
            }
            return Space;
        }
    }
}