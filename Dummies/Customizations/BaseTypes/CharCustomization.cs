namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class CharCustomization : CustomizationBase<char>
{
    private const string LatinAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public override IDummyBuilder<char> Build(Dummy dummy) => dummy.Build<char>().FromFactory(() => LatinAlphabet.GetRandom());
}