namespace Dummies.Tests.Customizations.Collections;

[TestClass]
public sealed class ImmutableDictionaryCustomizationTests : CustomizationTester<ImmutableDictionaryCustomization>
{
    [TestMethod]
    public void Always_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<ImmutableDictionary<int, string>>();

        //Assert
        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void WhenIsIDictionary_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IImmutableDictionary<string, long>>();

        //Assert
        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void WhenIsIReadOnlyDictionary_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IReadOnlyDictionary<string, float>>();

        //Assert
        result.Should().HaveCount(3);
    }
}