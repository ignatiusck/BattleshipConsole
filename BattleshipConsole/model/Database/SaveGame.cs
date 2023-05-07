using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class SaveGame
{
    [Key]
    public int Id { get; set; }
    public int ActivePlayer { get; set; }
    public string? Time { get; set; }

}