namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class ByteCustomizationTests : Tester
{
    [TestMethod]
    public void WhenCreate_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<byte>();

        //Assert
        result.Should().BeGreaterThan(1);
    }

    [TestMethod]
    public void WhenCreateMany_ReturnMany()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<byte>().ToList();

        //Assert
        result.Should().HaveCount(3);
    }
}