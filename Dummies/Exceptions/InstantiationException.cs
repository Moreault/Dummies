namespace ToolBX.Dummies.Exceptions;

/// <summary>
/// Thrown when Dummy fails to automatically instantiate an object instance from its type.
/// </summary>
public sealed class InstantiationException : Exception
{
    public InstantiationException(Type type)
        : base(string.Format(ExceptionMessages.Instantiation, type?.GetHumanReadableName() ?? "NULL TYPE"))
    {
    }

    public InstantiationException(Type type, IReadOnlyList<Exception> constructorExceptions)
        : base(string.Format(ExceptionMessages.Instantiation, type?.GetHumanReadableName() ?? "NULL TYPE"),
            constructorExceptions.Count > 0 ? new AggregateException(constructorExceptions) : null)
    {
    }
}