namespace ToolBX.Dummies;

public sealed record FactoryOptions
{
    public static FactoryOptions Default { get; } = new();

    /// <summary>
    /// If true, unspecified properties with public setters and fields will be set automatically with dummy values. False by default.
    /// </summary>
    public bool UseAutoProperties { get; init; }
}