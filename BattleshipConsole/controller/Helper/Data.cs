namespace Helpers
{
    public sealed class Data : IData
    {
        private readonly string _message;

        public string Message { get => _message; }
        public bool Status { get; set; }

        public Data(string message, bool status)
        {
            _message = message;
            Status = status;
        }
    }
}