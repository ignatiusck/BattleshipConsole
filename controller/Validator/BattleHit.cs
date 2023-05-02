namespace Validators
{
    class ValidatorHit
    {
        public bool IsInputValid(string Input)
        {
            string[] Coor = Input.Split("¼");
            return
                Coor.Length == 2 &&
                int.TryParse(Coor[0], out _) &&
                int.TryParse(Coor[1], out _);
        }
    }
}