namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class TimeSpanCustomization : CustomizationBase<TimeSpan>
{
    public override IDummyBuilder<TimeSpan> Build(Dummy dummy)
    {
        return dummy.Build<TimeSpan>().FromFactory(() =>
        {
            var days = dummy.CreateBetween(0, 999);
            var hours = dummy.CreateBetween(0, 23);
            var minutes = dummy.CreateBetween(0, 59);
            var seconds = dummy.CreateBetween(0, 59);
            var milliseconds = dummy.CreateBetween(0, 999);

            return new TimeSpan(days, hours, minutes, seconds, milliseconds);
        });
    }
}