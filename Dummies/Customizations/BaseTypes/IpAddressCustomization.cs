namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class IpAddressCustomization : CustomizationBase<IPAddress>
{
    public override IDummyBuilder<IPAddress> Build(IDummy dummy)
    {
        return dummy.Build<IPAddress>().FromFactory(() => new IPAddress(dummy.CreateMany<byte>(4).ToArray())).Without(x => x.ScopeId);
    }
}