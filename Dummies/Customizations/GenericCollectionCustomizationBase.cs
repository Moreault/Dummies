namespace ToolBX.Dummies.Customizations;

public abstract class GenericCollectionCustomizationBase : ICustomization
{
    public abstract IEnumerable<Type> Types { get; }

    public IDummyBuilder Build(Dummy dummy, Type type)
    {
        if (dummy is null) throw new ArgumentNullException(nameof(dummy));
        if (type is null) throw new ArgumentNullException(nameof(type));

        return dummy.Build<object>().FromFactory(() => Factory(dummy, type));
    }

    protected abstract object Factory(Dummy dummy, Type type);

    protected static object CreateEnumerable(Dummy dummy, Type genericType)
    {
        var objects = dummy.CreateMany(genericType);

        var listType = typeof(List<>).MakeGenericType(genericType);
        var instance = Activator.CreateInstance(listType)!;

        var addMethod = listType.GetSingleMethod(nameof(List<int>.Add));

        foreach (var thing in objects)
            addMethod.Invoke(instance, [thing]);

        return instance;
    }
}