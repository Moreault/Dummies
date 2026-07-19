namespace ToolBX.Dummies.Customizations.ComplexTypes;

[AutoCustomization]
public sealed class EqualityComparerCustomization : CustomizationBase
{
    protected override IEnumerable<Type> Types { get; } = [typeof(EqualityComparer<>), typeof(IEqualityComparer<>)];

    private static readonly ConcurrentDictionary<Type, object> _cache = new();

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    protected override IDummyBuilder BuildMe(IDummy dummy, Type type) => dummy.Build<object>().FromFactory(() =>
    {
        var generic = type.GetGenericArguments()[0];
        return _cache.GetOrAdd(generic, g =>
        {
            var defaultComparerProperty = typeof(EqualityComparer<>).MakeGenericType(g).GetSingleProperty(nameof(EqualityComparer<int>.Default));
            return defaultComparerProperty.GetValue(null)!;
        });
    });
}