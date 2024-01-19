namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ImmutableDictionaryCustomization : ICustomization
{
    public IEnumerable<Type> Types { get; } = new[] { typeof(ImmutableDictionary<,>), typeof(IImmutableDictionary<,>) };

    public IDummyBuilder Build(Dummy dummy, Type type)
    {
        if (dummy is null) throw new ArgumentNullException(nameof(dummy));
        if (type is null) throw new ArgumentNullException(nameof(type));

        return dummy.Build<object>().FromFactory(() =>
        {
            var keyType = type.GetGenericArguments()[0];
            var valueType = type.GetGenericArguments()[1];

            var keyValuePairType = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType);

            var instance = ListCustomization.MakeGenericList(dummy, keyValuePairType);

            return typeof(ImmutableDictionaryCustomization)
                .GetSingleMethod("ToImmutableDictionary").MakeGenericMethod(keyType, valueType)
                .Invoke(null, new[] { instance })!;
        });
    }

    private static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull => source.ToImmutableDictionary();

}