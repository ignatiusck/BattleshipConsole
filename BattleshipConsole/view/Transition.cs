using Components;

class Transition
{
    public string ComponentName { get => "Transition Page"; }
    public Builder PageBox = new();
    public Transition(bool Transition, bool LoadData, string PlayerName)
    {
        string Title = Transition ? "PREPARATION PHASE " : "   BATTLE PHASE   ";
        PageBox.AddComponent(new Header(Title));
        PageBox.AddComponent(new WriteSpace(1));
        PageBox.AddComponent(new BodyTransition(Transition, LoadData, PlayerName));
    }

    public string View()
    {
        return PageBox.Invoke();
    }
}