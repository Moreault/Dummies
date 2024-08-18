namespace Dummies.Sample.Tests.Customizations;

//When the generic type argument (Id) is a string, make it numeric but don't do any special manipulation if it's any other type
[AutoCustomization]
public sealed class MyGenericTypeCustomization : OpenGenericCustomizationBase
{
    protected override IEnumerable<Type> Types { get; } = [typeof(MyGenericType<>), typeof(IMyGenericType<>)];

    protected override object FromFactory<T>(IDummy dummy)
    {
        if (typeof(T) == typeof(string))
            return new MyGenericType<string> { Id = dummy.Create<int>().ToString() };
        return new MyGenericType<T> { Id = dummy.Create<T>() };
    }
}