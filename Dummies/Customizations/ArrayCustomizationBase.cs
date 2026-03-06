namespace ToolBX.Dummies.Customizations;

public abstract class ArrayCustomizationBase : ICustomization
{
    public Func<Type, bool> Condition => x => Types.Contains(x) || x.IsArray;

    protected abstract IEnumerable<Type> Types { get; }

    private static readonly ConcurrentDictionary<(Type CustomizationType, Type ElementType), MethodInfo> _convertCache = new();

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public IDummyBuilder Build(IDummy dummy, Type type)
    {
        if (dummy == null) throw new ArgumentNullException(nameof(dummy));
        if (type == null) throw new ArgumentNullException(nameof(type));

        return dummy.Build<object>().FromFactory(() =>
        {
            var elementType = type.GetElementType()!;
            var dimensions = type.IsArray ? type.GetArrayRank() : 1;
            if (dimensions == 1)
            {
                var list = CreateEnumerable(dummy, elementType);

                var method = _convertCache.GetOrAdd((GetType(), elementType), key =>
                    key.CustomizationType.GetSingleMethod(x => x.Name == nameof(Convert) && !x.IsAbstract).MakeGenericMethod(key.ElementType));

                return method.Invoke(this, [list])!;
            }

            return CreateMultiDimensionalArray(dummy, type, elementType);
        });
    }

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    protected static object CreateEnumerable(IDummy dummy, Type genericType) => EnumerableHelper.Create(dummy, genericType);

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    protected abstract object CreateMultiDimensionalArray(IDummy dummy, Type arrayType, Type elementType);

    protected abstract object Convert<T>(IEnumerable<T> source);
}
