namespace Components
{
    public class BodyInputHit : IComponent
    {
        public string ComponentName { get => "Body Input Hit"; }
        private string _bodyInputHit;
        public BodyInputHit()
        {
            _bodyInputHit =
                " \n \n Press " +
                AddColor.Message("'Home'", ConsoleColor.Yellow) +
                " to see your ship positions \n" +
                " Input : " +
                AddColor.Message("'y,x'", ConsoleColor.Yellow) +
                " Example : " +
                AddColor.Message("'3,2'", ConsoleColor.Yellow) + " \n" +
                " Hit Opponent : ";
        }

        public string Invoke()
        {
            return _bodyInputHit;
        }
    }
}