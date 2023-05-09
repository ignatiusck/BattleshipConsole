public partial class Program
{
    private static void BattlePhase()
    {
        //transition
        do
        {
            Console.Clear();
            Console.Write(new Transition(false, IsContinue, Game!.GetPlayerActive().Name).View());
        } while ((int)Console.ReadKey().Key != 13);

        bool WinnerStatus = false;
        while (!WinnerStatus)
        {
            while (true)
            {
                string[,] ArenaMap = Game.GetPlayerDataInGame().HitInOpponentArena;
                string[,] ShipPosition = Game.GetShipPlayerInArena();
                string PlayerName = Game.GetPlayerActive().Name;
                IData HPPlayer = Game.GetHPPlayer();

                Console.Clear();
                Console.Write(new BattleMap(PlayerName, ArenaMap, HPPlayer.Message).View());
                string Input = ReadKeyCoor();
                if (Input == "HOME")
                {
                    Console.Clear();
                    Console.Write(new ShipPosition(ShipPosition).View());
                    DataCorrect("", 3000);
                    break;
                }
                IData Data = Game.ValidateInputCoorHit(Input, ArenaMap)!;
                if (!Data.Status)
                {
                    DataNotCorrect("\n " + Data.Message, 1000);
                    break;
                }
                IData Result = Game.HitOpponent(Input);
                ArenaMap = Game.GetPlayerDataInGame().HitInOpponentArena;

                Console.Clear();
                Console.Write(new HitResult(Result.Status, Input, ArenaMap).View());

                string AdditionalMessage = "";
                if (Result.Message != "none")
                    AdditionalMessage = $"\n  You Destroyed {Result.Message} Opponent!";

                DataCorrect(AdditionalMessage, 2000);

                if (Game.GetWinnerStatus())
                {
                    DataCorrect("  All Ship Oppenet Distryed, YOU WIN!!", 2000);
                    WinnerStatus = true;
                    break;
                }
                Game.TurnControl();
                IData ResultSave = Game.SaveGame();
                if (!ResultSave.Status) DataNotCorrect(ResultSave.Message, 2000);
            }
        }
    }
}