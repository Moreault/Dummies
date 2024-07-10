namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class StringCustomizationTests : CustomizationTester<StringCustomization>
{
    [TestMethod]
    public void Always_CreateNonEmpty()
    {
        //Arrange

        //Act
        var result = Dummy.Create<string>();

        //Assert
        result.Should().NotBeEmpty();
    }

    [TestMethod]
    public void WhenCreateMany_ShouldAllBeDifferent()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<string>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}