namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class DateTimeCustomizationTests : Tester
{
    [TestMethod]
    public void Always_Generate()
    {
        //Arrange

        //Act
        var result = Dummy.Create<DateTime>();

        //Assert
        result.Should().NotBeCloseTo(default, TimeSpan.FromSeconds(100));
    }

    [TestMethod]
    public void WhenCreateMany_DoNotGenerateDuplicates()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<DateTime>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}