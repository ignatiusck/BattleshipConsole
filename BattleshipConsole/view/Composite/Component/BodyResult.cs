namespace Components
{
    public class BodyResult : IComponent
    {
        public string ComponentName { get => "Body Result"; }
        private string _bodyResult;
        public BodyResult(bool Status, string Coor)
        {
            Coor = AddColor.Message(Coor, ConsoleColor.Yellow);
            string Result = Status ?
                AddColor.Message("Hit!!", ConsoleColor.Green) :
                AddColor.Message("Miss", ConsoleColor.Red);
            _bodyResult =
                $"  Coordinate : {Coor}            Result: {Result}";

        }
        public string Invoke()
        {
            return _bodyResult;
        }
    }
}