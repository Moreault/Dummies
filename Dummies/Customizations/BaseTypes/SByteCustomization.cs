namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class SByteCustomization : CustomizationBase<sbyte>
{
    public override IDummyBuilder<sbyte> Build(IDummy dummy) => dummy.Build<sbyte>().FromFactory(() => dummy.Number.Between<sbyte>(1, sbyte.MaxValue).Create());
}