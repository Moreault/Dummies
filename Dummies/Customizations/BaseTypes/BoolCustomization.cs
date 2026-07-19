namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class BoolCustomization : CustomizationBase<bool>
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<bool> Build(IDummy dummy) => dummy.Build<bool>().FromFactory(Coin.Flip);
}