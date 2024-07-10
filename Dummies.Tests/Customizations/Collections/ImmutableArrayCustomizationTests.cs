namespace Dummies.Tests.Customizations.Collections;

[TestClass]
public sealed class ImmutableArrayCustomizationTests : CustomizationTester<ImmutableArrayCustomization>
{
    [TestMethod]
    public void Always_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<ImmutableArray<int>>();

        //Assert
        result.Should().HaveCount(3);
    }
}