namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ImmutableListCustomization : ListCustomizationBase
{
    protected override IEnumerable<Type> Types { get; } = [typeof(ImmutableList<>), typeof(IImmutableList<>)];
    protected override object Convert<T>(IEnumerable<T> source) => source.ToImmutableList();
}