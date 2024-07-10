namespace Dummies.Tests.Customizations.Collections;

[TestClass]
public sealed class DictionaryCustomizationTests : CustomizationTester<DictionaryCustomization>
{
    [TestMethod]
    public void Always_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Dictionary<int, string>>();

        //Assert
        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void WhenIsIDictionary_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IDictionary<string, long>>();

        //Assert
        result.Should().HaveCount(3);
    }
}