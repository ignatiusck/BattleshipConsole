namespace Components
{
    public class BodyTurnControl : IComponent
    {
        public string ComponentName { get => "Body Turn Control"; }
        private string _bodyTurnControl;
        public BodyTurnControl(bool Preparation, string PlayerName, string HPPlayer)
        {
            Arena arena = new();
            string View = Preparation ? "List Ship :           " : $"{HPPlayer}";
            PlayerName = AddColor.Message(PlayerName, ConsoleColor.Yellow);
            string Space = "";

            if (arena.ArenaSize.Width >= 10)
                for (int i = 10; i <= arena.ArenaSize.Width; i++) Space += "  ";

            _bodyTurnControl =
                View + Space + "Your Turn, " + PlayerName;
        }
        public string Invoke()
        {
            return _bodyTurnControl;
        }
    }

}