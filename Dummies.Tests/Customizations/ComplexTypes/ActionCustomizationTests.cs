namespace Dummies.Tests.Customizations.ComplexTypes;

[TestClass]
public class ActionCustomizationTests : Tester
{
    [TestMethod]
    public void WhenIsAction0_ExecutingItShouldNotThrow()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Action>();

        //Assert
        result.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction1_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string>>();

        //Act
        var action = () => result(Dummy.Create<string>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction2_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction3_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction4_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction5_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction6_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float, double>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>(), Dummy.Create<double>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction7_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float, double, decimal>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>(), Dummy.Create<double>(), Dummy.Create<decimal>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction8_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float, double, decimal, ulong>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>(), Dummy.Create<double>(), Dummy.Create<decimal>(), Dummy.Create<ulong>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction9_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float, double, decimal, ulong, ushort>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>(), Dummy.Create<double>(), Dummy.Create<decimal>(), Dummy.Create<ulong>(), Dummy.Create<ushort>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction10_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float, double, decimal, ulong, ushort, byte>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>(), Dummy.Create<double>(), Dummy.Create<decimal>(), Dummy.Create<ulong>(), Dummy.Create<ushort>(), Dummy.Create<byte>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction11_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float, double, decimal, ulong, ushort, byte, sbyte>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>(), Dummy.Create<double>(), Dummy.Create<decimal>(), Dummy.Create<ulong>(), Dummy.Create<ushort>(), Dummy.Create<byte>(), Dummy.Create<sbyte>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction12_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float, double, decimal, ulong, ushort, byte, sbyte, uint>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>(), Dummy.Create<double>(), Dummy.Create<decimal>(), Dummy.Create<ulong>(), Dummy.Create<ushort>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<uint>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction13_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float, double, decimal, ulong, ushort, byte, sbyte, uint, int>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>(), Dummy.Create<double>(), Dummy.Create<decimal>(), Dummy.Create<ulong>(), Dummy.Create<ushort>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<uint>(), Dummy.Create<int>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction14_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float, double, decimal, ulong, ushort, byte, sbyte, uint, int, string>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>(), Dummy.Create<double>(), Dummy.Create<decimal>(), Dummy.Create<ulong>(), Dummy.Create<ushort>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<uint>(), Dummy.Create<int>(), Dummy.Create<string>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction15_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float, double, decimal, ulong, ushort, byte, sbyte, uint, int, string, uint>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>(), Dummy.Create<double>(), Dummy.Create<decimal>(), Dummy.Create<ulong>(), Dummy.Create<ushort>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<uint>(), Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>());

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void WhenIsAction16_ExecutingItShouldNotThrow()
    {
        //Arrange
        var result = Dummy.Create<Action<string, char, int, long, float, double, decimal, ulong, ushort, byte, sbyte, uint, int, string, uint, char>>();

        //Act
        var action = () => result(Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<int>(), Dummy.Create<long>(), Dummy.Create<float>(), Dummy.Create<double>(), Dummy.Create<decimal>(), Dummy.Create<ulong>(), Dummy.Create<ushort>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<uint>(), Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<char>());

        //Assert
        action.Should().NotThrow();
    }
}
