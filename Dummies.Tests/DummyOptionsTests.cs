namespace Dummies.Tests;

[TestClass]
public sealed class DummyOptionsTests : Tester
{
    [TestMethod]
    public void GlobalUniqueGenerationAttempts_WhenValueIsNegativeOrZero_SetToOne()
    {
        //Arrange
        var value = Dummy.Number.LessThanOrEqualTo(0).Create();

        //Act
        DummyOptions.Global.UniqueGenerationAttempts = value;

        //Assert
        DummyOptions.Global.UniqueGenerationAttempts.Should().Be(1);
    }

    [TestMethod]
    public void GlobalUniqueGenerationAttempts_WhenValueIsGreaterThanZero_SetToThatValue()
    {
        //Arrange
        var value = Dummy.Create<int>();

        //Act
        DummyOptions.Global.UniqueGenerationAttempts = value;

        //Assert
        DummyOptions.Global.UniqueGenerationAttempts.Should().Be(value);
    }

    [TestMethod]
    public void UniqueGenerationAttempts_WhenValueIsNegativeOrZero_SetToOne()
    {
        //Arrange
        var instance = new DummyOptions();
        var value = Dummy.Number.LessThanOrEqualTo(0).Create();

        //Act
        instance.UniqueGenerationAttempts = value;

        //Assert
        instance.UniqueGenerationAttempts.Should().Be(1);
    }

    [TestMethod]
    public void UniqueGenerationAttempts_WhenValueIsGreaterThanZero_SetToThatValue()
    {
        //Arrange
        var instance = new DummyOptions();
        var value = Dummy.Create<int>();

        //Act
        instance.UniqueGenerationAttempts = value;

        //Assert
        instance.UniqueGenerationAttempts.Should().Be(value);
    }


}