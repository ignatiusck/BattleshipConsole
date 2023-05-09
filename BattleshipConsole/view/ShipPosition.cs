using Components;

class ShipPosition
{
    public string ComponentName { get => "Ship Position"; }
    public Builder PageBox = new();
    public ShipPosition(string[,] ArenaMap)
    {
        PageBox.AddComponent(new BodyArenaMap(ArenaMap));
        PageBox.AddComponent(new WriteSpace(1));
        PageBox.AddComponent(new BodyShipPosition());
    }

    public string View()
    {
        return PageBox.Invoke();
    }
}