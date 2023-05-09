namespace Components
{
    public class BodyShipPosition : IComponent
    {
        public string ComponentName { get => "Body Ship Position"; }
        private string _bodyShipPosition;
        public BodyShipPosition()
        {
            _bodyShipPosition =
                AddColor.Message(" Your Ship Position        Will close in 3s. ", ConsoleColor.Yellow);
        }
        public string Invoke()
        {
            return _bodyShipPosition;
        }
    }
}