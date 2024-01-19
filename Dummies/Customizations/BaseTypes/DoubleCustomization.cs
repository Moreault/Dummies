namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class DoubleCustomization : CustomizationBase<double>
{
    public override IDummyBuilder<double> Build(Dummy dummy) => dummy.Build<double>().FromFactory(() => dummy.GenerateFloatingPoint(1d, short.MaxValue, 5));
}