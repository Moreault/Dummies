namespace Dummies.Tests;

[TestClass]
public class DummyTests : Tester
{
    [TestMethod]
    public void WhenCreateInterfaceFromDotNetFramework_CreateProxyImplementingInterface()
    {
        //Arrange
        
        //Act
        var result = Dummy.Create<IFormatProvider>();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IFormatProvider));
    }

    [TestMethod]
    public void WhenCreateNonGeneric_ReturnAsObject()
    {
        //Arrange

        //Act
        var result = Dummy.Create(typeof(string));

        //Assert
        result.Should().BeOfType<string>();
    }

    public sealed record BogusType
    {
        public string A { get; init; }
        public int B { get; init; }
        public int C { get; init; }
        public char D { get; init; }
    }

    [TestMethod]
    public void WhenUsingBuild_OverrideDefaultBehavior()
    {
        //Arrange

        //Act
        var result = Dummy.Build<BogusType>().With(x => x.A, "Hello").Create();

        //Assert
        result.A.Should().Be("Hello");
    }
}