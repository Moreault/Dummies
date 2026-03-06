namespace ToolBX.Dummies.Customizations;

public abstract class IntegerCustomizationBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)] T> : CustomizationBase<T> where T : INumber<T>, IMinMaxValue<T>
{
    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    public override IDummyBuilder<T> Build(IDummy dummy) => dummy.Build<T>().FromFactory(() => dummy.Number.Between(T.One, T.CreateSaturating(short.MaxValue)).Create());
}
