using Components;

class BattleMap
{
    public string ComponentName { get => "Battle Map"; }
    public Builder PageBox = new();
    public BattleMap(string PlayerName, string[,] ArenaMap, string HPPlayer)
    {
        PageBox.AddComponent(new BodyArenaMap(ArenaMap));
        PageBox.AddComponent(new WriteSpace(1));
        PageBox.AddComponent(new BodyTurnControl(false, PlayerName, HPPlayer));
        PageBox.AddComponent(new BodyInputHit());
    }

    public string View()
    {
        return PageBox.Invoke();
    }
}