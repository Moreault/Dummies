namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ImmutableDictionaryCustomization : DictionaryCustomizationBase
{
    public override IEnumerable<Type> Types { get; } = [typeof(ImmutableDictionary<,>), typeof(IImmutableDictionary<,>) ];

    protected override object Convert<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source) => source.ToImmutableDictionary();
}