namespace MoqDummy;

public class MockDummy : IDummy
{
    private readonly IDummy _dummy;

    public DummyOptions Options => _dummy.Options;
    public IDummyNumberBuilder Number => _dummy.Number;
    public IDummyDateTimeBuilder Date => _dummy.Date;
    public IDummyStringBuilder String => _dummy.String;
    public IDummyFileNameBuilder FileName => _dummy.FileName;
    public IDummyPathBuilder Path => _dummy.Path;

    internal readonly Dictionary<Type, Mock> Mocks;

    public MockDummy()
    {
        _dummy = new Dummy();
        _dummy.Options.AbstractTypeGenerator = new MoqAbstractTypeGenerator();
        Mocks = new();
    }

    public Mock<T> GetMock<T>() where T : class
    {
        var isSuccess = Mocks.TryGetValue(typeof(T), out var mock);
        if (!isSuccess) throw new Exception($"There is no mock associated with type {typeof(T)}");
        return mock!.As<T>();
    }

    public T Create<T>() => _dummy.Create<T>();

    public object Create(Type type) => _dummy.Create(type);

    public IEnumerable<T> CreateMany<T>() => _dummy.CreateMany<T>();

    public IEnumerable<T> CreateMany<T>(int amount) => _dummy.CreateMany<T>(amount);

    public IEnumerable<object> CreateMany(Type type) => _dummy.CreateMany(type);

    public IEnumerable<object> CreateMany(Type type, int amount) => _dummy.CreateMany(type, amount);

    public IDummyBuilder<T> Build<T>() => _dummy.Build<T>();

    public IDummy Customize(params ICustomization[] customizations) => _dummy.Customize(customizations);

    public IDummy Customize(IEnumerable<ICustomization> customizations) => _dummy.Customize(customizations);

    public IDummy Exclude<TEnum>(params TEnum[] values) where TEnum : Enum => _dummy.Exclude(values);

    public IDummy Exclude<TEnum>(IEnumerable<TEnum> values) where TEnum : Enum => _dummy.Exclude(values);

    public void Register<T>(T? instance) => _dummy.Register(instance);

    public IDummy Root => this;
}