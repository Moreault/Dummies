using System.Globalization;
using ToolBX.EasyTypeParsing;
using ToolBX.SmartyStrings;

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


}

internal static class MathematicsExtensions
{
    //TODO Mathemancy 3.0.0
    public static T GetDecimals<T>(this T value) where T : IFloatingPoint<T>, IParsable<T> => value - T.Truncate(value);
}