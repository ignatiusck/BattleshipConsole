using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using Helpers;


public class GameDbContext : DbContext
{
    public DbSet<SaveGame>? SaveGames { get; set; }
    public DbSet<Player>? Players { get; set; }
    public DbSet<Ship>? Ships { get; set; }
    public DbSet<GamesPlayers>? GamesPlayers { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./controller/saveDbContext/DbData/Data.db");
    }

    public IData SaveData()
    {
        int Result = SaveChanges();
        if (Result == 0) Result = SaveChanges();
        if (Result == 0) Result = SaveChanges();
        if (Result == 0) return new Rejected("DB Error, failed to save changes");
        return new Accepted();
    }

    public static bool IsDataEmpty()
    {
        int count;
        using (GameDbContext db = new())
        {
            count = db.SaveGames!.Count();
        }
        return count == 0;
    }

    public static void ResetDb(string PathGameData)
    {
        if (GameDbContext.IsDataEmpty())
        {
            using (SQLiteConnection connection = new($"Data Source={PathGameData}"))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM sqlite_sequence WHERE name='SaveGames';";
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public static void CreateDb(string PathGameData)
    {
        using (GameDbContext context = new())
        {
            if (!context.Database.EnsureCreated())
                SQLiteConnection.CreateFile(PathGameData);
        }
    }
}