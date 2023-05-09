using System.ComponentModel.DataAnnotations;

public class SaveGame
{
    [Key]
    public int Id { get; set; }
    public int ActivePlayer { get; set; }
    public string? CountHP { get; set; }
    public string? Time { get; set; }
}