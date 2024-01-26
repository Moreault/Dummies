namespace ToolBX.Dummies.Customizations.ComplexTypes;

[AutoCustomization]
public sealed class ActionCustomization : ICustomization
{
    public IEnumerable<Type> Types { get; } =
    [
        typeof(Action),
        typeof(Action<>),
        typeof(Action<,>),
        typeof(Action<,,>),
        typeof(Action<,,,>),
        typeof(Action<,,,,>),
        typeof(Action<,,,,,>),
        typeof(Action<,,,,,,>),
        typeof(Action<,,,,,,,>),
        typeof(Action<,,,,,,,,>),
        typeof(Action<,,,,,,,,,>),
        typeof(Action<,,,,,,,,,,>),
        typeof(Action<,,,,,,,,,,,>),
        typeof(Action<,,,,,,,,,,,,>),
        typeof(Action<,,,,,,,,,,,,,>),
        typeof(Action<,,,,,,,,,,,,,,>),
        typeof(Action<,,,,,,,,,,,,,,,>),
    ];

    public IDummyBuilder Build(Dummy dummy, Type type)
    {
        return dummy.Build<object>().FromFactory(() =>
        {
            var genericArguments = type.GetGenericArguments();

            if (genericArguments.Length == 0)
                return Create();

            return typeof(ActionCustomization)
                .GetSingleMethod(x => x.Name == nameof(Create) && x.GetGenericArguments().Length == genericArguments.Length)
                .MakeGenericMethod(genericArguments)
                .Invoke(null, null)!;
        });
    }

    private static Action Create() => () => { };
    private static Action<T> Create<T>() => _ => { };
    private static Action<T1, T2> Create<T1, T2>() => (_, _) => { };
    private static Action<T1, T2, T3> Create<T1, T2, T3>() => (_, _, _) => { };
    private static Action<T1, T2, T3, T4> Create<T1, T2, T3, T4>() => (_, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>() => (_, _, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>() => (_, _, _, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>() => (_, _, _, _, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>() => (_, _, _, _, _, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>() => (_, _, _, _, _, _, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>() => (_, _, _, _, _, _, _, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>() => (_, _, _, _, _, _, _, _, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>() => (_, _, _, _, _, _, _, _, _, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>() => (_, _, _, _, _, _, _, _, _, _, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>() => (_, _, _, _, _, _, _, _, _, _, _, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>() => (_, _, _, _, _, _, _, _, _, _, _, _, _, _, _) => { };
    private static Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>() => (_, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _) => { };
}