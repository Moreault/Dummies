namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class BigIntegerCustomizationTests : CustomizationTester<BigIntegerCustomization>
{
    [TestMethod]
    public void WhenCreate_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<BigInteger>();

        //Assert
        result.Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void WhenCreateMany_ReturnMany()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<BigInteger>().ToList();

        //Assert
        result.Should().HaveCount(3);
    }
}