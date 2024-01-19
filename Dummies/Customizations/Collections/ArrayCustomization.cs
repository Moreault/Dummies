namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ArrayCustomization : ICustomization
{
    public IEnumerable<Type> Types { get; } = [typeof(Array)];

    public IDummyBuilder Build(Dummy dummy, Type type)
    {
        if (dummy is null) throw new ArgumentNullException(nameof(dummy));
        if (type is null) throw new ArgumentNullException(nameof(type));
        if (!type.IsArray) throw new ArgumentException("Type must be an array.", nameof(type));

        return dummy.Build<object>().FromFactory(() =>
        {
            var elementType = type.GetElementType();
            var listType = typeof(List<>).MakeGenericType(elementType);
            var list = ListCustomization.MakeGenericList(dummy, elementType);

            return typeof(ArrayCustomization)
                .GetSingleMethod("ToArray").MakeGenericMethod(elementType)
                .Invoke(null, new[] { list })!;
        });
    }

    private static T[] ToArray<T>(IEnumerable<T> source) => source.ToArray();
}