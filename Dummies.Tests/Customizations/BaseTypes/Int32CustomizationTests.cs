﻿namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class Int32CustomizationTests : CustomizationTester<Int32Customization>
{
    [TestMethod]
    public void WhenInt32_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<int>();

        //Assert
        result.Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void WhenCreateManyInt32_ShouldAllBeDifferent()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<int>().ToList();

        //Assert
        result.Distinct().Should().BeEquivalentTo(result);
    }
}