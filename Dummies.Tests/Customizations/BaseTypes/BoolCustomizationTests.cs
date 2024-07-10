namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class BoolCustomizationTests : CustomizationTester<BoolCustomization>
{
    [TestMethod]
    public void WhenCreate_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = new List<bool>();

        for (var i = 0; i < 15; i++)
            result.Add(Dummy.Create<bool>());

        //Assert
        result.Should().Contain(true);
        result.Should().Contain(false);
    }

    [TestMethod]
    public void WhenCreateMany_ReturnMany()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<bool>().ToList();

        //Assert
        result.Should().HaveCount(3);
    }
}