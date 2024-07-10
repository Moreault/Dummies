namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class DateTimeOffsetCustomizationTests : CustomizationTester<DateTimeOffsetCustomization>
{
    [TestMethod]
    public void Always_Generate()
    {
        //Arrange

        //Act
        var result = Dummy.Create<DateTimeOffset>();

        //Assert
        result.Should().NotBeCloseTo(default, TimeSpan.FromSeconds(100));
    }

    [TestMethod]
    public void WhenCreateMany_DoNotGenerateDuplicates()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<DateTimeOffset>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}