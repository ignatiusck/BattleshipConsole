using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

public class Ship
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string ShipName { get; set; }
    [Required]
    public string Key { get; set; }
    [Required]
    public int ShipSize { get; set; }
    [Required]
    public string SerializedCoor { get; set; }
    [ForeignKey("Player")]
    public int PlayerId { get; set; }

    public virtual Player Player { get; set; }

    [NotMapped]
    public List<Coordinate> ShipCoordinates { get; set; }

    public Ship() { }
    public Ship(string name, int size)
    {
        ShipName = name;
        ShipSize = size;
        ShipCoordinates = new List<Coordinate>();
    }
}