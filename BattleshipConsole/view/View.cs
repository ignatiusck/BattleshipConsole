public class View
{
    private ViewBuilder _buildView = new();
    private ViewDirector _director = new();
    private string _product = "";
    public string Home()
    {
        _director.Page = _buildView;
        _director.BuildHome();

        _product = _buildView.Invoke();
        _buildView.Reset();

        return _product;
    }

    public string BattleMap(string PlayerName, string[,] ArenaMap, string HPPlayer)
    {
        _director.Page = _buildView;
        _director.BuildBattleMap(PlayerName, ArenaMap, HPPlayer);

        _product = _buildView.Invoke();
        _buildView.Reset();

        return _product;
    }

    public string CreatePlayer(int ActivePlayer)
    {
        _director.Page = _buildView;
        _director.BuildCreatePlayer(ActivePlayer);

        _product = _buildView.Invoke();
        _buildView.Reset();

        return _product;
    }

    public string LoadGame(IList<SaveGame> ListData)
    {
        _director.Page = _buildView;
        _director.BuildLoadGame(ListData);

        _product = _buildView.Invoke();
        _buildView.Reset();

        return _product;
    }

    public string NotFound()
    {
        _director.Page = _buildView;
        _director.BuildNotFound();

        _product = _buildView.Invoke();
        _buildView.Reset();

        return _product;
    }

    public string Transition(bool Transition, bool LoadData, string PlayerName)
    {
        _director.Page = _buildView;
        _director.BuildTransition(Transition, LoadData, PlayerName);

        _product = _buildView.Invoke();
        _buildView.Reset();

        return _product;
    }

    public string PreparationMap(IDictionary<string, Ship> ListShipMenu, string PlayerName, string[,] ArenaMap)
    {
        _director.Page = _buildView;
        _director.BuildPreparationMap(ListShipMenu, PlayerName, ArenaMap);

        _product = _buildView.Invoke();
        _buildView.Reset();

        return _product;
    }

    public string ShipPosition(string[,] ArenaMap)
    {
        _director.Page = _buildView;
        _director.BuildShipPosition(ArenaMap);

        _product = _buildView.Invoke();
        _buildView.Reset();

        return _product;
    }

    public string PlayerWinner(bool state, string PlayerName)
    {
        _director.Page = _buildView;
        _director.BuildPlayerWinner(state, PlayerName);

        _product = _buildView.Invoke();
        _buildView.Reset();

        return _product;
    }

    public string HitResult(bool Status, string Coor, string[,] ArenaMap)
    {
        _director.Page = _buildView;
        _director.BuildHitResult(Status, Coor, ArenaMap);

        _product = _buildView.Invoke();
        _buildView.Reset();

        return _product;
    }
}