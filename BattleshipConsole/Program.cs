using Pages;
using MainGameController;
using MainLogger;
using Helpers;
using System.Data.SQLite;
using Newtonsoft.Json;

public partial class Program
{
    private static Logger<Program> Logger = new();
    private static readonly Page page = new();
    private static GameController? Game;
    private static string PathGameData = "./saveDbContext/DbData/Data.db";
    private static bool IsContinue;

    public static void Main(string[] args)
    {
        // DataDbContext.CreateDb(PathGameData);
        DataDbContext.ResetDb(PathGameData);

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
        if (int.TryParse(key.ToString(), out int number))
        {
            using (DataDbContext db = new())
            {
                SaveDb Data = db.GameDatas.Find(number)!;
                if (Data == null)
                {
                    DataNotCorrect(" = ID not found.", 1000);
                    return true;
                }
                List<Player> ListPlayer = JsonConvert.DeserializeObject<List<Player>>(Data.SerializedData)!;
                Game = new GameController(ListPlayer, Data.ActivePlayer, Data.Id);
            }
            return false;
        }
        return true;
    }

    private static void ListContinueData()
    {
        List<SaveDb> SavedData;

        DataDbContext db = new();
        SavedData = db.GameDatas.ToList();
        db.Dispose();

        Console.Clear();
        Console.Write(page.ListLoadData(SavedData));
    }
}