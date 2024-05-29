﻿namespace ToolBX.Dummies;

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
    internal IReadOnlyList<long> GeneratedNumbers => _generatedNumbers;
    private readonly List<long> _generatedNumbers = new();

    internal List<ICustomization> Customizations { get; } = new();

    internal readonly Dictionary<Type, List<object>> EnumExclusions = new();

    public IDummyNumberBuilder Number => new DummyNumberBuilder(this);

    public IDummyDateTimeBuilder Date => new DummyDateTimeBuilder(this);

    public IDummyStringBuilder String => new DummyStringBuilder(this);

    public T Create<T>() => Build<T>().Create();

    internal T Create<T>(int currentDepth) => new DummyBuilder<T>(this, currentDepth).Create();

    public object Create(Type type) => Create(type, 0);

    internal object Create(Type type, int currentDepth)
    {
        if (type is null) throw new ArgumentNullException(nameof(type));
        return typeof(Dummy).GetSingleMethod(x => x.Name == nameof(Create) && x.IsInternal() && x.ContainsGenericParameters).MakeGenericMethod(type).Invoke(this, [currentDepth])!;
    }

    public IEnumerable<T> CreateMany<T>() => CreateMany<T>(DummyOptions.Global.DefaultCollectionSize);

    public IEnumerable<T> CreateMany<T>(int amount) => Build<T>().CreateMany(amount);

    internal IEnumerable<T> CreateMany<T>(int amount, int currentDepth) => new DummyBuilder<T>(this, currentDepth).CreateMany(amount);

    public IEnumerable<object> CreateMany(Type type) => CreateMany(type, DummyOptions.Global.DefaultCollectionSize);

    public IEnumerable<object> CreateMany(Type type, int amount) => CreateMany(type, amount, 0);

    internal IEnumerable<object> CreateMany(Type type, int amount, int currentDepth)
    {
        if (type is null) throw new ArgumentNullException(nameof(type));

        var results = new List<object>();
        for (var i = 0; i < amount; i++)
            results.Add(Create(type, currentDepth));
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

    internal bool TryGenerate<T>(T value) where T : INumber<T>
    {
        var value64 = long.CreateSaturating(value);
        if (!_generatedNumbers.Contains(value64))
        {
            _generatedNumbers.Add(value64);
            return true;
        }
        return false;
    }
}