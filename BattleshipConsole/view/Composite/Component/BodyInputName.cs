namespace Components
{
    public class BodyInputName : IComponent
    {
        public string ComponentName { get => "Body Input Name"; }
        public string _bodyInoutName;

        public BodyInputName(int ActivePlayer)
        {
            _bodyInoutName =
                $"Enter your name (Player {ActivePlayer}) : ";
        }

        public string Invoke()
        {
            return _bodyInoutName;
        }
    }
}