namespace Dummies.Tests;

[TestClass]
public sealed class DummyFloatingPointNumberBuilderOfFloatTests : DummyFloatingPointNumberBuilderTester<float>;

[TestClass]
public sealed class DummyFloatingPointNumberBuilderOfDoubleTests : DummyFloatingPointNumberBuilderTester<double>;

[TestClass]
public sealed class DummyFloatingPointNumberBuilderOfDecimalTests : DummyFloatingPointNumberBuilderTester<decimal>;

public abstract class DummyFloatingPointNumberBuilderTester<T> : Tester where T : IFloatingPoint<T>, IMinMaxValue<T>
{
    [TestMethod]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(3)]
    [DataRow(5)]
    public void Create_WhenVanilla_CreateFloatingPointNumberWithNumberOfDigits(int digits)
    {
        // Arrange

        // Act
        var result = Dummy.Number.WithDecimals(digits).Create<T>();

        // Assert
        var decimalPart = result.GetDecimals();
        var asString = decimalPart.ToString($"F{digits}", CultureInfo.InvariantCulture)!.TrimStart('-').TrimStart("0.");
        asString.Length.Should().Be(digits);
    }

    [TestMethod]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(3)]
    [DataRow(5)]
    public void CreateMany_WhenVanilla_CreateManyFloatingPointNumberWithNumberOfDigits(int digits)
    {
        // Arrange

        // Act
        var result = Dummy.Number.WithDecimals(digits).CreateMany<T>();

        // Assert
        var decimalPart = result.Select(x => x.GetDecimals());
        var asString = decimalPart.Select(x => x.ToString($"F{digits}", CultureInfo.InvariantCulture)!.TrimStart('-').TrimStart("0.")).ToList();
        asString.Should().OnlyContain(x => x.Length == digits);
    }

    [TestMethod]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(3)]
    [DataRow(5)]
    public void CreateManyWithAmount_WhenVanilla_CreateManyFloatingPointNumberWithNumberOfDigits(int digits)
    {
        // Arrange

        // Act
        var result = Dummy.Number.WithDecimals(digits).CreateMany<T>(10);

        // Assert
        var decimalPart = result.Select(x => x.GetDecimals());
        var asString = decimalPart.Select(x => x.ToString($"F{digits}", CultureInfo.InvariantCulture)!.TrimStart('-').TrimStart("0.")).ToList();
        asString.Should().OnlyContain(x => x.Length == digits);
    }

    [TestMethod]
    public void LessThan_Always_ReturnNumbersSmallThanSpecified()
    {
        //Arrange
        var value = Dummy.Create<T>();

        //Act
        var result = Dummy.Number.WithDecimals().LessThan(value).CreateMany(20);

        //Assert
        result.Should().OnlyContain(x => x.IsLesserThan(value));
    }

    [TestMethod]
    public void LessThanOrEqualTo_Always_ReturnNumbersSmallThanSpecified()
    {
        //Arrange
        var value = Dummy.Create<T>();

        //Act
        var result = Dummy.Number.WithDecimals().LessThanOrEqualTo(value).CreateMany(20);

        //Assert
        result.Should().OnlyContain(x => x.IsLesserThanOrEqualTo(value));
    }

    [TestMethod]
    public void GreaterThan_Always_ReturnNumbersGreaterThanSpecified()
    {
        //Arrange
        var value = Dummy.Create<T>();

        //Act
        var result = Dummy.Number.WithDecimals().GreaterThan(value).CreateMany(20);

        //Assert
        result.Should().OnlyContain(x => x.IsGreaterThan(value));
    }

    [TestMethod]
    public void GreaterThanOrEqualTo_Always_ReturnNumbersGreaterThanSpecified()
    {
        //Arrange
        var value = Dummy.Create<T>();

        //Act
        var result = Dummy.Number.WithDecimals().GreaterThanOrEqualTo(value).CreateMany(20);

        //Assert
        result.Should().OnlyContain(x => x.IsGreaterThanOrEqualTo(value));
    }

}

internal static class MathematicsExtensions
{
    //TODO Mathemancy 3.0.0
    public static T GetDecimals<T>(this T value) where T : IFloatingPoint<T>, IParsable<T> => value - T.Truncate(value);
}