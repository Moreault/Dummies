namespace ToolBX.Dummies.Customizations.ComplexTypes;

[AutoCustomization]
public sealed class FuncCustomization : ICustomization
{
    public IEnumerable<Type> Types { get; } =
    [
        typeof(Func<>),
        typeof(Func<,>),
        typeof(Func<,,>),
        typeof(Func<,,,>),
        typeof(Func<,,,,>),
        typeof(Func<,,,,,>),
        typeof(Func<,,,,,,>),
        typeof(Func<,,,,,,,>),
        typeof(Func<,,,,,,,,>),
        typeof(Func<,,,,,,,,,>),
        typeof(Func<,,,,,,,,,,>),
        typeof(Func<,,,,,,,,,,,>),
        typeof(Func<,,,,,,,,,,,,>),
        typeof(Func<,,,,,,,,,,,,,>),
        typeof(Func<,,,,,,,,,,,,,,>),
        typeof(Func<,,,,,,,,,,,,,,,>),
        typeof(Func<,,,,,,,,,,,,,,,,>),
    ];

    public IDummyBuilder Build(Dummy dummy, Type type)
    {
        return dummy.Build<object>().FromFactory(() =>
        {
            var genericArguments = type.GetGenericArguments();

            return typeof(FuncCustomization)
                .GetSingleMethod(x => x.Name == nameof(Create) && x.GetGenericArguments().Length == genericArguments.Length).MakeGenericMethod(genericArguments)
                .Invoke(null, null)!;
        });
    }

    private static Func<T> Create<T>()
    {
        var id = Guid.NewGuid();
        return () => { var _ = id; return default!; };
    }

    private static Func<T1, T2> Create<T1, T2>()
    {
        var id = Guid.NewGuid();
        return _ => { var x = id; return default!; };
    }

    private static Func<T1, T2, T3> Create<T1, T2, T3>()
    {
        var id = Guid.NewGuid();
        return (_, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4> Create<T1, T2, T3, T4>()
    {
        var id = Guid.NewGuid();
        return (_, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _, _, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _, _, _, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _, _, _, _, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _, _, _, _, _, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _, _, _, _, _, _, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _, _, _, _, _, _, _, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _, _, _, _, _, _, _, _, _, _, _) => { var _ = id; return default!; };
    }

    private static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>()
    {
        var id = Guid.NewGuid();
        return (_, _, _, _, _, _, _, _, _, _, _, _, _, _, _, _) => { var _ = id; return default!; };
    }

}