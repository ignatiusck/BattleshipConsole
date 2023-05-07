using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

public class Player : IPlayer, IPlayerBattleship
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int IdInGame { get; set; }
    [Required]
    public string? Name { get; set; }

    public string? SerializedHit { get; set; }
    public string? SerializedShip { get; set; }
    public virtual ICollection<Ship> Ships { get; set; }


    [NotMapped]
    public string[,]? HitInOpponentArena { get; set; }

    [NotMapped]
    public string[,]? ShipPlayerInArena { get; set; }

    [NotMapped]
    public Dictionary<string, Ship>? ListShip { get; set; }

    public Player()
    {
        Arena arena = new();
        ListShip = new();
        HitInOpponentArena = new string[arena.ArenaSize.Height, arena.ArenaSize.Width];
        ShipPlayerInArena = new string[arena.ArenaSize.Height, arena.ArenaSize.Width];
    }
    public Player(int Id, string name)
    {
        IdInGame = Id;
        Name = name;
    }
}