using MainGameController;
public partial class Program
{
    private static void CreatePlayer()
    {
        if (!IsContinue) Game = new GameController();
        Logger.Message("Game started", LogLevel.Info);

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
                Console.Write(new CreatePlayer(Count).View());
                InputPlayer = Console.ReadLine()!;
                IData Data = Game!.ValidatorPlayer(InputPlayer);
                if (!Data.Status)
                {
                    DataNotCorrect(Data.Message, 1500);
                    DataPassed = Data.Status;
                    Logger.Message("fail to add name Player, retry to enter", LogLevel.Error);
                }
                else
                {
                    DataPassed = Data.Status;
                }
            } while (!DataPassed);
            Count++;
            Status = Game.AddPlayer(InputPlayer);
            DataCorrect("Player data saved", 1000);
        } while (Status);
    }
}