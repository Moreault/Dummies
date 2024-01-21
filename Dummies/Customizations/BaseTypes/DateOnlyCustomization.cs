namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class DateOnlyCustomization : CustomizationBase<DateOnly>
{
    public override IDummyBuilder<DateOnly> Build(Dummy dummy)
    {
        return dummy.Build<DateOnly>().FromFactory(() =>
        {
            var year = PseudoRandomNumberGenerator.Shared.Generate(1, 9999);
            var month = PseudoRandomNumberGenerator.Shared.Generate(1, 12);
            var day = PseudoRandomNumberGenerator.Shared.Generate(1, 28);

            return new DateOnly(year, month, day);
        });
    }
}