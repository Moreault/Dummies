namespace Dummies.Sample.Tests;

[TestClass]
public sealed class MyGenericTypeTests : Tester
{
    [TestMethod]
    public void Create_WhenTypeIsString_IdShouldBeNumericString()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IMyGenericType<string>>();

        //Assert
        result.Id.ToIntOrThrow().Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void Create_WhenTypeIsGuid_IdShouldJustBeAGuid()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IMyGenericType<Guid>>();

        //Assert
        result.Id.Should().NotBeEmpty();
    }
}