namespace Dummies.Tests.Customizations.Collections;

[TestClass]
public sealed class ArrayCustomizationTests : Tester
{
    [TestMethod]
    public void Always_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<int[]>();

        //Assert
        result.Should().HaveCount(3);
    }
}