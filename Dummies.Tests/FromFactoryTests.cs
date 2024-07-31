namespace Dummies.Tests;

[TestClass]
public sealed class FromFactoryTests : Tester
{
    public sealed record Garbage
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }

    [TestMethod]
    public void WhenFactoryIsNull_Throw()
    {
        //Arrange
        Func<Garbage> factory = null!;

        //Act
        var action = () => Dummy.Build<Garbage>().FromFactory(factory);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(factory));
    }

    [TestMethod]
    public void WhenOptionsAreOmitted_OmitAutoPropertiesByDefault()
    {
        //Arrange
        var expected = Dummy.Create<Garbage>();

        //Act
        var result = Dummy.Build<Garbage>().FromFactory(() => expected with {}).Create();

        //Assert
        result.Should().Be(expected);
    }

    [TestMethod]
    public void WhenOptionsAreNotSetToOmit_DoesNotOmitAutoProperties()
    {
        //Arrange
        var notExpected = Dummy.Create<Garbage>();

        //Act
        var result = Dummy.Build<Garbage>().FromFactory(() => notExpected with {}, new FactoryOptions { OmitAutoProperties = false }).Create();

        //Assert
        result.Should().NotBe(notExpected);
    }
}