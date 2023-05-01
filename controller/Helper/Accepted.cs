public class Accepted : IData
{

    public string Message { get; set; }
    public bool Status { get; set; }

    public Accepted()
    {
        Message = "Succed.";
        Status = true;
    }
}