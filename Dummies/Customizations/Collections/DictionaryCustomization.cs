namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class DictionaryCustomization : DictionaryCustomizationBase
{
    public override IEnumerable<Type> Types { get; } = [typeof(Dictionary<,>), typeof(IDictionary<,>), typeof(IReadOnlyDictionary<,>)];

    protected override object Convert<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source) => source.ToDictionary();
}