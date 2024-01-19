using System.Numerics;

namespace ToolBX.Dummies.Customizations;

public abstract class IntegerCustomizationBase<T> : CustomizationBase<T> where T : INumber<T>
{
    public override IDummyBuilder<T> Build(Dummy dummy) => dummy.Build<T>().FromFactory(() => dummy.TryGenerateUnique(T.One, T.CreateSaturating(short.MaxValue)));
}