namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class StringCustomization : CustomizationBase<string>
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<string> Build(IDummy dummy) => dummy.Build<string>().FromFactory(() => Guid.NewGuid().ToString());
}