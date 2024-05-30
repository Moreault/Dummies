namespace ToolBX.Dummies.Customizations;

public abstract class ArrayCustomizationBase : ICustomization
{
    public Func<Type, bool> Condition => x => Types.Contains(x) || x.IsArray;

    protected abstract IEnumerable<Type> Types { get; }

    public IDummyBuilder Build(IDummy dummy, Type type)
    {
        return dummy.Build<object>().FromFactory(() =>
        {
            var elementType = type.GetElementType()!;
            var dimensions = type.IsArray ? type.GetArrayRank() : 1;
            if (dimensions == 1)
            {
                var list = CreateEnumerable(dummy, elementType);

                return typeof(ArrayCustomization)
                    .GetSingleMethod(x => x.Name == nameof(Convert) && !x.IsAbstract).MakeGenericMethod(elementType)
                    .Invoke(this, [list])!;
            }

            return CreateMultiDimensionalArray(dummy, type, elementType);
        });
    }

    protected static object CreateEnumerable(IDummy dummy, Type genericType) => EnumerableHelper.Create(dummy, genericType);

    protected abstract object CreateMultiDimensionalArray(IDummy dummy, Type arrayType, Type elementType);

    protected abstract object Convert<T>(IEnumerable<T> source);

}