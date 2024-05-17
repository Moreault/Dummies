namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class BigIntegerCustomization : CustomizationBase<BigInteger>
{
    public override IDummyBuilder<BigInteger> Build(Dummy dummy) => dummy.Build<BigInteger>().FromFactory(() => dummy.Create<long>());
}