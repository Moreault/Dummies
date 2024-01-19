namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class DecimalCustomization : CustomizationBase<decimal>
{
    public override IDummyBuilder<decimal> Build(Dummy dummy) => dummy.Build<decimal>().FromFactory(() => dummy.GenerateFloatingPoint(1M, short.MaxValue, 5));
}