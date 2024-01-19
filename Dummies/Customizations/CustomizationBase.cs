namespace ToolBX.Dummies.Customizations;

public abstract class CustomizationBase<T> : ICustomization<T>
{
    public virtual IEnumerable<Type> Types => new[] { typeof(T) };

    IDummyBuilder ICustomization.Build(Dummy dummy, Type type) => Build(dummy);

    public abstract IDummyBuilder<T> Build(Dummy dummy);
}