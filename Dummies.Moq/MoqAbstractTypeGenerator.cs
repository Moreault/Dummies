using ToolBX.Reflection4Humans.Extensions;

namespace MoqDummy;

internal class MoqAbstractTypeGenerator : IAbstractTypeGenerator
{
    public object From(IDummy dummy, Type type)
    {
        //var methods = type.GetAllMethods(x => x.Name == nameof(From));
        return typeof(MoqAbstractTypeGenerator).GetSingleMethod(x => x.Name == nameof(From) && x.IsGenericMethod).MakeGenericMethod(type).Invoke(this, [dummy])!;
    }

    public T From<T>(IDummy dummy) where T : class
    {
        var mockDummy = (MockDummy)dummy.Root;
        var alreadyExists = mockDummy.Mocks.TryGetValue(typeof(T), out var existing);
        if (alreadyExists)
            return (T)existing!.Object;

        var mock = new Mock<T>();
        mockDummy.Mocks[typeof(T)] = mock;
        return mock.Object;
    }
}