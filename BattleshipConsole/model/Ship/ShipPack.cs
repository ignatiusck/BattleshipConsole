public class ShipPack : IShipPack
{
    public string? Name { get; set; }
    public List<IShipPack> List = new();
    public int InGameHp { get; set; }
    public int TotalHP { get; set; }

    public void AddData(IShipPack Ship)
    {
        List.Add(Ship);
    }

    public void RemoveData(IShipPack Ship)
    {
        List.Remove(Ship);
    }

    public int CountHitPoints()
    {
        TotalHP = 0;
        foreach (IShipPack item in List)
        {
            TotalHP += item.CountHitPoints();
        }
        return TotalHP;
    }
}