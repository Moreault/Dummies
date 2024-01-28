namespace ToolBX.Dummies;

public interface IDummy
{
    /// <summary>
    /// Generates unique random numbers bypassing all number <see cref="ICustomization"/>s.
    /// </summary>
    IDummyNumberBuilder Number { get; }

    /// <summary>
    /// Generates unique random date bypassing all DateTime <see cref="ICustomization"/>s.
    /// </summary>
    IDummyDateTimeBuilder Date { get; }

    /// <summary>
    /// Generates unique random strings bypassing all string <see cref="ICustomization"/>s.
    /// </summary>
    IDummyStringBuilder String { get; }

    T Create<T>();
    object Create(Type type);
    IEnumerable<T> CreateMany<T>();
    IEnumerable<T> CreateMany<T>(int amount);
    IEnumerable<object> CreateMany(Type type);
    IEnumerable<object> CreateMany(Type type, int amount);

    IDummyBuilder<T> Build<T>();
    IDummy Customize(params ICustomization[] customizations);
    IDummy Customize(IEnumerable<ICustomization> customizations);

    /// <summary>
    /// Excludes the specified values from being generated for the specified enum type.
    /// </summary>
    IDummy Exclude<TEnum>(params TEnum[] values) where TEnum : Enum;

    IDummy Exclude<TEnum>(IEnumerable<TEnum> values) where TEnum : Enum;
}

public sealed class Dummy : IDummy
{
    public static DummyOptions GlobalOptions { get; } = new();

    private readonly List<long> _generatedNumbers = new();

    internal List<ICustomization> Customizations { get; } = new();

    internal readonly Dictionary<Type, List<object>> EnumExclusions = new();

    public IDummyNumberBuilder Number => new DummyNumberBuilder(this);

    public IDummyDateTimeBuilder Date => new DummyDateTimeBuilder(this);

    public IDummyStringBuilder String => new DummyStringBuilder(this);

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

        var results = new List<object>();
        for (var i = 0; i < amount; i++)
            results.Add(Create(type));
        return results;
    }

    public IDummyBuilder<T> Build<T>() => new DummyBuilder<T>(this);

    public IDummy Customize(params ICustomization[] customizations) => Customize(customizations as IEnumerable<ICustomization>);

    public IDummy Customize(IEnumerable<ICustomization> customizations)
    {
        if (customizations is null) throw new ArgumentNullException(nameof(customizations));
        Customizations.AddRange(customizations);
        return this;
    }

    public IDummy Exclude<TEnum>(params TEnum[] values) where TEnum : Enum => Exclude(values as IEnumerable<TEnum>);

    public IDummy Exclude<TEnum>(IEnumerable<TEnum> values) where TEnum : Enum
    {
        var exclusions = EnumExclusions.TryGetValue(typeof(TEnum), out var list) ? list : [];
        foreach (var value in values)
            exclusions.Add(value);
        EnumExclusions[typeof(TEnum)] = exclusions;
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