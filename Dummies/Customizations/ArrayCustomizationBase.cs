using ToolBX.Dummies.Customizations.Collections;

namespace ToolBX.Dummies.Customizations;

public abstract class ArrayCustomizationBase : GenericCollectionCustomizationBase
{
    protected override object Factory(Dummy dummy, Type type)
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
    }

    protected abstract object CreateMultiDimensionalArray(Dummy dummy, Type arrayType, Type elementType);

    protected abstract object Convert<T>(IEnumerable<T> source);
}