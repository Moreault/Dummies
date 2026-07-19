namespace ToolBX.Dummies.Customizations;

public abstract class DictionaryCustomizationBase : GenericCollectionCustomizationBase
{
    private static readonly ConcurrentDictionary<(Type CustomizationType, Type KeyType, Type ValueType), MethodInfo> ConvertCache = new();
    private static readonly ConcurrentDictionary<(Type KeyType, Type ValueType), Type> KvpTypeCache = new();

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    protected override object Factory(IDummy dummy, Type type)
    {
        var keyType = type.GetGenericArguments()[0];
        var valueType = type.GetGenericArguments()[1];

        var keyValuePairType = KvpTypeCache.GetOrAdd((keyType, valueType), key =>
            typeof(KeyValuePair<,>).MakeGenericType(key.KeyType, key.ValueType));

        var instance = CreateEnumerable(dummy, keyValuePairType);

        var method = ConvertCache.GetOrAdd((GetType(), keyType, valueType), key =>
            key.CustomizationType.GetSingleMethod(x => x.Name == nameof(Convert) && !x.IsAbstract).MakeGenericMethod(key.KeyType, key.ValueType));

        return method.Invoke(this, [instance])!;
    }

    protected abstract object Convert<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull;
}
