namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class FloatCustomization : CustomizationBase<float>
{
    public override IDummyBuilder<float> Build(Dummy dummy) => dummy.Build<float>().FromFactory(() => dummy.GenerateFloatingPoint(1f, short.MaxValue, 5));
}