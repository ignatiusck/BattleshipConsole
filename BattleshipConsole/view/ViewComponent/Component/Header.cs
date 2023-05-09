namespace Components
{
    public class Header : IComponent
    {
        public string ComponentName { get => "Header"; }
        private string _header;

        public Header(string Title)
        {

            _header =
                    "=============================================\n" +
                    $"**            {Title}           **\n" +
                    "=============================================\n";
        }
        public string Invoke()
        {
            return _header;
        }
    }
}