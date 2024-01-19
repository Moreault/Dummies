namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ListCustomization : ICustomization
{
    public IEnumerable<Type> Types { get; } = [typeof(List<>), typeof(IList<>), typeof(IEnumerable<>), typeof(IReadOnlyList<>), typeof(IReadOnlyCollection<>), typeof(ICollection<>)];

    public IDummyBuilder Build(Dummy dummy, Type type)
    {
        if (dummy is null) throw new ArgumentNullException(nameof(dummy));
        if (type is null) throw new ArgumentNullException(nameof(type));

        return dummy.Build<object>().FromFactory(() => MakeGenericList(dummy, type.GetGenericArguments().Single()));
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
}