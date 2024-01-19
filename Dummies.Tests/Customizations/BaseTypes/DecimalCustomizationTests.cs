namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class DecimalCustomizationTests : Tester
{
    [TestMethod]
    public void Always_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<decimal>();

        //Assert
        result.Should().BeGreaterThan(1);
    }

    [TestMethod]
    public void WhenCreateMany_ShouldAllBeDifferent()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<decimal>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }

    [TestMethod]
    public void Always_ShouldHaveFloatingPoints()
    {
        //Arrange

        //Act
        var result = Dummy.Create<decimal>();

        //Assert
        (result % 1 == 0).Should().BeFalse();
    }
}