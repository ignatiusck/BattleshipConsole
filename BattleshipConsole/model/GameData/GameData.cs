using Newtonsoft.Json;
using MainGameController;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Keyless]
public class GameData
{
    // public List<Player>? ListPlayerInfo { get; set; }
    [NotMapped]
    public Arena? Arena { get; set; }
    public int ActivePlayer { get; set; }
    public string SerializedListPlayerInfo { get; set; }

    [NotMapped]
    public List<Player> ListPlayerInfo
    {
        get
        {
            if (string.IsNullOrEmpty(SerializedListPlayerInfo))
            {
                return new List<Player>();
            }

            return JsonConvert.DeserializeObject<List<Player>>(SerializedListPlayerInfo);
        }
        set
        {
            SerializedListPlayerInfo = JsonConvert.SerializeObject(value);
        }
    }
    public GameData()
    {
        ListPlayerInfo = new();
        Arena = new();
        ActivePlayer = new();
    }
    public GameData(string PathGameData)
    {
        GameData Data = new();
        using (StreamReader Reader = new(PathGameData))
        using (JsonReader jsonReader = new JsonTextReader(Reader))
        {
            JsonSerializer Serializer = new();
            Data = Serializer.Deserialize<GameData>(jsonReader)!;
        }
        ListPlayerInfo = Data.ListPlayerInfo;
        Arena = Data.Arena;
        ActivePlayer = Data.ActivePlayer;
    }

    public static bool IsDataEmpty(string PathGameData)
    {
        FileInfo info = new(PathGameData);
        return info.Length < 10;
    }
}

public class GameDataContext : DbContext
{
    public DbSet<GameData> GameDatas { get; set; }
    public DbSet<ArrayArena> ArenaMap { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./model/GameData/Data.db");
    }
}

public class ArrayArena
{
    public int Id { get; set; }
    public string Data { get; set; }
}