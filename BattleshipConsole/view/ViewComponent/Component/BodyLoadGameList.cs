namespace Components
{
    class BodyLoadGameList : IComponent
    {
        public string ComponentName { get => "Body Input Ship"; }
        private string _bodyLoadGameList;

        public BodyLoadGameList(IList<SaveGame> ListData)
        {
            string List = "List auto save data : \n\n ID           Time \n";
            foreach (SaveGame Data in ListData)
            {
                List += AddColor.Message($"[ {Data.Id} ]", ConsoleColor.Yellow) +
                "  " +
                Data.Time + "\n";
            }
            _bodyLoadGameList =
                List +
                "\nSelect ID : ";
        }

        public string Invoke()
        {
            return _bodyLoadGameList;
        }
    }
}