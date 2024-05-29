namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class DateOnlyCustomization : CustomizationBase<DateOnly>
{
    public override IDummyBuilder<DateOnly> Build(IDummy dummy)
    {
        return dummy.Build<DateOnly>().FromFactory(() =>
        {
            var year = PseudoRandomNumberGenerator.Shared.Generate(1900, 2100);
            var month = PseudoRandomNumberGenerator.Shared.Generate(1, 12);
            var day = PseudoRandomNumberGenerator.Shared.Generate(1, 28);

            return new DateOnly(year, month, day);
        });
    }
}