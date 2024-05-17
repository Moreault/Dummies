namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ListCustomization : ListCustomizationBase
{
    public override IEnumerable<Type> Types { get; } = [typeof(List<>), typeof(IList<>), typeof(IEnumerable<>), typeof(IReadOnlyList<>), typeof(IReadOnlyCollection<>), typeof(ICollection<>)];

    protected override object Convert<T>(IEnumerable<T> source) => source;
}