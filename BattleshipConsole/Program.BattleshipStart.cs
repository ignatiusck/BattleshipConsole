using MainGameController;

public partial class Program
{
    private static void BattleshipStart()
    {
        //display home menu
        IsContinue = false;
        bool LoadStatePage = true;
        while (LoadStatePage)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(page.Home());
                int KeyIn = (int)Console.ReadKey().Key;
                if (KeyIn == 13)
                {
                    LoadStatePage = false;
                    break;
                }
                if (KeyIn == 36)
                {
                    Task.Run(() => AddColor.Message("Loading...", ConsoleColor.Green));
                    if (DataDbContext.IsDataEmpty())
                    {
                        Console.Clear();
                        Console.WriteLine(page.DataNotFound());
                        DataNotCorrect("               Will Close in 3s.", 3000);
                        IsContinue = false;
                        break;
                    }
                    do
                    {
                        ListContinueData();
                    } while (SelectData());
                    IsContinue = true;
                    LoadStatePage = false;
                    break;
                }
            }
        }
    }
}