namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class DecimalCustomization : CustomizationBase<decimal>
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<decimal> Build(IDummy dummy) => dummy.Build<decimal>().FromFactory(() => dummy.Number.WithDecimals(5).Between(1M, short.MaxValue).Create());
}