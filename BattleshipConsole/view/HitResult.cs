using Components;

class HitResult
{
    public string ComponentName { get => "Hit Result Page"; }
    public Builder PageBox = new();
    public HitResult(bool Status, string Coor, string[,] ArenaMap)
    {
        PageBox.AddComponent(new BodyArenaMap(ArenaMap));
        PageBox.AddComponent(new WriteSpace(1));
        PageBox.AddComponent(new BodyResult(Status, Coor));
        PageBox.AddComponent(new WriteSpace(1));
    }

    public string View()
    {
        return PageBox.Invoke();
    }
}