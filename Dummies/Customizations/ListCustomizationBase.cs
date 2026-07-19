namespace ToolBX.Dummies.Customizations;

public abstract class ListCustomizationBase : GenericCollectionCustomizationBase
{
    private static readonly ConcurrentDictionary<(Type CustomizationType, Type ElementType), MethodInfo> _convertCache = new();

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    protected override object Factory(IDummy dummy, Type type)
    {
        var genericType = type.GetGenericArguments().Single();
        var list = CreateEnumerable(dummy, genericType);

        var method = _convertCache.GetOrAdd((GetType(), genericType), key =>
            key.CustomizationType.GetSingleMethod(x => x.Name == nameof(Convert) && !x.IsAbstract).MakeGenericMethod(key.ElementType));

        return method.Invoke(this, [list])!;
    }

    protected abstract object Convert<T>(IEnumerable<T> source);
}
