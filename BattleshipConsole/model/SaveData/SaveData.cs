public class SaveData
{
    public List<Player>? ListPlayerInfo { get; set; }
    public Arena? Arena { get; set; }
    public int ActivePlayer { get; set; }
    public string[,] ArenaArray { get; set; } = null!;
}