namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class DateTimeCustomization : CustomizationBase<DateTime>
{
    public override IDummyBuilder<DateTime> Build(Dummy dummy)
    {
        return dummy.Build<DateTime>().FromFactory(() =>
        {
            var year = dummy.CreateBetween(1, 9999);
            var month = dummy.CreateBetween(1, 12);
            var day = dummy.CreateBetween(1, 28);
            var hour = dummy.CreateBetween(0, 23);
            var minute = dummy.CreateBetween(0, 59);
            var second = dummy.CreateBetween(0, 59);
            var millisecond = dummy.CreateBetween(0, 999);

            return new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Unspecified);
        });
    }
}