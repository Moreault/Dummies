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

    [TestMethod]
    public void ZeroParameter_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action>();
        var action2 = Dummy.Create<Action>();

        //Act
        //Assert
        action1.Should().NotBeSameAs(action2);
    }

    [TestMethod]
    public void OneParameter_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?>>();
        var action2 = Dummy.Create<Action<object?>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void TwoParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int>>();
        var action2 = Dummy.Create<Action<object?, int>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void ThreeParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string>>();
        var action2 = Dummy.Create<Action<object?, int, string>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void FourParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void FiveParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void SixParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double, char>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double, char>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void SevenParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void EightParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void NineParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void TenParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void ElevenParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void TwelveParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte, sbyte>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte, sbyte>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void ThirteenParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte, sbyte, decimal>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte, sbyte, decimal>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void FourteenParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte, sbyte, decimal, DateOnly>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte, sbyte, decimal, DateOnly>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void FifteenParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte, sbyte, decimal, DateOnly, TimeOnly>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte, sbyte, decimal, DateOnly, TimeOnly>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }

    [TestMethod]
    public void SixteenParameters_WhenYouHaveTwoActionsOfSameTypeButDifferentReferences_ShouldNotBeEqual()
    {
        //Arrange
        var action1 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte, sbyte, decimal, DateOnly, TimeOnly, DateTimeOffset?>>();
        var action2 = Dummy.Create<Action<object?, int, string, DateTime, double, char, string, float, object, string, byte, sbyte, decimal, DateOnly, TimeOnly, DateTimeOffset?>>();

        //Act
        //Assert
        action1.Should().NotBe(action2);
    }
}
