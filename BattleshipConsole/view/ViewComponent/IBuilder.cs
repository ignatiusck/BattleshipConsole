public interface IBuilder
{
    void Reset();
    void AddComponent(IComponent Component);
    void RemoveComponent(IComponent Component);
}