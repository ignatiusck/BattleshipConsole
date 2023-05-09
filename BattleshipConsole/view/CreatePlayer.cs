using Components;

class CreatePlayer
{
    public string ComponentName { get => "Home Page"; }
    public Builder PageHome = new();
    public CreatePlayer(int ActivePlayer)
    {
        PageHome.AddComponent(new Header("    BATTLESHIP    "));
        PageHome.AddComponent(new BodyInputName(ActivePlayer));
    }

    public string View()
    {
        return PageHome.Invoke();
    }
}