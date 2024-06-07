namespace ToolBX.Dummies.Customizations.ComplexTypes;

[AutoCustomization]
public sealed class EqualityComparerCustomization : CustomizationBase
{
    protected override IEnumerable<Type> Types { get; } = [typeof(EqualityComparer<>), typeof(IEqualityComparer<>)];

    protected override IDummyBuilder BuildMe(IDummy dummy, Type type) => dummy.Build<object>().FromFactory(() =>
    {
        var generic = type.GetGenericArguments()[0];
        var defaultComparerProperty = typeof(EqualityComparer<>).MakeGenericType(generic).GetSingleProperty(nameof(EqualityComparer<int>.Default));
        return defaultComparerProperty.GetValue(null)!;
    });
}