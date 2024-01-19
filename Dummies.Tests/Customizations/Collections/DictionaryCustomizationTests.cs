namespace Dummies.Tests.Customizations.Collections;

[TestClass]
public sealed class ImmutableDictionaryCustomizationTests : Tester
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
}