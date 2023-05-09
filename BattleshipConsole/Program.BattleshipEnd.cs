using Helpers;
public partial class Program
{
    private static void BattleshipEnd()
    {
        Data Data = new("", true);
        Game!.ClearGameData();
        string PlayerName = Game!.GetPlayerActive().Name;
        _ = Task.Run(() => BattleshipEndPage(Data, PlayerName));
        do
        {
            Console.Clear();
            Console.Write(new PlayerWinner(Data.Status, PlayerName).View());
        } while ((int)Console.ReadKey().Key != 13);
    }

    private static async void BattleshipEndPage(IData Data, string PlayerName)
    {
        while (true)
        {
            await Task.Delay(1000);
            Console.Clear();
            Data.Status = !Data.Status;
            Console.Write(new PlayerWinner(Data.Status, PlayerName).View());
        }
    }
}