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
                component.WriteSpace(5) +
                component.BodyHome() +
                component.WriteSpace(6);
        }

        public string CreatePlayer(int ActivePlayer)
        {
            return
                component.Header("    BATTLESHIP    ") +
                component.BodyInputName(ActivePlayer);
        }

        public string Transition(bool Transition, string PlayerName)
        {

            string Title = Transition ? "PREPARATION PHASE " : "   BATTLE PHASE   ";
            return
                component.Header(Title) +
                component.WriteSpace(3) +
                component.BodyTransition(Transition, PlayerName);
        }

        public string PreparationMap(IDictionary<string, IShip> ListShipMenu, string PlayerName, string[,] ArenaMap)
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
                component.BodyWinner(true, PlayerName);
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