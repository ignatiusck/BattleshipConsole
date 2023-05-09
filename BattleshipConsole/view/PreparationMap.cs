using Components;

class PreparationMap
{
    public string ComponentName { get => "Preparation Map"; }
    public Builder PageBox = new();

    public PreparationMap(IDictionary<string, Ship> ListShipMenu, string PlayerName, string[,] ArenaMap)
    {
        PageBox.AddComponent(new BodyArenaMap(ArenaMap));
        PageBox.AddComponent(new WriteSpace(1));
        PageBox.AddComponent(new BodyTurnControl(true, PlayerName, " "));
        PageBox.AddComponent(new BodyListShipMenu(ListShipMenu));
        PageBox.AddComponent(new WriteSpace(1));
        PageBox.AddComponent(new BodyInputShip());
    }
    public string View()
    {
        return PageBox.Invoke();
    }
}