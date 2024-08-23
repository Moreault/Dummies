namespace ToolBX.Dummies;

public interface IDummyEnumBuilder<T> where T : Enum
{
    IDummyEnumBuilder<T> OneOf(params T[] values);
    IDummyEnumBuilder<T> OneOf(IEnumerable<T> values);
    IDummyEnumBuilder<T> Exclude(params T[] values);
    IDummyEnumBuilder<T> Exclude(IEnumerable<T> values);

    T Create();
    IEnumerable<T> CreateMany();
    IEnumerable<T> CreateMany(int amount);
}

internal sealed class DummyEnumBuilder<T> : IDummyEnumBuilder<T> where T : Enum
{
    private Func<T> _generator;

    private readonly IDummy _dummy;

    internal DummyEnumBuilder(IDummy dummy)
    {
        _dummy = dummy ?? throw new ArgumentNullException(nameof(dummy));
        _generator = () => _dummy.Create<T>();
    }

    public IDummyEnumBuilder<T> OneOf(params T[] values) => OneOf(values as IEnumerable<T>);

    public IDummyEnumBuilder<T> OneOf(IEnumerable<T> values)
    {
        if (values.IsNullOrEmpty()) 
            throw new ArgumentException("Values should be emtpy", nameof(values));
        _generator = () => values.GetRandom();
        return this;
    }

    public IDummyEnumBuilder<T> Exclude(params T[] values) => Exclude(values as IEnumerable<T>);

    public IDummyEnumBuilder<T> Exclude(IEnumerable<T> values)
    {
        _generator = () => EnumUtils.ToList<T>().Where(x => !values.Contains(x)).GetRandom();
        return this;
    }

    public T Create() => _generator.Invoke();

    public IEnumerable<T> CreateMany() => CreateMany(_dummy.Options.DefaultCollectionSize);

    public IEnumerable<T> CreateMany(int amount)
    {
        for (var i = 0; i < amount; i++)
            yield return Create();
    }
}