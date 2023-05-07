public partial class Program
{
    private static void PreparationPhase()
    {
        //transition
        do
        {
            Console.Clear();
            Console.Write(page.Transition(true, false, Game!.GetPlayerActive().Name));
        } while ((int)Console.ReadKey().Key != 13);

        //setup player ship
        int Count = 0;
        do
        {
            IDictionary<string, Ship> ListShipMenu = Game.GetListShipInGame();
            string PlayerName = Game.GetPlayerActive().Name;
            string[,] ArenaMap = Game.GetShipPlayerInArena(); //aarrgg
            while (ListShipMenu.Count != 0)
            {
                bool IsPassed = false;
                while (!IsPassed)
                {
                    Console.Clear();
                    Console.Write(page.PreparationMap(ListShipMenu, PlayerName, ArenaMap));
                    string? InputPlayer = Console.ReadLine();
                    IData Data = Game.ValidatorPreparation(InputPlayer!, ListShipMenu, ArenaMap);
                    if (!Data.Status)
                    {
                        DataNotCorrect(Data.Message, 1000);
                        Logger.Message(Data.Message, LogLevel.Error);
                        break;
                    }
                    Game.AddShipToArena(InputPlayer!, ListShipMenu);
                    Console.Clear();
                    Console.Write(page.PreparationMap(ListShipMenu, PlayerName, ArenaMap));
                    DataCorrect(" ship added.", 1000);
                    IsPassed = Data.Status;
                    Logger.Message("Ship added.", LogLevel.Info);
                }
            }
            DataCorrect("Ship Position saved.", 1000);
            Count += Game.TurnControl();
        } while (Count != 2);
    }
}