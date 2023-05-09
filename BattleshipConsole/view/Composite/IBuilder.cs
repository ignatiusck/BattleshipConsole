using System.ComponentModel;
public interface IBuilder
{
    void AddComponent(IComponent Component);
    void RemoveComponent(IComponent Component);
}