namespace Components
{
    public class WriteSpace : IComponent
    {
        public string ComponentName { get => "Body Data Not Found"; }
        private string _writeSpace;
        public WriteSpace(int count)
        {
            string Space = "";
            for (int i = 0; i < count; i++)
            {
                Space += "\n";
            }
            _writeSpace = Space;
        }
        public string Invoke()
        {
            return _writeSpace;
        }
    }
}