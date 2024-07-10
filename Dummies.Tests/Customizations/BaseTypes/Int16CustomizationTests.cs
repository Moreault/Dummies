namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class Int16CustomizationTests : CustomizationTester<Int16Customization>
{
    [TestMethod]
    public void WhenInt32_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<short>();

        //Assert
        result.Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void WhenCreateManyInt32_ShouldAllBeDifferent()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<short>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}