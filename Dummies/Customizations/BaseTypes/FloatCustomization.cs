namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class FloatCustomization : CustomizationBase<float>
{
    public override IDummyBuilder<float> Build(IDummy dummy) => dummy.Build<float>().FromFactory(() => dummy.Number.WithDecimals(5).Between(1f, short.MaxValue).Create());
}