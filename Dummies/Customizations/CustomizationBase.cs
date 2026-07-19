namespace ToolBX.Dummies.Customizations;

public abstract class CustomizationBase : ICustomization
{
    public Func<Type, bool> Condition => type => Types.Contains(type);

    protected abstract IEnumerable<Type> Types { get; }

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public IDummyBuilder Build(IDummy dummy, Type type)
    {
        if (dummy is null) throw new ArgumentNullException(nameof(dummy));
        if (type is null) throw new ArgumentNullException(nameof(type));
        return BuildMe(dummy, type);
    }

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    protected abstract IDummyBuilder BuildMe(IDummy dummy, Type type);
}

public abstract class CustomizationBase<T> : CustomizationBase
{
    protected override IEnumerable<Type> Types => AdditionalTypes.Concat(typeof(T));

    protected virtual IEnumerable<Type> AdditionalTypes { get; } = [];

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    protected override IDummyBuilder BuildMe(IDummy dummy, Type type) => Build(dummy);

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public abstract IDummyBuilder<T> Build(IDummy dummy);
}
