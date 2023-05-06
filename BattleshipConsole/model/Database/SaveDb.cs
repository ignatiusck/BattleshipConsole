using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class SaveDb
{
    [Key]
    public int Id { get; set; }
    public int ActivePlayer { get; set; }
    public string SerializedData { get; set; }
    public string Time { get; set; }
}