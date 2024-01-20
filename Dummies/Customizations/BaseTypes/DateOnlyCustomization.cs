namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class DateOnlyCustomization : CustomizationBase<DateOnly>
{
    public override IDummyBuilder<DateOnly> Build(Dummy dummy)
    {
        return dummy.Build<DateOnly>().FromFactory(() =>
        {
            var year = dummy.CreateBetween(1, 9999);
            var month = dummy.CreateBetween(1, 12);
            var day = dummy.CreateBetween(1, 28);

            return new DateOnly(year, month, day);
        });
    }
}