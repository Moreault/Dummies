namespace ToolBX.Dummies.Customizations;

public abstract class ListCustomizationBase : ICustomization
{
    public abstract IEnumerable<Type> Types { get; }

    public IDummyBuilder Build(Dummy dummy, Type type)
    {
        if (dummy is null) throw new ArgumentNullException(nameof(dummy));
        if (type is null) throw new ArgumentNullException(nameof(type));

        return dummy.Build<object>().FromFactory(() =>
        {
            var genericType = type.GetGenericArguments().Single();
            var list = MakeGenericList(dummy, genericType);
            return GetType().GetSingleMethod(x => x.Name == nameof(Convert) && !x.IsAbstract).MakeGenericMethod(genericType).Invoke(this, [list])!;

        });
    }

    internal static object MakeGenericList(Dummy dummy, Type genericType)
    {
        var objects = dummy.CreateMany(genericType);

        var listType = typeof(List<>).MakeGenericType(genericType);
        var instance = Activator.CreateInstance(listType)!;

        var addMethod = listType.GetSingleMethod(nameof(List<int>.Add));

        foreach (var thing in objects)
            addMethod.Invoke(instance, new[] { thing });

        return instance;
    }

    protected abstract object Convert<T>(IEnumerable<T> source);
}