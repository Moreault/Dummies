namespace Dummies.Tests;

[TestClass]
public sealed class RegisterTests : Tester
{
    public sealed record Garbage
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }

    public sealed record GarbageParent
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public Garbage Child { get; init; } = null!;
    }

    public sealed record GarbageChild
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public ImmutableList<Garbage> Children { get; init; } = ImmutableList<Garbage>.Empty;
    }

    public sealed record VeryGarbageParent
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public GarbageChild Child { get; init; } = null!;
    }

    [TestMethod]
    public void Create_WhenTypeIsRegistered_ReturnSameObject()
    {
        //Arrange
        Dummy.Register(new Garbage
        {
            Id = 44,
            Name = "Roger"
        });

        //Act
        var result = Dummy.Create<Garbage>();

        //Assert
        result.Should().Be(new Garbage
        {
            Id = 44,
            Name = "Roger"
        });
    }

    [TestMethod]
    public void CreateMany_WhenTypeIsRegistered_AlwaysReturnSameObject()
    {
        //Arrange
        Dummy.Register(new Garbage
        {
            Id = 44,
            Name = "Roger"
        });

        //Act
        var result = Dummy.CreateMany<Garbage>();

        //Assert
        result.Should().OnlyContain(x => x == new Garbage
        {
            Id = 44,
            Name = "Roger"
        });
    }

    [TestMethod]
    public void CreateMany_WhenTypeIsNotRegistered_AlwaysReturnSomethingDifferent()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<Garbage>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }

    [TestMethod]
    public void CreateMany_WhenTypIsRegisteredButBuildIsUsed_AlwaysReturnSomethingDifferent()
    {
        //Arrange
        Dummy.Register(new Garbage
        {
            Id = 44,
            Name = "Roger"
        });

        //Act
        var result = Dummy.Build<Garbage>().CreateMany().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }

    [TestMethod]
    public void Create_WhenWhenTypeIsRegisteredButOnChild_AlwaysReturnRegisteredInstance()
    {
        //Arrange
        Dummy.Register(new Garbage
        {
            Id = 44,
            Name = "Roger"
        });

        //Act
        var result = Dummy.Create<GarbageParent>();

        //Assert
        result.Child.Should().Be(new Garbage
        {
            Id = 44,
            Name = "Roger"
        });
    }

    [TestMethod]
    public void Create_WhenWhenTypeIsRegisteredButOnCollectionOnChild_AlwaysReturnRegisteredInstance()
    {
        //Arrange
        Dummy.Register(new Garbage
        {
            Id = 44,
            Name = "Roger"
        });

        //Act
        var result = Dummy.Create<VeryGarbageParent>();

        //Assert
        result.Child.Children.Should().OnlyContain(x => x == new Garbage
        {
            Id = 44,
            Name = "Roger"
        });
    }
}