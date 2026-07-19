namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class GuidCustomization : CustomizationBase<Guid>
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<Guid> Build(IDummy dummy) => dummy.Build<Guid>().FromFactory(Guid.NewGuid);
}