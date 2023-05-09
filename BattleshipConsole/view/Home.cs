using Components;

class Home
{
    public string ComponentName { get => "Home Page"; }
    public Builder PageHome = new();
    public Home()
    {
        PageHome.AddComponent(new Header("    BATTLESHIP    "));
        PageHome.AddComponent(new WriteSpace(2));
        PageHome.AddComponent(new BodyHome());
    }

    public string View()
    {
        return PageHome.Invoke();
    }
}