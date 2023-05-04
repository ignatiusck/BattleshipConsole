using Newtonsoft.Json;
using MainGameController;

public class GameData
{
    public List<Player>? ListPlayerInfo { get; set; }
    public Arena? Arena { get; set; }
    public int ActivePlayer { get; set; }

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