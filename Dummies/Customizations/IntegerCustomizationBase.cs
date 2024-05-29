namespace ToolBX.Dummies.Customizations;

public abstract class IntegerCustomizationBase<T> : CustomizationBase<T> where T : INumber<T>, IMinMaxValue<T>
{
    public override IDummyBuilder<T> Build(IDummy dummy) => dummy.Build<T>().FromFactory(() => dummy.Number.Between(T.One, T.CreateSaturating(short.MaxValue)).Create());
}