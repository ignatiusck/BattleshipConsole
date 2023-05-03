using System;
using MainGameController;
using Moq;

namespace BattleshipConsole.test;

public class BattleshipConsoleTest
{
    private GameController Game;
    public BattleshipConsoleTest()
    {
        Game = new GameController();
    }

    [Fact]
    public void AddPlayer_ShouldCreateAllDataPlayer()
    {
        bool Expected = true;

        string playerName1 = "Doni";
        string playerName2 = "Dini";

        bool Result1 = Game.AddPlayer(playerName1);
        bool Result2 = Game.AddPlayer(playerName2);

        bool Result = (Result1 == true && Result2 == false) ? true : false;
        Assert.Equal(Result, Expected);
    }

    [Theory]
    [InlineData("Dodo", true)]
    [InlineData("Do", false)]
    [InlineData("       ", false)]
    [InlineData(" ", false)]
    public void ValidatorPlayer_ShouldValidatePlayerInputName(string Name, bool Expected)
    {
        IData Result = Game.ValidatorPlayer(Name);
        Assert.Equal(Result.Status, Expected);
    }

    [Theory]
    [InlineData("dhjehdke", false)]
    [InlineData("G 2,2 V", false)]
    [InlineData("B a,a V", false)]
    [InlineData("B 2,2 K", false)]
    [InlineData("B 2.2 V", false)]
    [InlineData("B 2,2 V", true)]
    public void ValidatorPreparation_SouldValidateInputPosisitionShip(String Input, bool Expected)
    {
        Dictionary<string, IShip> ListShipMenu = new()
        {
            ["B"] = new Battleship(),
            //["B"] = (IShip)new Mock<IShip>(),
        };
        string[,] ArenaMap = new string[10, 10];

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                ArenaMap[i, j] = "_";
            }
        }

        IData Result = Game.ValidatorPreparation(Input, ListShipMenu, ArenaMap);
        Assert.Equal(Result.Status, Expected);
    }

    // [Theory]
    // [InlineData("dhjehdke", false)]
    // [InlineData("a,a", false)]
    // [InlineData("1.1", false)]
    // [InlineData("1,1", true)]
    // public void ValidateInputCoorHit(string Input, bool Expected)
    // {

    // }
}