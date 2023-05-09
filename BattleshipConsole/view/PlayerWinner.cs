using Components;

class PlayerWinner
{
    public string ComponentName { get => "Player Winner"; }
    public Builder PageBox = new();

    public PlayerWinner(bool state, string PlayerName)
    {
        PageBox.AddComponent(new Header("    BATTLESHIP    "));
        PageBox.AddComponent(new WriteSpace(3));
        PageBox.AddComponent(new BodyWinner(state, PlayerName));
    }
    public string View()
    {
        return PageBox.Invoke();
    }
}