public partial class Program
{
    private static void PreparationPhase()
    {
        //transition
        do
        {
            Console.Clear();
            Console.Write(_view.Transition(true, false, _game!.GetPlayerActive().Name));
        } while ((int)Console.ReadKey().Key != 13);

        //setup player ship
        int Count = 0;
        do
        {
            IDictionary<string, Ship> ListShipMenu = _game.GetListShipInGame();
            string PlayerName = _game.GetPlayerActive().Name;
            string[,] ArenaMap = _game.GetShipPlayerInArena(); //aarrgg
            while (ListShipMenu.Count != 0)
            {
                bool IsPassed = false;
                while (!IsPassed)
                {
                    Console.Clear();
                    Console.Write(_view.PreparationMap(ListShipMenu, PlayerName, ArenaMap));
                    string? InputPlayer = Console.ReadLine();
                    IData Data = _game.ValidatorPreparation(InputPlayer!, ListShipMenu, ArenaMap);
                    if (!Data.Status)
                    {
                        DataNotCorrect(Data.Message, 1000);
                        _logger.Message(Data.Message, LogLevel.Error);
                        break;
                    }
                    _game.AddShipToArena(InputPlayer!, ListShipMenu);
                    Console.Clear();
                    Console.Write(_view.PreparationMap(ListShipMenu, PlayerName, ArenaMap));
                    DataCorrect(" ship added.", 1000);
                    IsPassed = Data.Status;
                    _logger.Message("Ship added.", LogLevel.Info);
                }
            }
            DataCorrect("Ship Position saved.", 1000);
            Count += _game.TurnControl();
        } while (Count != 2);
    }
}