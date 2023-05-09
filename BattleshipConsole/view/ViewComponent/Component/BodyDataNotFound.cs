namespace Components
{
    public class BodyDataNotFound : IComponent
    {
        public string ComponentName { get => "Body Data Not Found"; }
        private string _bodyDataNotFound;
        public BodyDataNotFound()
        {
            _bodyDataNotFound =
                "                Data not found!";
        }
        public string Invoke()
        {
            return _bodyDataNotFound;
        }
    }
}