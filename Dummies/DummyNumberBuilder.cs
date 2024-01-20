namespace ToolBX.Dummies;

public interface IDummyNumberBuilder
{
    IDummyNumberBuilder<T> LessThan<T>(T value) where T : INumber<T>, IMinMaxValue<T>;
    IDummyNumberBuilder<T> LessThanOrEqualTo<T>(T value) where T : INumber<T>, IMinMaxValue<T>;
    IDummyNumberBuilder<T> GreaterThan<T>(T value) where T : INumber<T>, IMinMaxValue<T>;
    IDummyNumberBuilder<T> GreaterThanOrEqualTo<T>(T value) where T : INumber<T>, IMinMaxValue<T>;
    IDummyNumberBuilder<T> Between<T>(T min, T max) where T : INumber<T>, IMinMaxValue<T>;

    T Create<T>() where T : INumber<T>, IMinMaxValue<T>;
    IEnumerable<T> CreateMany<T>() where T : INumber<T>, IMinMaxValue<T>;
    IEnumerable<T> CreateMany<T>(int amount) where T : INumber<T>, IMinMaxValue<T>;
}

public interface IDummyNumberBuilder<out T> where T : INumber<T>
{
    T Create();
    IEnumerable<T> CreateMany();
    IEnumerable<T> CreateMany(int amount);
}

internal sealed class DummyNumberBuilder : IDummyNumberBuilder
{
    private readonly Dummy _dummy;

    internal DummyNumberBuilder(Dummy dummy) => _dummy = dummy;

    public IDummyNumberBuilder<T> LessThan<T>(T value) where T : INumber<T>, IMinMaxValue<T> => new DummyNumberBuilder<T>(() => _dummy.TryGenerateUnique(T.MinValue, value - T.One));

    public IDummyNumberBuilder<T> LessThanOrEqualTo<T>(T value) where T : INumber<T>, IMinMaxValue<T> => new DummyNumberBuilder<T>(() => _dummy.TryGenerateUnique(T.MinValue, value));

    public IDummyNumberBuilder<T> GreaterThan<T>(T value) where T : INumber<T>, IMinMaxValue<T> => new DummyNumberBuilder<T>(() => _dummy.TryGenerateUnique(value + T.One, T.MaxValue));

    public IDummyNumberBuilder<T> GreaterThanOrEqualTo<T>(T value) where T : INumber<T>, IMinMaxValue<T> => new DummyNumberBuilder<T>(() => _dummy.TryGenerateUnique(value, T.MaxValue));

    public IDummyNumberBuilder<T> Between<T>(T min, T max) where T : INumber<T>, IMinMaxValue<T> => new DummyNumberBuilder<T>(() => _dummy.TryGenerateUnique(min, max));

    public T Create<T>() where T : INumber<T>, IMinMaxValue<T> => _dummy.Create<T>();

    public IEnumerable<T> CreateMany<T>() where T : INumber<T>, IMinMaxValue<T> => _dummy.CreateMany<T>();

    public IEnumerable<T> CreateMany<T>(int amount) where T : INumber<T>, IMinMaxValue<T> => _dummy.CreateMany<T>(amount);
}

internal sealed class DummyNumberBuilder<T> : IDummyNumberBuilder<T> where T : INumber<T>, IMinMaxValue<T>
{
    private readonly Func<T> _factory;

    internal DummyNumberBuilder(Func<T> factory)
    {
        _factory = factory;
    }

    public T Create() => _factory();

    public IEnumerable<T> CreateMany() => CreateMany(Dummy.GlobalOptions.DefaultCollectionSize);

    public IEnumerable<T> CreateMany(int amount)
    {
        for (var i = 0; i < amount; i++)
            yield return _factory();
    }
}