namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ArrayListCustomization : CustomizationBase
{
    protected override IEnumerable<Type> Types => [typeof(ArrayList), typeof(IEnumerable), typeof(IList), typeof(ICollection)];

    [RequiresUnreferencedCode("Customization uses reflection to construct objects.")]
    [RequiresDynamicCode("Customization may require runtime code generation.")]
    protected override IDummyBuilder BuildMe(IDummy dummy, Type type)
    {
        return dummy.Build<object>().FromFactory(() =>
        {
            var arrayList = new ArrayList();
            arrayList.AddRange(dummy.CreateMany<object>().ToList());
            return arrayList;
        });
    }
}