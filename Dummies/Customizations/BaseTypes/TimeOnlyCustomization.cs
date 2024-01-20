namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class TimeOnlyCustomization : CustomizationBase<TimeOnly>
{
    public override IDummyBuilder<TimeOnly> Build(Dummy dummy)
    {
        return dummy.Build<TimeOnly>().FromFactory(() =>
        {
            var hour = dummy.CreateBetween(0, 23);
            var minute = dummy.CreateBetween(0, 59);
            var second = dummy.CreateBetween(0, 59);
            var millisecond = dummy.CreateBetween(0, 999);

            return new TimeOnly(hour, minute, second, millisecond);
        });
    }
}