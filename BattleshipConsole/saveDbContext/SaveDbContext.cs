using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;


public class DataDbContext : DbContext
{
    public DbSet<SaveDb> GameDatas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./saveDbContext/DbData/Data.db");
    }

    public static bool IsDataEmpty()
    {
        int count;
        using (DataDbContext db = new())
        {
            count = db.GameDatas.Count();
        }
        return count == 0;
    }

    public static void ResetDb(string PathGameData)
    {
        if (DataDbContext.IsDataEmpty())
        {
            using (SQLiteConnection connection = new($"Data Source={PathGameData}"))
            {
                connection.Open();
                // using (var command = connection.CreateCommand())
                // {
                //     command.Connection = connection;
                //     command.CommandText = "DELETE FROM GameDatas;";
                //     command.ExecuteNonQuery();
                // }
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM sqlite_sequence WHERE name='GameDatas';";
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public static void CreateDb(string PathGameData)
    {
        using (DataDbContext context = new())
        {
            if (!context.Database.EnsureCreated())
                SQLiteConnection.CreateFile(PathGameData);
        }
    }
}