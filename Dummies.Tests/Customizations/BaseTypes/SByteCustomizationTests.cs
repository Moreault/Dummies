namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class SByteCustomizationTests : CustomizationTester<SByteCustomization>
{
    [TestMethod]
    public void WhenCreate_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<sbyte>();

        //Assert
        result.Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void WhenCreateMany_ShouldReturnMany()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<sbyte>().ToList();

        //Assert
        result.Should().HaveCount(3);
    }
}