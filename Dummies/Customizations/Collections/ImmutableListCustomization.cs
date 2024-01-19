﻿namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ImmutableListCustomization : ICustomization
{
    public IEnumerable<Type> Types { get; } = [typeof(ImmutableList<>), typeof(IImmutableList<>)];

    public IDummyBuilder Build(Dummy dummy, Type type)
    {
        if (dummy is null) throw new ArgumentNullException(nameof(dummy));
        if (type is null) throw new ArgumentNullException(nameof(type));

        return dummy.Build<object>().FromFactory(() =>
        {
            var genericType = type.GenericTypeArguments.Single();
            var instance = ListCustomization.MakeGenericList(dummy, genericType);

            return typeof(ImmutableListCustomization)
                .GetSingleMethod("ToImmutableList").MakeGenericMethod(genericType)
                .Invoke(null, new[] { instance })!;
        });
    }

    private static ImmutableList<T> ToImmutableList<T>(IEnumerable<T> source) => source.ToImmutableList();
}