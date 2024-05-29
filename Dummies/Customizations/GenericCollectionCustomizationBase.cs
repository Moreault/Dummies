namespace ToolBX.Dummies.Customizations;

public abstract class GenericCollectionCustomizationBase : CustomizationBase
{
    protected override IDummyBuilder BuildMe(IDummy dummy, Type type)
    {
        return dummy.Build<object>().WithoutAutoProperties().FromFactory(() => Factory(dummy, type));
    }

    protected abstract object Factory(IDummy dummyGenerator, Type type);

    protected static object CreateEnumerable(IDummy dummy, Type genericType) => EnumerableHelper.Create(dummy, genericType);
}

internal static class EnumerableHelper
{
    internal static object Create(IDummy dummy, Type genericType)
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