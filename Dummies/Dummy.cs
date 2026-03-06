namespace ToolBX.Dummies;

public interface IDummy
{
    DummyOptions Options { get; }

    /// <summary>
    /// Generates custom numbers bypassing all number <see cref="ICustomization"/>s.
    /// </summary>
    IDummyNumberBuilder Number { get; }

    /// <summary>
    /// Generates custom dates bypassing all DateTime <see cref="ICustomization"/>s.
    /// </summary>
    IDummyDateTimeBuilder Date { get; }

    /// <summary>
    /// Generates custom strings bypassing all string <see cref="ICustomization"/>s.
    /// </summary>
    IDummyStringBuilder String { get; }

    /// <summary>
    /// Generates custom file names bypassing all string <see cref="ICustomization"/>s.
    /// </summary>
    IDummyFileNameBuilder FileName { get; }

    /// <summary>
    /// Generates custom paths bypassing all string <see cref="ICustomization"/>s.
    /// </summary>
    IDummyPathBuilder Path { get; }

    /// <summary>
    /// Generates custom enum values.
    /// </summary>
    IDummyEnumBuilder<T> Enum<T>() where T : Enum;

    T Create<T>();
    object Create(Type type);
    IEnumerable<T> CreateMany<T>();
    IEnumerable<T> CreateMany<T>(int amount);
    IEnumerable<object> CreateMany(Type type);
    IEnumerable<object> CreateMany(Type type, int amount);

    IEnumerable<T> CreateDistinct<T>(int amount);

    IDummyBuilder<T> Build<T>();
    IDummy Customize(params ICustomization[] customizations);
    IDummy Customize(IEnumerable<ICustomization> customizations);

    /// <summary>
    /// Excludes the specified values from being generated for the specified enum type.
    /// </summary>
    IDummy Exclude<TEnum>(params TEnum[] values) where TEnum : Enum;

    /// <summary>
    /// Excludes the specified values from being generated for the specified enum type.
    /// </summary>
    IDummy Exclude<TEnum>(IEnumerable<TEnum> values) where TEnum : Enum;

    /// <summary>
    /// Using <see cref="Create"/> on type <see cref="T"/> will always return the registered instance.
    /// </summary>
    void Register<T>(T? instance);

    /// <summary>
    /// Creates a <see cref="T"/> and registers it so that all subsequent calls to <see cref="Create{T}"/> return the same instance.
    /// </summary>
    T Freeze<T>();
}

public sealed class Dummy : IDummy
{
    private readonly HashSet<long> _generatedNumbers = [];

    internal List<ICustomization> Customizations { get; } = [];

    internal readonly Dictionary<Type, List<object>> EnumExclusions = new();

    private readonly Dictionary<Type, object?> _registered = new();

    public DummyOptions Options { get; } = new();

    public Dummy()
    {
        _number = new Lazy<IDummyNumberBuilder>(() => new DummyNumberBuilder(this));
        _date = new Lazy<IDummyDateTimeBuilder>(() => new DummyDateTimeBuilder(this));
        _string = new Lazy<IDummyStringBuilder>(() => new DummyStringBuilder(this));
        _fileName = new Lazy<IDummyFileNameBuilder>(() => new DummyFileNameBuilder(this));
        _path = new Lazy<IDummyPathBuilder>(() => new DummyPathBuilder(this));
    }

    private readonly Lazy<IDummyNumberBuilder> _number;
    private readonly Lazy<IDummyDateTimeBuilder> _date;
    private readonly Lazy<IDummyStringBuilder> _string;
    private readonly Lazy<IDummyFileNameBuilder> _fileName;
    private readonly Lazy<IDummyPathBuilder> _path;

    public IDummyNumberBuilder Number => _number.Value;

    public IDummyDateTimeBuilder Date => _date.Value;

    public IDummyStringBuilder String => _string.Value;

    public IDummyFileNameBuilder FileName => _fileName.Value;

    public IDummyPathBuilder Path => _path.Value;

    public IDummyEnumBuilder<T> Enum<T>() where T : Enum => new DummyEnumBuilder<T>(this);

    public T Create<T>()
    {
        if (_registered.TryGetValue(typeof(T), out var value))
            return (T)value!;

        return Build<T>().Create();
    }

    internal T Create<T>(int currentDepth)
    {
        if (_registered.TryGetValue(typeof(T), out var value))
            return (T)value!;
        return new DummyBuilder<T>(this, currentDepth).Create();
    }

    private static readonly ConcurrentDictionary<Type, MethodInfo> _createMethodCache = new();

    private static MethodInfo GetOrCreateGenericMethod(Type type)
    {
        return _createMethodCache.GetOrAdd(type, t =>
            typeof(Dummy).GetSingleMethod(x => x.Name == nameof(Create) && x.IsInternal() && x.ContainsGenericParameters).MakeGenericMethod(t));
    }

    public object Create(Type type) => Create(type, 0);

    internal object Create(Type type, int currentDepth)
    {
        if (type is null) throw new ArgumentNullException(nameof(type));
        return GetOrCreateGenericMethod(type).Invoke(this, [currentDepth])!;
    }

    public IEnumerable<T> CreateMany<T>() => CreateMany<T>(Options.DefaultCollectionSize);

    public IEnumerable<T> CreateMany<T>(int amount)
    {
        if (_registered.TryGetValue(typeof(T), out var value))
            return Enumerable.Repeat((T)value!, amount);

        return Build<T>().CreateMany(amount);
    }

    internal IEnumerable<T> CreateMany<T>(int amount, int currentDepth) => new DummyBuilder<T>(this, currentDepth).CreateMany(amount);

    public IEnumerable<object> CreateMany(Type type) => CreateMany(type, Options.DefaultCollectionSize);

    public IEnumerable<object> CreateMany(Type type, int amount) => CreateMany(type, amount, 0);

    internal IEnumerable<object> CreateMany(Type type, int amount, int currentDepth)
    {
        if (type is null) throw new ArgumentNullException(nameof(type));
        ArgumentOutOfRangeException.ThrowIfNegative(amount, nameof(amount));

        var results = new List<object>();
        for (var i = 0; i < amount; i++)
            results.Add(Create(type, currentDepth));
        return results;
    }

    public IEnumerable<T> CreateDistinct<T>(int amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount, nameof(amount));

        var results = new HashSet<T>();
        var maxAttempts = amount * Options.UniqueGenerationAttempts;
        var attempts = 0;
        while (results.Count < amount)
        {
            if (attempts++ >= maxAttempts)
                throw new InvalidOperationException($"Could not generate {amount} distinct values of type {typeof(T).GetHumanReadableName()} after {maxAttempts} attempts.");
            results.Add(Create<T>());
        }
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
        exclusions.AddRange(values.Cast<object>());
        EnumExclusions[typeof(TEnum)] = exclusions;
        return this;
    }

    public void Register<T>(T? instance) => _registered[typeof(T)] = instance;

    public T Freeze<T>()
    {
        var instance = Create<T>();
        Register(instance);
        return instance;
    }

    internal bool TryGenerate<T>(T value) where T : INumber<T>
    {
        var value64 = long.CreateSaturating(value);
        if (_generatedNumbers.Contains(value64)) 
            return false;

        _generatedNumbers.Add(value64);
        return true;
    }


}