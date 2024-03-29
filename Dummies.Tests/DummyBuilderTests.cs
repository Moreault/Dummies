﻿namespace Dummies.Tests;

[TestClass]
public sealed class DummyBuilderTests : Tester
{
    public sealed record Garbage
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public long Experience { get; init; }
        public short Level { get; init; }
        public float Health { get; init; }
        public double Mana { get; init; }
        public decimal Gold { get; init; }
        public sbyte Symbol { get; init; }
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeSByte_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Symbol, -Dummy.Create<sbyte>()).Create();

        //Assert
        result.Symbol.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeShort_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Level, -Dummy.Create<short>()).Create();

        //Assert
        result.Level.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeInt_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Experience, -Dummy.Create<long>()).Create();

        //Assert
        result.Experience.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeFloat_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Health, -Dummy.Create<float>()).Create();

        //Assert
        result.Health.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeDouble_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Mana, -Dummy.Create<double>()).Create();

        //Assert
        result.Mana.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeDecimal_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Gold, -Dummy.Create<decimal>()).Create();

        //Assert
        result.Gold.Should().BeNegative();
    }



    [TestMethod]
    public void WhenUsingWithOnNegativeSByteAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Symbol, () => -Dummy.Create<sbyte>()).Create();

        //Assert
        result.Symbol.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeShortAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Level, () => -Dummy.Create<short>()).Create();

        //Assert
        result.Level.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeIntAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Experience, () => -Dummy.Create<long>()).Create();

        //Assert
        result.Experience.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeFloatAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Health, () => -Dummy.Create<float>()).Create();

        //Assert
        result.Health.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeDoubleAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Mana, () => -Dummy.Create<double>()).Create();

        //Assert
        result.Mana.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeDecimalAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Gold, () => -Dummy.Create<decimal>()).Create();

        //Assert
        result.Gold.Should().BeNegative();
    }
}