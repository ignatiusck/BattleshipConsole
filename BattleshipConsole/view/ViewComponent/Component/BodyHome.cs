namespace Components
{
    public class BodyHome : IComponent
    {
        public string ComponentName { get => "Body Home"; }
        private string _bodyHome;

        public BodyHome()
        {
            _bodyHome =
                "                 MULTIPLAYER \n" +
                "  Game Menu : \n \n" +
                "    > Start New Game --- " +
                AddColor.Message("[ENTER]", ConsoleColor.Yellow) +
                " \n    > Continue Game  --- " +
                AddColor.Message("[HOME]", ConsoleColor.Yellow) + "  " +
                AddColor.Message("(Beta)", ConsoleColor.Black) +
                " \n \n \n \n \n" +
                "Press 'Enter' or 'Home' button to continue...";
        }

        public string Invoke()
        {
            return _bodyHome;
        }
    }
}