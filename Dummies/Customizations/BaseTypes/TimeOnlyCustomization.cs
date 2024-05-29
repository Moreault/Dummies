namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class TimeOnlyCustomization : CustomizationBase<TimeOnly>
{
    public override IDummyBuilder<TimeOnly> Build(IDummy dummy)
    {
        return dummy.Build<TimeOnly>().FromFactory(() =>
        {
            var hour = PseudoRandomNumberGenerator.Shared.Generate(0, 23);
            var minute = PseudoRandomNumberGenerator.Shared.Generate(0, 59);
            var second = PseudoRandomNumberGenerator.Shared.Generate(0, 59);
            var millisecond = PseudoRandomNumberGenerator.Shared.Generate(0, 999);

            return new TimeOnly(hour, minute, second, millisecond);
        });
    }
}