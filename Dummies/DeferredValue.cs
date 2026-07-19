namespace ToolBX.Dummies;

internal interface IDeferredValue
{
    object? Evaluate();
}

internal sealed class DeferredValue<T>(Func<T> factory) : IDeferredValue
{
    public object? Evaluate() => factory();
}
