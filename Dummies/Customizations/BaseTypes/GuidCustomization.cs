namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class GuidCustomization : CustomizationBase<Guid>
{
    public override IDummyBuilder<Guid> Build(Dummy dummy) => dummy.Build<Guid>().FromFactory(Guid.NewGuid);
}