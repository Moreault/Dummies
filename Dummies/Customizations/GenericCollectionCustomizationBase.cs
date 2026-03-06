namespace ToolBX.Dummies.Customizations;

public abstract class GenericCollectionCustomizationBase : CustomizationBase
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    protected override IDummyBuilder BuildMe(IDummy dummy, Type type)
    {
        return dummy.Build<object>().FromFactory(() => Factory(dummy, type));
    }

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    protected abstract object Factory(IDummy dummyGenerator, Type type);

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    protected static object CreateEnumerable(IDummy dummy, Type genericType) => EnumerableHelper.Create(dummy, genericType);
}

internal static class EnumerableHelper
{
    private static readonly ConcurrentDictionary<Type, (Type ListType, MethodInfo AddMethod)> _cache = new();

    [RequiresUnreferencedCode("Uses reflection to create generic list instances.")]
    [RequiresDynamicCode("May require runtime code generation for generic types.")]
    internal static object Create(IDummy dummy, Type genericType)
    {
        var objects = dummy.CreateMany(genericType);

        var cached = _cache.GetOrAdd(genericType, t =>
        {
            var listType = typeof(List<>).MakeGenericType(t);
            var addMethod = listType.GetSingleMethod(nameof(List<int>.Add));
            return (listType, addMethod);
        });

        var instance = Activator.CreateInstance(cached.ListType)!;

        foreach (var thing in objects)
            cached.AddMethod.Invoke(instance, [thing]);

        return instance;
    }
}
