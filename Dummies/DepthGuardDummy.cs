namespace ToolBX.Dummies;

internal sealed class DepthGuardDummy : IDummy
{
    private readonly Dummy _dummy;
    internal int CurrentDepth { get; }

    internal IReadOnlyList<ICustomization> Customizations => _dummy.Customizations;
    internal IReadOnlyDictionary<Type, List<object>> EnumExclusions => _dummy.EnumExclusions;

    public DummyOptions Options => _dummy.Options;
    public IDummyNumberBuilder Number => _dummy.Number;
    public IDummyDateTimeBuilder Date => _dummy.Date;
    public IDummyStringBuilder String => _dummy.String;
    public IDummyFileNameBuilder FileName => _dummy.FileName;
    public IDummyPathBuilder Path => _dummy.Path;

    internal DepthGuardDummy(Dummy dummy, int currentDepth)
    {
        _dummy = dummy ?? throw new ArgumentNullException(nameof(dummy));
        CurrentDepth = currentDepth;
    }

    public IDummyEnumBuilder<T> Enum<T>() where T : Enum => _dummy.Enum<T>();

    public T Create<T>() => _dummy.Create<T>(CurrentDepth);

    public object Create(Type type) => _dummy.Create(type, CurrentDepth);

    public IEnumerable<T> CreateMany<T>() => _dummy.CreateMany<T>(_dummy.Options.DefaultCollectionSize, CurrentDepth);

    public IEnumerable<T> CreateMany<T>(int amount) => _dummy.CreateMany<T>(amount, CurrentDepth);

    public IEnumerable<object> CreateMany(Type type) => _dummy.CreateMany(type, _dummy.Options.DefaultCollectionSize, CurrentDepth);

    public IEnumerable<object> CreateMany(Type type, int amount) => _dummy.CreateMany(type, amount, CurrentDepth);

    public IDummyBuilder<T> Build<T>() => new DummyBuilder<T>(_dummy, CurrentDepth);

    public IDummy Customize(params ICustomization[] customizations) => _dummy.Customize(customizations);

    public IDummy Customize(IEnumerable<ICustomization> customizations) => _dummy.Customize(customizations);

    public IDummy Exclude<TEnum>(params TEnum[] values) where TEnum : Enum => _dummy.Exclude(values);

    public IDummy Exclude<TEnum>(IEnumerable<TEnum> values) where TEnum : Enum => _dummy.Exclude(values);

    public void Register<T>(T? instance) => _dummy.Register(instance);

    public DepthGuardDummy Deeper() => new(_dummy, CurrentDepth + 1);
}