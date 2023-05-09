using MainGameController;
using MainLogger;
using Helpers;
using System.Data.SQLite;
using Newtonsoft.Json;

public partial class Program
{
    private static Logger<Program> Logger = new();
    private static GameController? Game;
    private static string PathGameData = "./controller/saveDbContext/DbData/Data.db";
    private static bool IsContinue;

    public static void Main(string[] args)
    {
        // GameDbContext.CreateDb(PathGameData);
        GameDbContext.ResetDb(PathGameData);

        Logger.Config(false);

        BattleshipStart();
        if (!IsContinue)
        {
            CreatePlayer();
            PreparationPhase();
        }
        BattlePhase();
        BattleshipEnd();
    }

    private static void DataCorrect(string Message, int Count)
    {
        Console.WriteLine(AddColor.Message(Message, ConsoleColor.Green));
        Thread.Sleep(Count);
    }

    private static void DataNotCorrect(string Message, int Count)
    {
        Console.WriteLine(AddColor.Message(Message, ConsoleColor.Red));
        Thread.Sleep(Count);
    }

    private static string ReadKeyCoor()
    {
        string InputCoor = "";
        while (true)
        {
            ConsoleKey key = Console.ReadKey().Key;
            if ((int)key == 36) return "HOME";
            if ((int)key == 13) return InputCoor;
            char Input = Convert.ToChar(key);
            InputCoor += Input.ToString();
        }
    }

    private static bool SelectData()
    {
        char key = Console.ReadKey().KeyChar;
        if (int.TryParse(key.ToString(), out int Id))
        {
            List<Player> ListPlayerInfo = new();
            using (GameDbContext db = new())
            {
                SaveGame GetData = db.SaveGames.Find(Id)!;
                if (GetData == null)
                {
                    DataNotCorrect(" = ID not found.", 1000);
                    return true;
                }

                foreach (GamesPlayers GP in db.GamesPlayers!.Where(gp => gp.SaveGameId == GetData.Id).ToList())
                {
                    Player? GetPlayer = new();
                    GetPlayer = db.Players!.Find(GP.PlayerId);
                    foreach (Ship ship in db.Ships!.Where(Ship => Ship.PlayerId == GetPlayer.Id))
                    {
                        GetPlayer!.ListShip![ship.Key] = ship;
                        GetPlayer.ListShip[ship.Key].ShipCoordinates = JsonConvert.DeserializeObject<List<Coordinate>>(ship.SerializedCoor)!;
                    }
                    GetPlayer!.HitInOpponentArena = JsonConvert.DeserializeObject<string[,]>(GetPlayer.SerializedHit!);
                    GetPlayer.ShipPlayerInArena = JsonConvert.DeserializeObject<string[,]>(GetPlayer.SerializedShip!);

                    ListPlayerInfo.Add(GetPlayer);
                }
                Game = new GameController(ListPlayerInfo, GetData.ActivePlayer, GetData.Id, GetData.CountHP);
            }
            return false;
        }
        return true;
    }

    private static void ListContinueData()
    {
        List<SaveGame> SavedData;

        GameDbContext db = new();
        SavedData = db.SaveGames.ToList();
        db.Dispose();

        Console.Clear();
        Console.Write(new LoadGame(SavedData).View());
    }
}