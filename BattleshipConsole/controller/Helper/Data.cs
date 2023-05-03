namespace Helpers
{
    public sealed class Data : IData
    {
        private string _message;
        private bool _status;

        public string Message { get => _message; }
        public bool Status { get => _status; }

        public Data(string message, bool status)
        {
            _message = message;
            _status = status;
        }
    }
}