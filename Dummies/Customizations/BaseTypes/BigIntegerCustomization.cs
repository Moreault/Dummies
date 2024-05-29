namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class BigIntegerCustomization : CustomizationBase<BigInteger>
{
    public override IDummyBuilder<BigInteger> Build(IDummy dummy) => dummy.Build<BigInteger>().FromFactory(() => dummy.Create<long>());
}