namespace ToolBX.Dummies.Customizations;

public interface ICustomization
{
    IEnumerable<Type> Types { get; }
    IDummyBuilder Build(Dummy dummy, Type type);
}

public interface ICustomization<T> : ICustomization
{
    IDummyBuilder<T> Build(Dummy dummy);
}