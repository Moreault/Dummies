namespace ToolBX.Dummies.Generation;

internal static class DynamicObjectGenerator
{
    private static readonly ProxyGenerator ProxyGenerator = new();

    [RequiresDynamicCode("Castle.Core emits IL at runtime to generate proxies.")]
    [RequiresUnreferencedCode("Castle.Core uses reflection to generate proxies.")]
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