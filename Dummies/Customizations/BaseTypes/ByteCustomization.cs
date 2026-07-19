namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class ByteCustomization : CustomizationBase<byte>
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<byte> Build(IDummy dummy) => dummy.Build<byte>().FromFactory(() => dummy.Number.Between<byte>(1, byte.MaxValue).Create());
}