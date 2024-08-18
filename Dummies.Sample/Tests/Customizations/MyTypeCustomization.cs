namespace Dummies.Sample.Tests.Customizations;

//Follows the assumption that MyType's Id is a numeric string
[AutoCustomization]
public sealed class MyTypeCustomization : CustomizationBase<MyType>
{
    protected override IEnumerable<Type> AdditionalTypes { get; } = [typeof(IMyType)];

    public override IDummyBuilder<MyType> Build(IDummy dummy) => dummy.Build<MyType>().With(x => x.Id, x => x.Create<int>().ToString());
}