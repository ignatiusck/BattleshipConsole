using Components;

namespace Pages
{
    class Page
    {
        private Component component = new();

        public string Home()
        {
            return
                component.Header("    BATTLESHIP    ") +
                component.WriteSpace(2) +
                component.BodyHome();
        }

        public string DataNotFound()
        {
            return
                component.Header("    BATTLESHIP    ") +
                component.WriteSpace(4) +
                component.BodyDataNotFound() +
                component.WriteSpace(4);

        }

        public string ListLoadData(IList<SaveDb> ListData)
        {
            string List = "List auto save data : \n\n ID           Time \n";
            foreach (SaveDb Data in ListData)
            {
                List += AddColor.Message($"[ {Data.Id} ]", ConsoleColor.Yellow) +
                "  " +
                Data.Time + "\n";
            }

            return
                component.Header("    BATTLESHIP    ") +
                List +
                "\nSelect ID : ";
        }

        public string CreatePlayer(int ActivePlayer)
        {
            return
                component.Header("    BATTLESHIP    ") +
                component.BodyInputName(ActivePlayer);
        }

        public string Transition(bool Transition, bool LoadData, string PlayerName)
        {

            string Title = Transition ? "PREPARATION PHASE " : "   BATTLE PHASE   ";
            return
                component.Header(Title) +
                component.WriteSpace(1) +
                component.BodyTransition(Transition, LoadData, PlayerName);
        }

        public string PreparationMap(IDictionary<string, Ship> ListShipMenu, string PlayerName, string[,] ArenaMap)
        {
            return
                component.BodyArenaMap(ArenaMap) +
                component.WriteSpace(1) +
                component.BodyTurnControl(true, PlayerName) +
                component.BodyListShipMenu(ListShipMenu) +
                component.WriteSpace(1) +
                component.BodyInputShip();
        }

        public string BattleMap(string PlayerName, string[,] ArenaMap)
        {
            return
                component.BodyArenaMap(ArenaMap) +
                component.WriteSpace(1) +
                component.BodyTurnControl(false, PlayerName) +
                component.BodyInputHit();
        }

        public string ShipPosition(string[,] ArenaMap)
        {
            return
                component.BodyArenaMap(ArenaMap) +
                component.WriteSpace(1) +
                component.BodyShipPosition();
        }

        public string PlayerWinner(bool state, string PlayerName)
        {
            return
                component.Header("    BATTLESHIP    ") +
                component.WriteSpace(3) +
                component.BodyWinner(state, PlayerName);
        }

        public string HitResult(bool Status, string Coor, string[,] ArenaMap)
        {
            string[] Data = Coor.Split("¼");
            Coor = Data[0] + "," + Data[1];
            return
                component.BodyArenaMap(ArenaMap) +
                component.WriteSpace(1) +
                component.BodyResult(Status, Coor) +
                component.WriteSpace(1);
        }
    }
}