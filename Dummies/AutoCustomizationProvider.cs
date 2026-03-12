namespace ToolBX.Dummies;

[RequiresUnreferencedCode("AutoCustomizationProvider uses reflection to get AutoCustomizations from assemblies.")]
internal sealed class AutoCustomizationProvider
{
    private static readonly ImmutableList<ICustomization> BuiltInCustomizations = ImmutableList.Create<ICustomization>(
    new BigIntegerCustomization(),
    new BoolCustomization(),
    new ByteCustomization(),
    new CharCustomization(),
    new DateOnlyCustomization(),
    new DateTimeCustomization(),
    new DateTimeOffsetCustomization(),
    new DecimalCustomization(),
    new DoubleCustomization(),
    new FloatCustomization(),
    new GuidCustomization(),
    new Int16Customization(),
    new Int32Customization(),
    new Int64Customization(),
    new IpAddressCustomization(),
    new SByteCustomization(),
    new StringCustomization(),
    new TimeOnlyCustomization(),
    new TimeSpanCustomization(),
    new UInt16Customization(),
    new UInt32Customization(),
    new UInt64Customization(),
    new ArrayCustomization(),
    new ArrayListCustomization(),
    new DictionaryCustomization(),
    new GenericStackCustomization(),
    new ImmutableArrayCustomization(),
    new ImmutableDictionaryCustomization(),
    new ImmutableListCustomization(),
    new ListCustomization(),
    new ActionCustomization(),
    new EqualityComparerCustomization(),
    new FuncCustomization()
);

    internal static ImmutableList<ICustomization> AutoCustomizations => BuiltInCustomizations.AddRange(UserCustomizations.Value);

    private static readonly Lazy<ImmutableList<ICustomization>> UserCustomizations = new(() =>
        GetSafeTypes()
        .Where(x => x.Assembly != typeof(Dummy).Assembly
            && x.HasAttribute<AutoCustomizationAttribute>()
            && !x.IsAbstract && x.Implements<ICustomization>())
        .Select(x => (ICustomization)Activator.CreateInstance(x)!)
        .ToImmutableList());

    private static IEnumerable<Type> GetSafeTypes()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(t => t is not null).ToArray()!;
            }
            catch
            {
                continue;
            }

            foreach (var type in types)
                yield return type;
        }
    }
}
