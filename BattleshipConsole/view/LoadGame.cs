using Components;

class LoadGame
{
    public string ComponentName { get => "Home Page"; }
    public Builder PageHome = new();
    public LoadGame(IList<SaveGame> ListData)
    {
        PageHome.AddComponent(new Header("    BATTLESHIP    "));
        PageHome.AddComponent(new BodyLoadGameList(ListData));
    }

    public string View()
    {
        return PageHome.Invoke();
    }
}