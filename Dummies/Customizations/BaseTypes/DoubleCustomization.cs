namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class DoubleCustomization : CustomizationBase<double>
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<double> Build(IDummy dummy) => dummy.Build<double>().FromFactory(() => dummy.Number.WithDecimals(5).Between(1d, short.MaxValue).Create());
}