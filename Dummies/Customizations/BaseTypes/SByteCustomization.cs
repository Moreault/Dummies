namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class SByteCustomization : CustomizationBase<sbyte>
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<sbyte> Build(IDummy dummy) => dummy.Build<sbyte>().FromFactory(() => dummy.Number.Between<sbyte>(1, sbyte.MaxValue).Create());
}