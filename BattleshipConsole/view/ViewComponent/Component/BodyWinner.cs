namespace Components
{
    public class BodyWinner : IComponent
    {
        public string ComponentName { get => "Body Winner"; }
        private string _bodyWinner;
        public BodyWinner(bool state, string PlayerName)
        {
            string Win = state ? AddColor.Message("WINNER", ConsoleColor.Black) : AddColor.Message("WINNER", ConsoleColor.Yellow);
            _bodyWinner =
                Win + "\n" +
                PlayerName + "\n \n \n" +
                "Press Enter to exit...";
        }
        public string Invoke()
        {
            return _bodyWinner;
        }
    }
}