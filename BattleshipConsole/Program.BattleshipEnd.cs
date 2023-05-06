using Pages;
using MainGameController;
using MainLogger;
using Helpers;
using System.Data.SQLite;
public partial class Program
{
    private static void BattleshipEnd()
    {
        Data Data = new("", true);
        Game.ClearGameData();
        string PlayerName = Game!.GetPlayerActive().Name;
        _ = Task.Run(() => BattleshipEndPage(Data, PlayerName));
        do
        {
            Console.Clear();
            Console.Write(page.PlayerWinner(Data.Status, PlayerName));
        } while ((int)Console.ReadKey().Key != 13);
    }

    private static async void BattleshipEndPage(IData Data, string PlayerName)
    {
        while (true)
        {
            await Task.Delay(1000);
            Console.Clear();
            Data.Status = !Data.Status;
            Console.Write(page.PlayerWinner(Data.Status, PlayerName));
        }
    }
}