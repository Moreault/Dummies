namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class DateOnlyCustomizationTests : CustomizationTester<DateOnlyCustomization>
{
    [TestMethod]
    public void Always_Generate()
    {
        //Arrange

        //Act
        var result = Dummy.Create<DateOnly>();

        //Assert
        result.Should().NotBe(default);
    }

    [TestMethod]
    public void WhenCreateMany_DoNotGenerateDuplicates()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<DateOnly>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}