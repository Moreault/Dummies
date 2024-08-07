﻿namespace ToolBX.Dummies.Generation;

internal static class DynamicObjectGenerator
{
    private static readonly ProxyGenerator ProxyGenerator = new();

    public static object From(Type type)
    {
        if (type is null) throw new ArgumentNullException(nameof(type));

        var interceptor = new UniversalInterceptor();
        if (type.IsInterface)
        {
            return ProxyGenerator.CreateInterfaceProxyWithoutTarget(type, interceptor);
        }
        return ProxyGenerator.CreateClassProxy(type, interceptor);
    }
}