namespace Dummies.Sample.Code;

public interface IMyGenericType<T>
{
    T Id { get; }
}

public sealed class MyGenericType<T> : IMyGenericType<T>
{
    public T Id { get; init; } = default!;
}