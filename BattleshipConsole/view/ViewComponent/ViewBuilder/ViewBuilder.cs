class ViewBuilder : IComponent, IBuilder
{
    public string ComponentName => "Page Box";
    private List<IComponent> _listComponents = new();
    private string? _page;

    public void Reset()
    {
        _listComponents = new();
    }

    public void AddComponent(IComponent component)
    {
        _listComponents.Add(component);
    }

    public void RemoveComponent(IComponent component)
    {
        _listComponents.Remove(component);
    }

    public string Invoke()
    {
        _page = "";
        foreach (IComponent item in _listComponents)
        {
            _page += item.Invoke();
        }

        return _page;
    }
}