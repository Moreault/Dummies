namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class BigIntegerCustomization : CustomizationBase<BigInteger>
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<BigInteger> Build(IDummy dummy) => dummy.Build<BigInteger>().FromFactory(() => dummy.Create<long>());
}