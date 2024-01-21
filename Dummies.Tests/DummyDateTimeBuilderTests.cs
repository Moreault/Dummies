namespace Dummies.Tests;

[TestClass]
public sealed class DummyDateTimeBuilderTests : Tester
{
    [TestMethod]
    public void Between_Always_CreateDateBetweenTheTwo()
    {
        //Arrange
        var a = Dummy.Create<DateTime>();
        var b = Dummy.Create<DateTime>();

        var min = a < b ? a : b;
        var max = a > b ? a : b;

        //Act
        var result = Dummy.Date.Between(min, max).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x >= min && x <= max);
    }

    [TestMethod]
    public void BetweenOffset_Always_CreateDateBetweenTheTwo()
    {
        //Arrange
        var a = Dummy.Create<DateTimeOffset>();
        var b = Dummy.Create<DateTimeOffset>();

        var min = a < b ? a : b;
        var max = a > b ? a : b;

        //Act
        var result = Dummy.Date.Between(min, max).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x >= min && x <= max);
    }

    [TestMethod]
    public void BetweenDateOnly_Always_CreateDateBetweenTheTwo()
    {
        //Arrange
        var a = Dummy.Create<DateOnly>();
        var b = Dummy.Create<DateOnly>();

        var min = a < b ? a : b;
        var max = a > b ? a : b;

        //Act
        var result = Dummy.Date.Between(min, max).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x >= min && x <= max);
    }

    [TestMethod]
    public void After_Always_ReturnLaterDate()
    {
        //Arrange
        var value = Dummy.Create<DateTime>();

        //Act
        var result = Dummy.Date.After(value).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x > value);
    }

    [TestMethod]
    public void AfterOffset_Always_ReturnLaterDate()
    {
        //Arrange
        var value = Dummy.Create<DateTimeOffset>();

        //Act
        var result = Dummy.Date.After(value).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x > value);
    }

    [TestMethod]
    public void AfterDateOnly_Always_ReturnLaterDate()
    {
        //Arrange
        var value = Dummy.Create<DateOnly>();

        //Act
        var result = Dummy.Date.After(value).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x > value);
    }

    [TestMethod]
    public void Before_Always_ReturnEarlerDate()
    {
        //Arrange
        var value = Dummy.Create<DateTime>();

        //Act
        var result = Dummy.Date.Before(value).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x < value);
    }

    [TestMethod]
    public void BeforeOffset_Always_ReturnEarlerDate()
    {
        //Arrange
        var value = Dummy.Create<DateTimeOffset>();

        //Act
        var result = Dummy.Date.Before(value).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x < value);
    }

    [TestMethod]
    public void BeforeDateOnly_Always_ReturnEarlerDate()
    {
        //Arrange
        var value = Dummy.Create<DateOnly>();

        //Act
        var result = Dummy.Date.Before(value).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x < value);
    }
}