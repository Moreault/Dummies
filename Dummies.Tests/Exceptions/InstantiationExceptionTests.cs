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

    [TestMethod]
    public void ConstructorWithExceptions_WhenExceptionsAreNotEmpty_InnerExceptionIsAggregateException()
    {
        //Arrange
        var exceptions = new List<Exception> { new InvalidOperationException("oops"), new ArgumentException("bad") };

        //Act
        var result = new InstantiationException(typeof(int), exceptions);

        //Assert
        result.InnerException.Should().BeOfType<AggregateException>();
        ((AggregateException)result.InnerException!).InnerExceptions.Should().HaveCount(2);
    }

    [TestMethod]
    public void ConstructorWithExceptions_WhenExceptionsAreEmpty_InnerExceptionIsNull()
    {
        //Arrange
        var exceptions = new List<Exception>();

        //Act
        var result = new InstantiationException(typeof(int), exceptions);

        //Assert
        result.InnerException.Should().BeNull();
    }

    [TestMethod]
    public void ConstructorWithExceptions_WhenTypeIsNotNull_ReturnMessageWithTypeName()
    {
        //Arrange
        var exceptions = new List<Exception> { new InvalidOperationException("oops") };

        //Act
        var result = new InstantiationException(typeof(List<Garbage<int>>), exceptions);

        //Assert
        result.Message.Should().Be(string.Format(ExceptionMessages.Instantiation, "List<Garbage<Int32>>"));
    }
}