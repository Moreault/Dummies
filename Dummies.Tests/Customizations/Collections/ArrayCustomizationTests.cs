namespace Dummies.Tests.Customizations.Collections;

[TestClass]
public sealed class ArrayCustomizationTests : CustomizationTester<ArrayCustomization>
{
    [TestMethod]
    public void WhenIsOneDimensional_CreateWithMultipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<int[]>();

        //Assert
        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void WhenHasTwoDimensions_CreateWithMultipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<string[,]>();

        //Assert
        result.GetLength(0).Should().Be(3);
        result.GetLength(1).Should().Be(3);
    }

    [TestMethod]
    public void WhenHasTwoDimensions_PopulateBothDimensions()
    {
        //Arrange

        //Act
        var result = Dummy.Create<string[,]>();

        //Assert
        for (var x = 0; x < result.GetLength(0); x++)
            for (var y = 0; y < result.GetLength(1); y++)
                result[x, y].Should().NotBeEmpty();
    }
}