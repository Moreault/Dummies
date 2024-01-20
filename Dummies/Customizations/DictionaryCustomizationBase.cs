namespace ToolBX.Dummies.Customizations;

public abstract class DictionaryCustomizationBase : ICustomization
{
    public abstract IEnumerable<Type> Types { get; }

    public IDummyBuilder Build(Dummy dummy, Type type)
    {
        if (dummy is null) throw new ArgumentNullException(nameof(dummy));
        if (type is null) throw new ArgumentNullException(nameof(type));

        return dummy.Build<object>().FromFactory(() =>
        {
            var keyType = type.GetGenericArguments()[0];
            var valueType = type.GetGenericArguments()[1];

            var keyValuePairType = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType);

            var instance = ListCustomizationBase.MakeGenericList(dummy, keyValuePairType);

            return GetType().GetSingleMethod(x => x.Name == nameof(Convert) && !x.IsAbstract).MakeGenericMethod(keyType, valueType).Invoke(this, [instance])!;
        });
    }

    protected abstract object Convert<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull;

}