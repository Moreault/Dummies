namespace ToolBX.Dummies;

public interface IDummyOptions
{
    int DefaultCollectionSize { get; set; }
    int MaximumDepth { get; set; }

    /// <summary>
    /// Number of attempts before Dummy gives up trying to create a unique number.
    /// </summary>
    int UniqueGenerationAttempts { get; set; }
}

internal sealed class GlobalDummyOptions : IDummyOptions
{
    public int DefaultCollectionSize { get; set; } = 3;
    public int MaximumDepth { get; set; } = 3;

    public int UniqueGenerationAttempts
    {
        get => _uniqueGenerationAttempts;
        set => _uniqueGenerationAttempts = Math.Clamp(value, 1, int.MaxValue);
    }
    private int _uniqueGenerationAttempts;
}

public sealed class DummyOptions : IDummyOptions
{
    public static IDummyOptions Global { get; } = new GlobalDummyOptions();

    public int DefaultCollectionSize
    {
        get => _defaultCollectionSize ?? Global.DefaultCollectionSize;
        set => _defaultCollectionSize = value;
    }
    private int? _defaultCollectionSize;

    public int MaximumDepth
    {
        get => _maximumDepth ?? Global.MaximumDepth;
        set => _maximumDepth = value;
    }
    private int? _maximumDepth;

    public int UniqueGenerationAttempts
    {
        get => _uniqueGenerationAttempts ?? Global.UniqueGenerationAttempts;
        set => _uniqueGenerationAttempts = Math.Clamp(value, 1, int.MaxValue);
    }
    private int? _uniqueGenerationAttempts;

    public void Reset()
    {
        _defaultCollectionSize = null;
        _maximumDepth = null;
        _uniqueGenerationAttempts = null;
    }
}