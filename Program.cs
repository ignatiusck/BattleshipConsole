class Program
{
    private static GameController game = new();
    private static bool _playerAddShip = true;
    private static bool _playerTurn = true;
    private static int _index = 1;

    public static void Main(string[] args)
    {
        BattleshipStart();
        Preparation();
        BattleStart();
        BattleEnd();
    }
    private static void BattleshipStart()
    {
        game.GameHome();
        game.CreatePlayer();
    }
    private static void Preparation()
    {
        while (_playerTurn)
        {
            game.ResetShipCoor();
            game.AddShipToArena(_playerAddShip);
            game.TurnControl();
            _playerAddShip = (_index < 2) ? _playerTurn = true : _playerTurn = false;
            _index++;
        }
        _playerTurn = true;
    }
    private static void BattleStart()
    {
        while (_playerTurn)
        {
            string inputCoor = "";
            bool DisplayMap = true;
            bool TryInput = true;
            while (TryInput)
            {
                while (DisplayMap)
                {
                    game.DisplayClear();
                    game.DisplayHitArena();
                    game.DisplayPlayerTurn();

                    inputCoor = game.ReadKeyCoor();
                    DisplayMap = (inputCoor == "false") ? true : false;
                }
                string[] coor = inputCoor.Split("¼");
                string result = game.HitEnemy(int.Parse(coor[0]), int.Parse(coor[1]));
                if (result == "false")
                {
                    Console.WriteLine($"Input Invalid. try again.");
                    Thread.Sleep(1000);
                    break;
                }
                game.DisplayClear();
                game.DisplayPlayerTurn();
                game.DisplayHitArena();
                game.DisplayHitResult(coor, result);
                game.TurnControl();
                TryInput = false;
            }
        }
    }

    private static void BattleEnd()
    {

    }

}