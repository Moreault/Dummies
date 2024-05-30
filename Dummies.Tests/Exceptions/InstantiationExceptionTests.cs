namespace Dummies.Tests.Exceptions;

[TestClass]
public sealed class InstantiationExceptionTests
{
    public sealed class Garbage<T>;

    [TestMethod]
    public void Constructor_WhenTypeIsNull_ReturnMessageWithNullType()
    {
        //Arrange

        //Act
        var result = new InstantiationException(null!);

        //Assert
        result.Message.Should().Be(string.Format(ExceptionMessages.Instantiation, "NULL TYPE"));
    }

    [TestMethod]
    public void Constructor_WhenTypeIsNotNull_ReturnMessageWithTypeName()
    {
        //Arrange

        //Act
        var result = new InstantiationException(typeof(List<Garbage<int>>));

        //Assert
        result.Message.Should().Be(string.Format(ExceptionMessages.Instantiation, "List<Garbage<Int32>>"));
    }
}