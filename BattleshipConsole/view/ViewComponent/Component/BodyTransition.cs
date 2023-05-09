namespace Components
{
    public class BodyTransition : IComponent
    {
        public string ComponentName { get => "BodyTransition"; }
        private string _bodyTransition;

        public BodyTransition(bool Preparation, bool IsContinue, string PlayerName)
        {
            string View = !Preparation ? "Attack your Opponent!!" : "Place all your ship in the Arena.";
            string Continue = IsContinue ? "Game data loaded!" : " ";
            PlayerName = AddColor.Message(PlayerName, ConsoleColor.Yellow);
            _bodyTransition =
                $"              {Continue} \n \n \n" +
                $" {View} \n" +
                " You play first, " +
                PlayerName + "\n \n \n \n" +
                "Press Enter to continue...";
        }
        public string Invoke()
        {
            return _bodyTransition;
        }
    }
}