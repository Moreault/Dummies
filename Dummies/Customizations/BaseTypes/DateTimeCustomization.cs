namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class DateTimeCustomization : CustomizationBase<DateTime>
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<DateTime> Build(IDummy dummy)
    {
        return dummy.Build<DateTime>().FromFactory(() =>
        {
            var year = PseudoRandomNumberGenerator.Shared.Generate(1900, 2100);
            var month = PseudoRandomNumberGenerator.Shared.Generate(1, 12);
            var day = PseudoRandomNumberGenerator.Shared.Generate(1, 28);
            var hour = PseudoRandomNumberGenerator.Shared.Generate(0, 23);
            var minute = PseudoRandomNumberGenerator.Shared.Generate(0, 59);
            var second = PseudoRandomNumberGenerator.Shared.Generate(0, 59);
            var millisecond = PseudoRandomNumberGenerator.Shared.Generate(0, 999);

            return new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Unspecified);
        });
    }
}