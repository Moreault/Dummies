namespace ToolBX.Dummies.Customizations;

public interface ICustomization
{
    Func<Type, bool> Condition { get; }

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    IDummyBuilder Build(IDummy dummy, Type type);
}