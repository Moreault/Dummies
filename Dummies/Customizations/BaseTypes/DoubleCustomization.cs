namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class DoubleCustomization : CustomizationBase<double>
{
    public override IDummyBuilder<double> Build(IDummy dummy) => dummy.Build<double>().FromFactory(() => dummy.Number.WithDecimals(5).Between(1d, short.MaxValue).Create());
}