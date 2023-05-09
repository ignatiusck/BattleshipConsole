using Helpers;
public partial class Program
{
    private static void BattleshipEnd()
    {
        Data Data = new("", true);
        _game!.ClearGameData();
        string PlayerName = _game!.GetPlayerActive().Name;
        _ = Task.Run(() => BattleshipEndPage(Data, PlayerName));
        do
        {
            Console.Clear();
            Console.Write(_view.PlayerWinner(Data.Status, PlayerName));
        } while ((int)Console.ReadKey().Key != 13);
    }

    private static async void BattleshipEndPage(IData Data, string PlayerName)
    {
        while (true)
        {
            await Task.Delay(1000);
            Console.Clear();
            Data.Status = !Data.Status;
            Console.Write(_view.PlayerWinner(Data.Status, PlayerName));
        }
    }
}