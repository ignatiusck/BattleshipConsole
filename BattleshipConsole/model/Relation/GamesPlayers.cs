using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class GamesPlayers
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("SaveGame")]
    public int SaveGameId { get; set; }

    [Required]
    [ForeignKey("Player")]
    public int PlayerId { get; set; }

    public SaveGame? SaveGame { get; set; }
    public Player? Player { get; set; }
}