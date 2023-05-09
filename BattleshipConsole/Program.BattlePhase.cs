public partial class Program
{
    private static void BattlePhase()
    {
        //transition
        do
        {
            Console.Clear();
            Console.Write(_view.Transition(false, _isContinue, _game!.GetPlayerActive().Name));
        } while ((int)Console.ReadKey().Key != 13);

        bool WinnerStatus = false;
        while (!WinnerStatus)
        {
            while (true)
            {
                string[,] ArenaMap = _game.GetPlayerDataInGame().HitInOpponentArena;
                string[,] ShipPosition = _game.GetShipPlayerInArena();
                string PlayerName = _game.GetPlayerActive().Name;
                IData HPPlayer = _game.GetHPPlayer();

                Console.Clear();
                Console.Write(_view.BattleMap(PlayerName, ArenaMap, HPPlayer.Message));
                string Input = ReadKeyCoor();
                if (Input == "HOME")
                {
                    Console.Clear();
                    Console.Write(_view.ShipPosition(ShipPosition));
                    DataCorrect("", 3000);
                    break;
                }
                IData Data = _game.ValidateInputCoorHit(Input, ArenaMap)!;
                if (!Data.Status)
                {
                    DataNotCorrect("\n " + Data.Message, 1000);
                    break;
                }
                IData Result = _game.HitOpponent(Input);
                ArenaMap = _game.GetPlayerDataInGame().HitInOpponentArena;

                Console.Clear();
                Console.Write(_view.HitResult(Result.Status, Input, ArenaMap));

                string AdditionalMessage = "";
                if (Result.Message != "none")
                    AdditionalMessage = $"\n  You Destroyed {Result.Message} Opponent!";

                DataCorrect(AdditionalMessage, 2000);

                if (_game.GetWinnerStatus())
                {
                    DataCorrect("  All Ship Oppenet Distryed, YOU WIN!!", 2000);
                    WinnerStatus = true;
                    break;
                }
                _game.TurnControl();
                IData ResultSave = _game.SaveGame();
                if (!ResultSave.Status) DataNotCorrect(ResultSave.Message, 2000);
            }
        }
    }
}