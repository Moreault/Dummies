namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class TimeSpanCustomization : CustomizationBase<TimeSpan>
{
    public override IDummyBuilder<TimeSpan> Build(Dummy dummy)
    {
        return dummy.Build<TimeSpan>().FromFactory(() =>
        {
            var days = PseudoRandomNumberGenerator.Shared.Generate(0, 999);
            var hours = PseudoRandomNumberGenerator.Shared.Generate(0, 23);
            var minutes = PseudoRandomNumberGenerator.Shared.Generate(0, 59);
            var seconds = PseudoRandomNumberGenerator.Shared.Generate(0, 59);
            var milliseconds = PseudoRandomNumberGenerator.Shared.Generate(0, 999);

            return new TimeSpan(days, hours, minutes, seconds, milliseconds);
        });
    }
}