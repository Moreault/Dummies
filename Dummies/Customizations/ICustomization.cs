namespace ToolBX.Dummies.Customizations;

public interface ICustomization
{
    Func<Type, bool> Condition { get; }
    //IEnumerable<Type> Types { get; }
    IDummyBuilder Build(IDummy dummy, Type type);
}

public interface ICustomization<T> : ICustomization
{
    IDummyBuilder<T> Build(IDummy dummy);
}