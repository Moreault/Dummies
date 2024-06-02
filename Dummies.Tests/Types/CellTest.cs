namespace Dummies.Tests.Types;

[TestClass]
public sealed class CellTest : Tester
{
    //Taken from ToolBX.Collections
    public readonly record struct Cell<T>
    {
        public Vector2<int> Index { get; init; }
        public T? Value { get; init; }

        public int X => Index.X;
        public int Y => Index.Y;

        public Cell(int x, int y, T? value) : this(new Vector2<int>(x, y), value)
        {

        }

        public Cell(Vector2<int> index, T? value)
        {
            Index = index;
            Value = value;
        }

        public void Deconstruct(out Vector2<int> index, out T? value)
        {
            index = Index;
            value = Value;
        }
    }

    public sealed record Garbage
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
    }

    [TestMethod]
    public void BuildWith_WhenIsCell_ReturnExpectedValues()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Cell<Garbage>>().With(x => x.Index, new Vector2<int>(Dummy.Number.Between(-5, 5).Create(), Dummy.Number.Between(-5, 5).Create())).Create();

        //Assert
        result.X.Should().BeInRange(-5, 5);
        result.Y.Should().BeInRange(-5, 5);
    }
}