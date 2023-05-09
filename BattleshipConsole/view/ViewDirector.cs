using Components;

class ViewDirector
{
    public IBuilder? Page;

    public void BuildHome()
    {
        Page!.AddComponent(new Header("    BATTLESHIP    "));
        Page.AddComponent(new WriteSpace(2));
        Page.AddComponent(new BodyHome());
    }

    public void BuildBattleMap(string PlayerName, string[,] ArenaMap, string HPPlayer)
    {
        Page!.AddComponent(new BodyArenaMap(ArenaMap));
        Page.AddComponent(new WriteSpace(1));
        Page.AddComponent(new BodyTurnControl(false, PlayerName, HPPlayer));
        Page.AddComponent(new BodyInputHit());
    }

    public void BuildCreatePlayer(int ActivePlayer)
    {
        Page!.AddComponent(new Header("    BATTLESHIP    "));
        Page.AddComponent(new BodyInputName(ActivePlayer));
    }

    public void BuildLoadGame(IList<SaveGame> ListData)
    {
        Page!.AddComponent(new Header("    BATTLESHIP    "));
        Page.AddComponent(new BodyLoadGameList(ListData));
    }

    public void BuildNotFound()
    {
        Page!.AddComponent(new Header("    BATTLESHIP    "));
        Page.AddComponent(new WriteSpace(4));
        Page.AddComponent(new BodyDataNotFound());
        Page.AddComponent(new WriteSpace(4));
    }

    public void BuildTransition(bool Transition, bool LoadData, string PlayerName)
    {
        string Title = Transition ? "PREPARATION PHASE " : "   BATTLE PHASE   ";
        Page!.AddComponent(new Header(Title));
        Page.AddComponent(new WriteSpace(1));
        Page.AddComponent(new BodyTransition(Transition, LoadData, PlayerName));
    }

    public void BuildPreparationMap(IDictionary<string, Ship> ListShipMenu, string PlayerName, string[,] ArenaMap)
    {
        Page!.AddComponent(new BodyArenaMap(ArenaMap));
        Page.AddComponent(new WriteSpace(1));
        Page.AddComponent(new BodyTurnControl(true, PlayerName, " "));
        Page.AddComponent(new BodyListShipMenu(ListShipMenu));
        Page.AddComponent(new WriteSpace(1));
        Page.AddComponent(new BodyInputShip());
    }

    public void BuildShipPosition(string[,] ArenaMap)
    {
        Page!.AddComponent(new BodyArenaMap(ArenaMap));
        Page.AddComponent(new WriteSpace(1));
        Page.AddComponent(new BodyShipPosition());
    }

    public void BuildPlayerWinner(bool state, string PlayerName)
    {
        Page!.AddComponent(new Header("    BATTLESHIP    "));
        Page.AddComponent(new WriteSpace(3));
        Page.AddComponent(new BodyWinner(state, PlayerName));
    }

    public void BuildHitResult(bool Status, string Coor, string[,] ArenaMap)
    {
        Page!.AddComponent(new BodyArenaMap(ArenaMap));
        Page.AddComponent(new WriteSpace(1));
        Page.AddComponent(new BodyResult(Status, Coor));
        Page.AddComponent(new WriteSpace(1));
    }
}