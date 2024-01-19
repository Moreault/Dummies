using System.Collections;

namespace ToolBX.Dummies.Customizations.Collections;

[AutoCustomization]
public sealed class ArrayListCustomization : ICustomization
{
    public IEnumerable<Type> Types { get; } = [typeof(ArrayList), typeof(IEnumerable), typeof(IList), typeof(ICollection)];

    public IDummyBuilder Build(Dummy dummy, Type type)
    {
        if (dummy is null) throw new ArgumentNullException(nameof(dummy));
        if (type is null) throw new ArgumentNullException(nameof(type));

        return dummy.Build<object>().FromFactory(() =>
        {
            var arrayList = new ArrayList();
            arrayList.AddRange(dummy.CreateMany<object>().ToList());
            return arrayList;
        });
    }
}