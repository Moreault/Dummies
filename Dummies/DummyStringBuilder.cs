namespace ToolBX.Dummies;

public interface IDummyStringBuilder : ISpecializedBuilder<string>
{
    IDummyStringLengthBuilder WithLength { get; }
}

public interface IDummyStringLengthBuilder
{
    IDummyStringBuilder Exactly(int value);
    IDummyStringBuilder Between(int min, int max);
    IDummyStringBuilder LessThan(int value);
    IDummyStringBuilder LessThanOrEqualTo(int value);
}

internal sealed class DummyStringBuilder : SpecializedBuilder<string>, IDummyStringBuilder
{
    internal Func<int> Length = () => 24;

    internal DummyStringBuilder(IDummy dummy) : base(dummy)
    {
    }

    public IDummyStringLengthBuilder WithLength => new DummyStringLengthBuilder(this);

    public override string Create() => RandomStringGenerator.Generate(Length());
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