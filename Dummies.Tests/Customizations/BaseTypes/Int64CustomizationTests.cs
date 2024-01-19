namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class Int64CustomizationTests : Tester
{
    [TestMethod]
    public void WhenInt64_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<long>();

        //Assert
        result.Should().BeGreaterThan(1);
    }

    [TestMethod]
    public void WhenCreateManyInt64_ShouldAllBeDifferent()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<long>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}