namespace ToolBX.Dummies.Exceptions;

/// <summary>
/// Thrown when Dummy fails to automatically instantiate an object instance from its type.
/// </summary>
public sealed class InstantiationException(Type type) : Exception(string.Format(ExceptionMessages.Instantiation, type?.GetHumanReadableName() ?? "NULL TYPE"));