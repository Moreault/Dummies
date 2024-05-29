namespace ToolBX.Dummies;

public sealed class DummyOptions
{
    public static DummyOptions Global { get; } = new();

    public int DefaultCollectionSize { get; set; } = 3;

    public int MaximumDepth { get; set; } = 3;
}