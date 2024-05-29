namespace ToolBX.Dummies;

public interface IDummyFloatingPointNumberBuilder
{
    IDummyNumberBuilder<T> LessThan<T>(T value) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T>;
    IDummyNumberBuilder<T> LessThanOrEqualTo<T>(T value) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T>;
    IDummyNumberBuilder<T> GreaterThan<T>(T value) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T>;
    IDummyNumberBuilder<T> GreaterThanOrEqualTo<T>(T value) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T>;
    IDummyNumberBuilder<T> Between<T>(T min, T max) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T>;

    T Create<T>() where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T>;
    IEnumerable<T> CreateMany<T>() where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T>;
    IEnumerable<T> CreateMany<T>(int amount) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T>;
}

internal sealed class DummyFloatingPointNumberBuilder : IDummyFloatingPointNumberBuilder
{
    private readonly int _digits;

    internal DummyFloatingPointNumberBuilder(int digits)
    {
        _digits = digits;
    }

    private T GenerateFloatingPoint<T>(T min, T max) where T : IFloatingPoint<T>
    {
        var integer = PseudoRandomNumberGenerator.Shared.Generate(min, max);
        var floating = PseudoRandomNumberGenerator.Shared.GenerateFractions<T>();
        var clamped = T.Clamp(integer + floating, min, max);
        return T.Round(clamped, _digits);
    }

    public IDummyNumberBuilder<T> LessThan<T>(T value) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T> => new DummyNumberBuilder<T>(() => GenerateFloatingPoint(T.MinValue, value - T.One));

    public IDummyNumberBuilder<T> LessThanOrEqualTo<T>(T value) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T> => new DummyNumberBuilder<T>(() => GenerateFloatingPoint(T.MinValue, value));

    public IDummyNumberBuilder<T> GreaterThan<T>(T value) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T> => new DummyNumberBuilder<T>(() => GenerateFloatingPoint(value + T.One, T.MaxValue));

    public IDummyNumberBuilder<T> GreaterThanOrEqualTo<T>(T value) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T> => new DummyNumberBuilder<T>(() => GenerateFloatingPoint(value, T.MaxValue));

    public IDummyNumberBuilder<T> Between<T>(T min, T max) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T> => new DummyNumberBuilder<T>(() => GenerateFloatingPoint(min, max));

    public T Create<T>() where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T> => GenerateFloatingPoint(T.MinValue, T.MaxValue);

    public IEnumerable<T> CreateMany<T>() where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T> => CreateMany<T>(DummyOptions.Global.DefaultCollectionSize);

    public IEnumerable<T> CreateMany<T>(int amount) where T : INumber<T>, IMinMaxValue<T>, IFloatingPoint<T>
    {
        for (var i = 0; i < amount; i++)
            yield return GenerateFloatingPoint(T.MinValue, T.MaxValue);
    }
}