namespace Dummies.Tests.Customizations.Collections;

[TestClass]
public sealed class GenericGenericStackCustomizationTests : Tester
{
    [TestMethod]
    public void Always_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Stack<string>>();

        //Assert
        result.Should().HaveCount(3);
    }
}