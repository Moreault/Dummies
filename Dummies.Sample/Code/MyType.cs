namespace Dummies.Sample.Code;

public interface IMyType
{
    string Id { get; }
    int YearsActive { get; }
}

public sealed class MyType : IMyType
{
    public string Id { get; init; } = null!;
    public int YearsActive { get; init; }
}