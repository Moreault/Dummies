namespace ToolBX.Dummies;

public interface IDummyNumberBuilder
{
    IDummyFloatingPointNumberBuilder WithDecimals(int digits = 3);

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

    public IDummyFloatingPointNumberBuilder WithDecimals(int digits = 3) => new DummyFloatingPointNumberBuilder(_dummy, digits);

    public IDummyNumberBuilder<T> LessThan<T>(T value) where T : INumber<T>, IMinMaxValue<T> => new DummyNumberBuilder<T>(_dummy, () => TryGenerateUnique(T.MinValue, value - T.One));

    public IDummyNumberBuilder<T> LessThanOrEqualTo<T>(T value) where T : INumber<T>, IMinMaxValue<T> => new DummyNumberBuilder<T>(_dummy, () => TryGenerateUnique(T.MinValue, value));

    public IDummyNumberBuilder<T> GreaterThan<T>(T value) where T : INumber<T>, IMinMaxValue<T> => new DummyNumberBuilder<T>(_dummy, () => TryGenerateUnique(value + T.One, T.MaxValue));

    public IDummyNumberBuilder<T> GreaterThanOrEqualTo<T>(T value) where T : INumber<T>, IMinMaxValue<T> => new DummyNumberBuilder<T>(_dummy, () => TryGenerateUnique(value, T.MaxValue));

    public IDummyNumberBuilder<T> Between<T>(T min, T max) where T : INumber<T>, IMinMaxValue<T> => new DummyNumberBuilder<T>(_dummy, () => TryGenerateUnique(min, max));

    public T Create<T>() where T : INumber<T>, IMinMaxValue<T> => TryGenerateUnique(T.MinValue, T.MaxValue);

    public IEnumerable<T> CreateMany<T>() where T : INumber<T>, IMinMaxValue<T> => CreateMany<T>(_dummy.Options.DefaultCollectionSize);

    public IEnumerable<T> CreateMany<T>(int amount) where T : INumber<T>, IMinMaxValue<T>
    {
        for (var i = 0; i < amount; i++)
            yield return TryGenerateUnique(T.MinValue, T.MaxValue);
    }

    private T TryGenerateUnique<T>(T min, T max, int attempts = 5) where T : INumber<T>
    {
        //TODO Proper message
        if (min >= max) throw new ArgumentException();

        if (attempts <= 0)
            attempts = 1;

        T generated;
        var i = 0;
        do
        {
            i++;
            generated = PseudoRandomNumberGenerator.Shared.Generate(min, max);
            if (_dummy.TryGenerate(generated))
                break;

        } while (i < attempts);

        return generated;
    }
}

internal sealed class DummyNumberBuilder<T> : IDummyNumberBuilder<T> where T : INumber<T>, IMinMaxValue<T>
{
    private readonly IDummy _dummy;
    private readonly Func<T> _factory;

    internal DummyNumberBuilder(IDummy dummy, Func<T> factory)
    {
        _factory = factory;
    }

    public T Create() => _factory();

    public IEnumerable<T> CreateMany() => CreateMany(_dummy.Options.DefaultCollectionSize);

    public IEnumerable<T> CreateMany(int amount)
    {
        for (var i = 0; i < amount; i++)
            yield return _factory();
    }
}