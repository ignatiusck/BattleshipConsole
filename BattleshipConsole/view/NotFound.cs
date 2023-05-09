using Components;

class NotFound
{
    public Builder pageBox = new();
    public NotFound()
    {
        pageBox.AddComponent(new Header("    BATTLESHIP    "));
        pageBox.AddComponent(new WriteSpace(4));
        pageBox.AddComponent(new BodyDataNotFound());
        pageBox.AddComponent(new WriteSpace(4));
    }
    public string View()
    {
        return pageBox.Invoke();
    }
}
