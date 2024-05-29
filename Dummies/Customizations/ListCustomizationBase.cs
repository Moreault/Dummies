namespace ToolBX.Dummies.Customizations;

public abstract class ListCustomizationBase : GenericCollectionCustomizationBase
{
    protected override object Factory(IDummy dummy, Type type)
    {
        var genericType = type.GetGenericArguments().Single();
        var list = CreateEnumerable(dummy, genericType);
        return GetType().GetSingleMethod(x => x.Name == nameof(Convert) && !x.IsAbstract).MakeGenericMethod(genericType).Invoke(this, [list])!;
    }

    protected abstract object Convert<T>(IEnumerable<T> source);
}