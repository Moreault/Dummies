namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class GuidCustomizationTests : Tester
{
    [TestMethod]
    public void Always_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Guid>();

        //Assert
        result.Should().NotBeEmpty();
    }

    [TestMethod]
    public void WhenCreateMany_ShouldAllBeDifferent()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<Guid>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}