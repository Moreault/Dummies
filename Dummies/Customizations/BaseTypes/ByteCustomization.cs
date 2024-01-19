namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class SByteCustomization : CustomizationBase<sbyte>
{
    public override IDummyBuilder<sbyte> Build(Dummy dummy) => dummy.Build<sbyte>().FromFactory(() => dummy.TryGenerateUnique<sbyte>(1, sbyte.MaxValue));
}