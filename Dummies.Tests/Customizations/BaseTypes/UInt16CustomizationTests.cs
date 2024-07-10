namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class UInt16CustomizationTests : CustomizationTester<UInt16Customization>
{
    [TestMethod]
    public void WhenInt32_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<ushort>();

        //Assert
        result.Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void WhenCreateManyInt32_ShouldAllBeDifferent()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<ushort>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}