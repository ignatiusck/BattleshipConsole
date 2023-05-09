public partial class Program
{
    private static void BattleshipStart()
    {
        //display home menu
        _isContinue = false;
        bool LoadStatePage = true;
        while (LoadStatePage)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(_view.Home());
                int KeyIn = (int)Console.ReadKey().Key;
                if (KeyIn == 13)
                {
                    LoadStatePage = false;
                    break;
                }
                if (KeyIn == 36)
                {
                    Task.Run(() => AddColor.Message("Loading...", ConsoleColor.Green));
                    if (GameDbContext.IsDataEmpty())
                    {
                        Console.Clear();
                        Console.WriteLine(_view.NotFound());
                        DataNotCorrect("               Will Close in 3s.", 3000);
                        _isContinue = false;
                        break;
                    }
                    do
                    {
                        ListContinueData();
                    } while (SelectData());
                    _isContinue = true;
                    LoadStatePage = false;
                    break;
                }
            }
        }
    }
}