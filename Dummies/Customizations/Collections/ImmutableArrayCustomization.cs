namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ImmutableArrayCustomization : ListCustomizationBase
{
    public override IEnumerable<Type> Types => [typeof(ImmutableArray<>)];

    protected override object Convert<T>(IEnumerable<T> source) => source.ToImmutableArray();
}