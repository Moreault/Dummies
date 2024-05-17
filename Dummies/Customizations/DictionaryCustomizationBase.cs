namespace ToolBX.Dummies.Customizations;

public abstract class DictionaryCustomizationBase : GenericCollectionCustomizationBase
{
    protected override object Factory(Dummy dummy, Type type)
    {
        var keyType = type.GetGenericArguments()[0];
        var valueType = type.GetGenericArguments()[1];

        var keyValuePairType = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType);

        var instance = CreateEnumerable(dummy, keyValuePairType);

        return GetType().GetSingleMethod(x => x.Name == nameof(Convert) && !x.IsAbstract).MakeGenericMethod(keyType, valueType).Invoke(this, [instance])!;
    }

    protected abstract object Convert<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull;
}