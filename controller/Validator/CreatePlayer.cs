public class ValidatorCreatePlayer
{

    public bool IslengthUnderLimit(string Input, int Limit)
    {
        return Input.Trim().Length < Limit;
    }

    public bool IsPlayerAvailable(string Input, List<Player> PlayerList)
    {
        return PlayerList.Any(player =>
            string.Equals(player.Name, Input, StringComparison.OrdinalIgnoreCase)
            );
    }
}