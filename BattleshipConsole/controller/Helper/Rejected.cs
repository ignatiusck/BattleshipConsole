namespace Helpers
{
    public sealed class Rejected : IData
    {
        private string _message;
        private bool _status;

        public string Message { get => _message; }
        public bool Status { get => _status; set => _status = value; }

        public Rejected(string Message)
        {
            _message = Message;
            _status = false;
        }
    }
}