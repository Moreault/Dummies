namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class UInt64CustomizationTests : CustomizationTester<UInt64Customization>
{
    [TestMethod]
    public void WhenInt32_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<ulong>();

        //Assert
        result.Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void WhenCreateManyInt32_ShouldAllBeDifferent()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<ulong>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}