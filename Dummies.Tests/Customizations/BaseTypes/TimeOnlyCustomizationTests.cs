namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class TimeOnlyCustomizationTests : CustomizationTester<TimeOnlyCustomization>
{
    [TestMethod]
    public void Always_Generate()
    {
        //Arrange

        //Act
        var result = Dummy.Create<TimeOnly>();

        //Assert
        result.Should().NotBeCloseTo(default, TimeSpan.FromSeconds(100));
    }

    [TestMethod]
    public void WhenCreateMany_DoNotGenerateDuplicates()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<TimeOnly>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}