using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;


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