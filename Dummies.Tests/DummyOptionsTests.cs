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

    [TestMethod]
    public void Reset_Always_ResetsToGlobalOptions()
    {
        //Arrange
        var instance = new DummyOptions();

        //Act
        instance.Reset();

        //Assert
        instance.DefaultCollectionSize.Should().Be(DummyOptions.Global.DefaultCollectionSize);
        instance.MaximumDepth.Should().Be(DummyOptions.Global.MaximumDepth);
        instance.UniqueGenerationAttempts.Should().Be(DummyOptions.Global.UniqueGenerationAttempts);
    }

    [TestMethod]
    public void DefaultCollectionSize_WhenSet_ReturnOverridenValue()
    {
        //Arrange
        var value = Dummy.Create<int>();

        //Act
        var instance = new DummyOptions
        {
            DefaultCollectionSize = value
        };

        //Assert
        instance.DefaultCollectionSize.Should().Be(value);
    }

    [TestMethod]
    public void DefaultCollectionSize_WhenNotSet_ReturnGlobalValue()
    {
        //Arrange

        //Act
        var instance = new DummyOptions();

        //Assert
        instance.DefaultCollectionSize.Should().Be(DummyOptions.Global.DefaultCollectionSize);
    }

    [TestMethod]
    public void MaximumDepth_WhenSet_ReturnOverridenValue()
    {
        //Arrange
        var value = Dummy.Create<int>();

        //Act
        var instance = new DummyOptions
        {
            MaximumDepth = value
        };

        //Assert
        instance.MaximumDepth.Should().Be(value);
    }

    [TestMethod]
    public void MaximumDepth_WhenNotSet_ReturnGlobalValue()
    {
        //Arrange

        //Act
        var instance = new DummyOptions();

        //Assert
        instance.MaximumDepth.Should().Be(DummyOptions.Global.MaximumDepth);
    }

    [TestMethod]
    public void UniqueGenerationAttempts_WhenSet_ReturnOverridenValue()
    {
        //Arrange
        var value = Dummy.Create<int>();

        //Act
        var instance = new DummyOptions
        {
            UniqueGenerationAttempts = value
        };

        //Assert
        instance.UniqueGenerationAttempts.Should().Be(value);
    }

    [TestMethod]
    public void UniqueGenerationAttempts_WhenNotSet_ReturnGlobalValue()
    {
        //Arrange

        //Act
        var instance = new DummyOptions();

        //Assert
        instance.UniqueGenerationAttempts.Should().Be(DummyOptions.Global.UniqueGenerationAttempts);
    }
}