namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class TimeSpanCustomizationTests : CustomizationTester<TimeSpanCustomization>
{
    [TestMethod]
    public void Always_Generate()
    {
        //Arrange

        //Act
        var result = Dummy.Create<TimeSpan>();

        //Assert
        result.Should().NotBeCloseTo(default, TimeSpan.FromSeconds(100));
    }

    [TestMethod]
    public void WhenCreateMany_DoNotGenerateDuplicates()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<TimeSpan>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}