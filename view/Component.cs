using System.Drawing;

class Component
{
    private static readonly Arena? _arena = new();

    public string Header(string Title)
    {
        return
            "==============================================\n" +
            $"**            {Title}            **\n" +
            "==============================================\n";
    }

    public string BodyHome()
    {
        return
            "                 MULTIPLAYER \n" +
            "          Press Enter to continue...";
    }

    public string BodyInputName(int ActivePlayer)
    {
        return
            $"Enter your name (Player {ActivePlayer}) : ";
    }

    public string BodyTransition(bool Preparation, string PlayerName)
    {
        string View = Preparation ? "Attack your Opponent!!" : "Place all your ship in the Arena.";
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
            Map += $"   {k}";
        }
        Map += "\n";
        for (int i = 1; i <= _arena.ArenaSize.Height; i++)
        {
            if (i == 10)
            {
                Map += $" {i}";
            }
            else
            {
                Map += $"  {i}";
            }

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

    public string BodyListShipMenu(IDictionary<string, IShip> ListShipMenu)
    {
        string ShipMenu = "";
        foreach (KeyValuePair<string, IShip> ship in ListShipMenu)
        {
            ShipMenu += $"[{ship.Key}]   {ship.Value.ShipSize}   {ship.Value.ShipName}\n";
        }

        return
            "\nKEY  Size    NAME \n" + ShipMenu;
    }

    public string BodyTurnControl(bool Preparation, string PlayerName)
    {
        string View = Preparation ? "List Ship : " : "            ";
        PlayerName = AddColor.Message(PlayerName, ConsoleColor.Yellow);
        return
            View + "              Your Turn, " + PlayerName;
    }

    public string BodyInputShip()
    {
        return
            "Input : 'KEY Coordinat H/V' Example : 'B 1,1 H' \n" +
            "Place your ship : ";
    }

    public string BodyInputHit()
    {
        return
            " Press 'Home' to see your ship positions \n" +
            " Input : 'y,x' Example : '3,2' \n" +
            " Hit Enemy : ";
    }

    public string BodyShipPosition()
    {
        return
            AddColor.Message(" Your Ship Position        Will close in 3s. ", ConsoleColor.Yellow);
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