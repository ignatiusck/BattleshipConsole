using MainGameController;
public partial class Program
{
    private static void CreatePlayer()
    {
        if (!_isContinue) _game = new GameController();
        _logger.Message("Game started", LogLevel.Info);

        //Create new player
        int Count = 1;
        bool Status;
        do
        {
            string InputPlayer;
            bool DataPassed;
            do
            {
                Console.Clear();
                Console.Write(_view.CreatePlayer(Count));
                InputPlayer = Console.ReadLine()!;
                IData Data = _game!.ValidatorPlayer(InputPlayer);
                if (!Data.Status)
                {
                    DataNotCorrect(Data.Message, 1500);
                    DataPassed = Data.Status;
                    _logger.Message("fail to add name Player, retry to enter", LogLevel.Error);
                }
                else
                {
                    DataPassed = Data.Status;
                }
            } while (!DataPassed);
            Count++;
            Status = _game.AddPlayer(InputPlayer);
            DataCorrect("Player data saved", 1000);
        } while (Status);
    }
}