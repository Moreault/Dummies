namespace ToolBX.Dummies;

public interface IDummyStringBuilder
{
    IDummyStringLengthBuilder WithLength { get; }

    string Create();
    IEnumerable<string> CreateMany();
    IEnumerable<string> CreateMany(int amount);
}

public interface IDummyStringLengthBuilder
{
    IDummyStringBuilder Exactly(int value);
    IDummyStringBuilder Between(int min, int max);
    IDummyStringBuilder LessThan(int value);
    IDummyStringBuilder LessThanOrEqualTo(int value);
}

internal sealed class DummyStringBuilder : IDummyStringBuilder
{
    private readonly Dummy _dummy;

    internal Func<int> Length = () => 24;

    private const string LatinAlphabet = Characters.Letters + Characters.Numbers;

    internal DummyStringBuilder(Dummy dummy)
    {
        _dummy = dummy;
    }

    public IDummyStringLengthBuilder WithLength => new DummyStringLengthBuilder(this);

    public string Create()
    {
        var length = Length();
        var sb = new StringBuilder();
        for (var i = 0; i < length; i++)
        {
            sb.Append(LatinAlphabet.GetRandom());
        }
        return sb.ToString();
    }

    public IEnumerable<string> CreateMany() => CreateMany(Dummy.GlobalOptions.DefaultCollectionSize);

    public IEnumerable<string> CreateMany(int amount)
    {
        for (var i = 0; i < amount; i++)
            yield return Create();
    }
}

internal sealed class DummyStringLengthBuilder : IDummyStringLengthBuilder
{
    private readonly DummyStringBuilder _stringBuilder;

    internal DummyStringLengthBuilder(DummyStringBuilder stringBuilder)
    {
        _stringBuilder = stringBuilder;
    }

    public IDummyStringBuilder Exactly(int value)
    {
        _stringBuilder.Length = () => value;
        return _stringBuilder;
    }

    public IDummyStringBuilder Between(int min, int max)
    {
        _stringBuilder.Length = () => PseudoRandomNumberGenerator.Shared.Generate(min, max);
        return _stringBuilder;
    }

    public IDummyStringBuilder LessThan(int value) => Between(0, value - 1);

    public IDummyStringBuilder LessThanOrEqualTo(int value) => Between(0, value);
}