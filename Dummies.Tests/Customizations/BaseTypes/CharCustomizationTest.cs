namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class CharCustomizationTest : Tester
{
    private const string LatinAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    [TestMethod]
    public void Always_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<char>();

        //Assert
        result.Should().BeOneOf(LatinAlphabet);
    }

    [TestMethod]
    public void WhenCreateMany_ShouldReturnMultipleDifferentResults()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<char>(15).ToList();

        //Assert
        result.Distinct().Should().HaveCountGreaterThan(1);
    }
}