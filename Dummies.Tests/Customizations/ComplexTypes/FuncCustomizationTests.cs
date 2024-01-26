namespace Dummies.Tests.Customizations.ComplexTypes;

[TestClass]
public class FuncTests : Tester
{
    [TestMethod]
    public void WhenIsFunc1_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int>>();

        //Assert
        result().Should().Be(0);
    }

    [TestMethod]
    public void WhenIsFunc2_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string>>();

        //Assert
        result(Dummy.Create<int>()).Should().BeNull();
    }

    [TestMethod]
    public void WhenIsFunc3_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>()).Should().Be(0);
    }

    [TestMethod]
    public void WhenIsFunc4_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>()).Should().Be(0);
    }

    [TestMethod]
    public void WhenIsFunc5_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc6_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc7_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte, sbyte>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>(), Dummy.Create<byte>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc8_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte, sbyte, string>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>(), Dummy.Create<byte>(), Dummy.Create<sbyte>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc9_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte, sbyte, string, char>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<string>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc10_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte, sbyte, string, char, long>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<string>(), Dummy.Create<char>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc11_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte, sbyte, string, char, long, byte>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<long>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc12_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte, sbyte, string, char, long, byte, uint>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<long>(), Dummy.Create<byte>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc13_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte, sbyte, string, char, long, byte, uint, string>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<long>(), Dummy.Create<byte>(), Dummy.Create<uint>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc14_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte, sbyte, string, char, long, byte, uint, string, char>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<long>(), Dummy.Create<byte>(), Dummy.Create<uint>(), Dummy.Create<string>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc15_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte, sbyte, string, char, long, byte, uint, string, char, ulong>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<long>(), Dummy.Create<byte>(), Dummy.Create<uint>(), Dummy.Create<string>(), Dummy.Create<char>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc16_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte, sbyte, string, char, long, byte, uint, string, char, ulong, char>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<long>(), Dummy.Create<byte>(), Dummy.Create<uint>(), Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<ulong>()).Should().Be(default);
    }

    [TestMethod]
    public void WhenIsFunc17_BuildFuncThatReturnsDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Func<int, string, uint, long, char, byte, sbyte, string, char, long, byte, uint, string, char, ulong, char, sbyte>>();

        //Assert
        result(Dummy.Create<int>(), Dummy.Create<string>(), Dummy.Create<uint>(), Dummy.Create<long>(), Dummy.Create<char>(), Dummy.Create<byte>(), Dummy.Create<sbyte>(), Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<long>(), Dummy.Create<byte>(), Dummy.Create<uint>(), Dummy.Create<string>(), Dummy.Create<char>(), Dummy.Create<ulong>(), Dummy.Create<char>()).Should().Be(default);
    }
}