using System.ComponentModel.DataAnnotations.Schema;

public class ShipPart : IShipPart
{
    public string Name { get; set; }
    public List<IShipPart> List = new();
    public int InGameHp { get; set; }
    public int TotalHP { get; set; }

    public void AddData(IShipPart Ship)
    {
        List.Add(Ship);
    }

    public void RemoveData(IShipPart Ship)
    {
        List.Remove(Ship);
    }

    public int CountHitPoints()
    {
        TotalHP = 0;
        foreach (IShipPart item in List)
        {
            TotalHP += item.CountHitPoints();
        }
        return TotalHP;
    }
}