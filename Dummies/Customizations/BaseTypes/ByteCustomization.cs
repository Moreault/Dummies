namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class ByteCustomization : CustomizationBase<byte>
{
    public override IDummyBuilder<byte> Build(Dummy dummy) => dummy.Build<byte>().FromFactory(() => dummy.TryGenerateUnique<byte>(1, byte.MaxValue));
}