public interface IComponent
{
    string ComponentName { get; }
    string Invoke();
}