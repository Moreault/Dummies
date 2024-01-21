namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class CharCustomization : CustomizationBase<char>
{
    private const string LatinAlphabet = Characters.Letters + Characters.Numbers;

    public override IDummyBuilder<char> Build(Dummy dummy) => dummy.Build<char>().FromFactory(() => LatinAlphabet.GetRandom());
}