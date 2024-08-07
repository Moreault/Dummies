﻿namespace ToolBX.Dummies.Generation;

internal sealed class UniversalInterceptor : IInterceptor
{
    private readonly ConcurrentDictionary<string, object> _propertyValues = new();

    public void Intercept(IInvocation invocation)
    {
        var methodName = invocation.Method.Name;

        if (methodName.StartsWith("get_", StringComparison.OrdinalIgnoreCase))
        {
            var propertyName = methodName[4..];
            if (_propertyValues.TryGetValue(propertyName, out var value))
            {
                invocation.ReturnValue = value;
            }
            else
            {
                var returnType = invocation.Method.ReturnType;
                var defaultValue = returnType.GetDefaultValue();
                _propertyValues[propertyName] = defaultValue;
                invocation.ReturnValue = defaultValue;
            }
        }
        else if (methodName.StartsWith("set_", StringComparison.OrdinalIgnoreCase))
        {
            var propertyName = methodName[4..];
            _propertyValues[propertyName] = invocation.Arguments[0];
        }
        else if (invocation.Method.IsAbstract && invocation.Method.ReturnType != typeof(void))
        {
            invocation.ReturnValue = invocation.Method.ReturnType.GetDefaultValue();
        }
        else if (invocation.Method.IsAbstract && invocation.Method.ReturnType == typeof(void))
        {
            // Do nothing
        }
        else
        {
            invocation.Proceed();
        }
    }
}