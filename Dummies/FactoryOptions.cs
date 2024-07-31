namespace ToolBX.Dummies;

public sealed record FactoryOptions
{
    public static FactoryOptions Default { get; } = new();

    /// <summary>
    /// If true, unspecified properties with public setters and fields will be set automatically with dummy values.
    /// By default, auto properties are omitted when using a factory method.
    /// </summary>
    public bool OmitAutoProperties { get; init; } = true;
}