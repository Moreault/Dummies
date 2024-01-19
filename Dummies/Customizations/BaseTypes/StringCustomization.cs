namespace ToolBX.Dummies.Customizations.BaseTypes;

[AutoCustomization]
public sealed class StringCustomization : CustomizationBase<string>
{
    public override IDummyBuilder<string> Build(Dummy dummy) => dummy.Build<string>().FromFactory(() => Guid.NewGuid().ToString());
}