namespace ToolBX.Dummies.Customizations;

public interface ICustomization
{
    Func<Type, bool> Condition { get; }
    IDummyBuilder Build(IDummy dummy, Type type);
}