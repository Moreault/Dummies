namespace Dummies.Tests.Customizations.Collections;

[TestClass]
public sealed class ImmutableListCustomizationTests : CustomizationTester<ImmutableArrayCustomization>
{
    [TestMethod]
    public void Always_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<ImmutableList<int>>();

        //Assert
        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void WhenCreateIImmutableList_CreateList()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IImmutableList<string>>();

        //Assert
        result.Should().HaveCount(3);
    }
}