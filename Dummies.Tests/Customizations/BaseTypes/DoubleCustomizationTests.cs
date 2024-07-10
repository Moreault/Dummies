namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class DoubleCustomizationTests : CustomizationTester<DoubleCustomization>
{
    [TestMethod]
    public void Always_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<double>();

        //Assert
        result.Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void WhenCreateMany_ShouldAllBeDifferent()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<double>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }

    [TestMethod]
    public void Always_ShouldHaveFloatingPoints()
    {
        //Arrange

        //Act
        var result = Dummy.Create<double>();

        //Assert
        (result % 1 == 0).Should().BeFalse();
    }
}