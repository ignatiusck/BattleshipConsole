namespace Components
{
    public class BodyListShipMenu : IComponent
    {
        public string ComponentName { get => "Body List Ship Menu"; }
        private string _bodyListShipMenu;
        public BodyListShipMenu(IDictionary<string, Ship> ListShipMenu)
        {
            string ShipMenu = "";
            foreach (KeyValuePair<string, Ship> ship in ListShipMenu)
            {
                ShipMenu += $"[{ship.Key}]   {ship.Value.ShipSize}   {ship.Value.ShipName}\n";
            }

            _bodyListShipMenu =
                "\nKEY  SIZE    NAME \n" + ShipMenu;
        }

        public string Invoke()
        {
            return _bodyListShipMenu;
        }
    }
}