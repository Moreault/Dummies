namespace Dummies.Tests;

public abstract class CustomizationTester<T> : Tester where T : ICustomization, new()
{
    protected readonly T Instance = new();

    [TestMethod]
    public void Build_WhenDummyIsNull_Throw()
    {
        //Arrange
        IDummy dummy = null!;
        var type = Dummy.Create<Type>();

        //Act
        var action = () => Instance.Build(dummy, type);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(dummy));
    }

    [TestMethod]
    public void Build_WhenWhenTypeIsNull_Throw()
    {
        //Arrange
        Type type = null!;

        //Act
        var action = () => Instance.Build(Dummy, type);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(type));

    }
}