namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ArrayListCustomization : CustomizationBase
{
    protected override IEnumerable<Type> Types => [typeof(ArrayList), typeof(IEnumerable), typeof(IList), typeof(ICollection)];

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