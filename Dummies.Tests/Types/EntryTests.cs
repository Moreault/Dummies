namespace Dummies.Tests.Types;

[TestClass]
public sealed class EntryTests : Tester
{
    //Taken from ToolBX.Collections.Inventory
    public sealed record Entry<T> : EntryBase<T>
    {
        public Entry()
        {

        }

        public Entry(T item, int quantity = 1) : base(item, quantity)
        {

        }

        public override string ToString() => base.ToString();
    }

    public abstract record EntryBase<T>
    {
        public T? Item { get; init; }

        public int Quantity
        {
            get => _quantity;
            init => _quantity = value <= 0 ? throw new ArgumentOutOfRangeException(nameof(value), value, "Quantity must be at least one") : value;
        }
        private readonly int _quantity;

        protected EntryBase()
        {

        }

        protected EntryBase(T item, int quantity = 1)
        {
            Item = item;
            Quantity = quantity;
        }

        public void Deconstruct(out T? item, out int quantity)
        {
            item = Item;
            quantity = Quantity;
        }

        public override string ToString() => $"{(Item is null ? "NULL" : Item.ToString())} x{Quantity}";
    }

    public record Garbage
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Level { get; init; }

        public override string ToString() => Name;
    }

    [TestMethod]
    public void ToString_WhenItemIsNull_ReturnNull()
    {
        //Arrange
        var instance = Dummy.Build<Entry<Garbage>>().Without(x => x.Item).Create();

        //Act
        var result = instance.ToString();

        //Assert
        result.Should().Be($"NULL x{instance.Quantity}");
    }

    [TestMethod]
    public void ToString_WhenItemIsNotNull_ReturnItemWithQuantity()
    {
        //Arrange
        var instance = Dummy.Create<Entry<Garbage>>();

        //Act
        var result = instance.ToString();

        //Assert
        result.Should().Be($"{instance.Item} x{instance.Quantity}");
    }
}