namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class DecimalCustomization : CustomizationBase<decimal>
{
    public override IDummyBuilder<decimal> Build(IDummy dummy) => dummy.Build<decimal>().FromFactory(() => dummy.Number.WithDecimals(5).Between(1M, short.MaxValue).Create());
}