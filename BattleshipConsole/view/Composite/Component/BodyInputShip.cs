namespace Components
{
    public class BodyInputShip : IComponent
    {
        public string ComponentName { get => "Body Input Ship"; }
        private string _bodyInputShip;
        public BodyInputShip()
        {
            _bodyInputShip =
                "Input : " +
                AddColor.Message("'KEY Coordinat H/V'", ConsoleColor.Yellow) +
                " Example : " +
                AddColor.Message("'B 1,1 H' \n", ConsoleColor.Yellow) +
                "Place your ship : ";
        }
        public string Invoke()
        {
            return _bodyInputShip;
        }
    }
}