namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class BoolCustomization : CustomizationBase<bool>
{
    public override IDummyBuilder<bool> Build(Dummy dummy) => dummy.Build<bool>().FromFactory(Coin.Flip);
}