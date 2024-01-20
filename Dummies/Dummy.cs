using System.Numerics;
using ToolBX.Mathemancy.Randomness;

namespace ToolBX.Dummies;

public interface IDummy
{
    T Create<T>();
    object Create(Type type);
    IEnumerable<T> CreateMany<T>();
    IEnumerable<T> CreateMany<T>(int amount);
    IEnumerable<object> CreateMany(Type type);
    IEnumerable<object> CreateMany(Type type, int amount);
    IDummyBuilder<T> Build<T>();
    IDummy Customize(params ICustomization[] customizations);
    IDummy Customize(IEnumerable<ICustomization> customizations);
}

public sealed class Dummy : IDummy
{
    public static DummyOptions GlobalOptions { get; } = new();

    private readonly List<long> _generatedNumbers = new();

    internal List<ICustomization> Customizations { get; } = new();

    public T Create<T>() => Build<T>().Create();

    public object Create(Type type)
    {
        if (type is null) throw new ArgumentNullException(nameof(type));
        return typeof(Dummy).GetSingleMethod(x => x.Name == nameof(Create) && x.ContainsGenericParameters).MakeGenericMethod(type).Invoke(this, Array.Empty<object>())!;
    }

    public IEnumerable<T> CreateMany<T>() => CreateMany<T>(GlobalOptions.DefaultCollectionSize);

    public IEnumerable<T> CreateMany<T>(int amount) => Build<T>().CreateMany(amount);

    public IEnumerable<object> CreateMany(Type type) => CreateMany(type, GlobalOptions.DefaultCollectionSize);

    public IEnumerable<object> CreateMany(Type type, int amount)
    {
        if (type is null) throw new ArgumentNullException(nameof(type));

        for (var i = 0; i < amount; i++)
            yield return Create(type);
    }

    public IDummyBuilder<T> Build<T>() => new DummyBuilder<T>(this);

    public IDummy Customize(params ICustomization[] customizations) => Customize(customizations as IEnumerable<ICustomization>);

    public IDummy Customize(IEnumerable<ICustomization> customizations)
    {
        if (customizations is null) throw new ArgumentNullException(nameof(customizations));
        Customizations.AddRange(customizations);
        return this;
    }

    internal T TryGenerateUnique<T>(T min, T max, int attempts = 5) where T : INumber<T>
    {
        //TODO Proper message
        if (min >= max) throw new ArgumentException();

        if (attempts <= 0)
            attempts = 1;

        var generated = PseudoRandomNumberGenerator.Shared.Generate(min, max);
        for (var i = 0; i < attempts; i++)
        {
            var generated64 = long.CreateSaturating(generated);
            if (!_generatedNumbers.Contains(generated64))
            {
                _generatedNumbers.Add(generated64);
            }
        }
        return generated;
    }

    internal T GenerateFloatingPoint<T>(T min, T max, int maxDigits = 3) where T : IFloatingPoint<T>
    {
        var integer = PseudoRandomNumberGenerator.Shared.Generate(min, max);
        var floating = PseudoRandomNumberGenerator.Shared.GenerateFractions<T>();
        var clamped = T.Clamp(integer + floating, min, max);
        return T.Round(clamped, maxDigits);
    }
}