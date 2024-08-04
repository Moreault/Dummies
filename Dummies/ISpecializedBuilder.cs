namespace ToolBX.Dummies;

public interface ISpecializedBuilder<out T>
{
    /// <summary>
    /// Generates a single <see cref="T"/>.
    /// </summary>
    T Create();

    /// <summary>
    /// Generates multiple <see cref="T"/> based on the <see cref="DummyOptions.DefaultCollectionSize"/> value.
    /// </summary>
    IEnumerable<T> CreateMany();

    /// <summary>
    /// Generates a specific amount of <see cref="T"/>.
    /// </summary>
    IEnumerable<T> CreateMany(int amount);
}

internal abstract class SpecializedBuilder<T> : ISpecializedBuilder<T>
{
    private readonly IDummy _dummy;

    public abstract T Create();

    internal SpecializedBuilder(IDummy dummy)
    {
        _dummy = dummy ?? throw new ArgumentNullException(nameof(dummy));
    }

    public IEnumerable<T> CreateMany() => CreateMany(_dummy.Options.DefaultCollectionSize);

    public IEnumerable<T> CreateMany(int amount)
    {
        for (var i = 0; i < amount; i++)
            yield return Create();
    }
}