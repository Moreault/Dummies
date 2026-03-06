namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class CharCustomization : CustomizationBase<char>
{
    private const string LatinAlphabet = Characters.Letters + Characters.Numbers;

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<char> Build(IDummy dummy) => dummy.Build<char>().FromFactory(() => LatinAlphabet.GetRandom());
}