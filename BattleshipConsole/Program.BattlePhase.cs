public partial class Program
{
    private static void BattlePhase()
    {
        //transition
        do
        {
            Console.Clear();
            Console.Write(page.Transition(false, IsContinue, Game!.GetPlayerActive().Name));
        } while ((int)Console.ReadKey().Key != 13);

        bool WinnerStatus = false;
        while (!WinnerStatus)
        {
            while (true)
            {
                string[,] ArenaMap = Game.GetPlayerDataInGame().HitInOpponentArena;
                string[,] ShipPosition = Game.GetShipPlayerInArena();
                string PlayerName = Game.GetPlayerActive().Name;

                Console.Clear();
                Console.Write(page.BattleMap(PlayerName, ArenaMap));
                string Input = ReadKeyCoor();
                if (Input == "HOME")
                {
                    Console.Clear();
                    Console.Write(page.ShipPosition(ShipPosition));
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
                Console.Write(page.HitResult(Result.Status, Input, ArenaMap));

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
                Game.SaveGame();
            }
        }
    }
}