namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class IpAddressCustomization : CustomizationBase<IPAddress>
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<IPAddress> Build(IDummy dummy)
    {
        return dummy.Build<IPAddress>().FromFactory(() => new IPAddress(dummy.CreateMany<byte>(4).ToArray()));
    }
}