using TimeProvider = ToolBX.TimeProvider.TimeProvider;

namespace Dummies.Tests;

[TestClass]
public sealed class DummyDateTimeBuilderTests : Tester
{
    [TestMethod]
    public void Between_WhenMinIsGreaterThanMax_Throw()
    {
        //Arrange
        var min = Dummy.Create<DateTime>();
        var max = Dummy.Date.Before(min).Create();

        //Act
        var action = () => Dummy.Date.Between(min, max).Create();

        //Assert
        action.Should().Throw<ArgumentException>().WithMessage(ExceptionMessages.StartDateMustBeEarlier);
    }

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
    public void BetweenWithoutAmount_Always_CreateDateBetweenTheTwo()
    {
        //Arrange
        var a = Dummy.Create<DateTime>();
        var b = Dummy.Create<DateTime>();

        var min = a < b ? a : b;
        var max = a > b ? a : b;

        //Act
        var result = Dummy.Date.Between(min, max).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x >= min && x <= max);
    }

    [TestMethod]
    public void BetweenOffset_WhenMinIsGreaterThanMax_Throw()
    {
        //Arrange
        var min = Dummy.Create<DateTimeOffset>();
        var max = Dummy.Date.Before(min).Create();

        //Act
        var action = () => Dummy.Date.Between(min, max).Create();

        //Assert
        action.Should().Throw<ArgumentException>().WithMessage(ExceptionMessages.StartDateMustBeEarlier);
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
    public void BetweenOffsetWithoutAmount_Always_CreateDateBetweenTheTwo()
    {
        //Arrange
        var a = Dummy.Create<DateTimeOffset>();
        var b = Dummy.Create<DateTimeOffset>();

        var min = a < b ? a : b;
        var max = a > b ? a : b;

        //Act
        var result = Dummy.Date.Between(min, max).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x >= min && x <= max);
    }

    [TestMethod]
    public void BetweenDateOnly_WhenMinIsGreaterThanMax_Throw()
    {
        //Arrange
        var min = Dummy.Create<DateOnly>();
        var max = Dummy.Date.Before(min).Create();

        //Act
        var action = () => Dummy.Date.Between(min, max).Create();

        //Assert
        action.Should().Throw<ArgumentException>().WithMessage(ExceptionMessages.StartDateMustBeEarlier);
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
    public void BetweenDateOnlyWithoutAmount_Always_CreateDateBetweenTheTwo()
    {
        //Arrange
        var a = Dummy.Create<DateOnly>();
        var b = Dummy.Create<DateOnly>();

        var min = a < b ? a : b;
        var max = a > b ? a : b;

        //Act
        var result = Dummy.Date.Between(min, max).CreateMany();

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
    public void AfterWithoutAmount_Always_ReturnLaterDate()
    {
        //Arrange
        var value = Dummy.Create<DateTime>();

        //Act
        var result = Dummy.Date.After(value).CreateMany();

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
    public void AfterOffsetWithoutAmount_Always_ReturnLaterDate()
    {
        //Arrange
        var value = Dummy.Create<DateTimeOffset>();

        //Act
        var result = Dummy.Date.After(value).CreateMany();

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
    public void AfterDateOnlyWithoutAmount_Always_ReturnLaterDate()
    {
        //Arrange
        var value = Dummy.Create<DateOnly>();

        //Act
        var result = Dummy.Date.After(value).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x > value);
    }

    [TestMethod]
    public void Before_Always_ReturnEarlierDate()
    {
        //Arrange
        var value = Dummy.Create<DateTime>();

        //Act
        var result = Dummy.Date.Before(value).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x < value);
    }

    [TestMethod]
    public void BeforeWithoutAmount_Always_ReturnEarlierDate()
    {
        //Arrange
        var value = Dummy.Create<DateTime>();

        //Act
        var result = Dummy.Date.Before(value).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x < value);
    }

    [TestMethod]
    public void BeforeOffset_Always_ReturnEarlierDate()
    {
        //Arrange
        var value = Dummy.Create<DateTimeOffset>();

        //Act
        var result = Dummy.Date.Before(value).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x < value);
    }

    [TestMethod]
    public void BeforeOffsetWithoutAmount_Always_ReturnEarlierDate()
    {
        //Arrange
        var value = Dummy.Create<DateTimeOffset>();

        //Act
        var result = Dummy.Date.Before(value).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x < value);
    }

    [TestMethod]
    public void BeforeDateOnly_Always_ReturnEarlierDate()
    {
        //Arrange
        var value = Dummy.Create<DateOnly>();

        //Act
        var result = Dummy.Date.Before(value).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x < value);
    }

    [TestMethod]
    public void BeforeDateOnlyWithoutAmount_Always_ReturnEarlierDate()
    {
        //Arrange
        var value = Dummy.Create<DateOnly>();

        //Act
        var result = Dummy.Date.Before(value).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x < value);
    }

    [TestMethod]
    public void Create_WhenNoCustomization_ReturnRandomDateTime()
    {
        //Arrange

        //Act
        var result = Dummy.Date.Create();

        //Assert
        result.Should().BeOnOrAfter(DateTime.MinValue);
        result.Should().BeOnOrBefore(DateTime.MaxValue);
    }

    [TestMethod]
    public void CreateMany_WhenNoCustomization_ReturnManyRandomDateTime()
    {
        //Arrange

        //Act
        var result = Dummy.Date.CreateMany();

        //Assert
        result.Should().OnlyContain(x => x >= DateTime.MinValue && x <= DateTime.MaxValue);
    }

    [TestMethod]
    public void CreateManyWithAmount_WhenNoCustomization_ReturnManyRandomDateTime()
    {
        //Arrange

        //Act
        var result = Dummy.Date.CreateMany(10);

        //Assert
        result.Should().OnlyContain(x => x >= DateTime.MinValue && x <= DateTime.MaxValue);
    }

    [TestMethod]
    public void CreateOffset_WhenNoCustomization_ReturnRandomDateTime()
    {
        //Arrange

        //Act
        var result = Dummy.Date.CreateOffset();

        //Assert
        result.Should().BeOnOrAfter(DateTimeOffset.MinValue);
        result.Should().BeOnOrBefore(DateTimeOffset.MaxValue);
    }

    [TestMethod]
    public void CreateManyOffset_WhenNoCustomization_ReturnManyRandomDateTime()
    {
        //Arrange

        //Act
        var result = Dummy.Date.CreateManyOffset();

        //Assert
        result.Should().OnlyContain(x => x >= DateTimeOffset.MinValue && x <= DateTimeOffset.MaxValue);
    }

    [TestMethod]
    public void CreateManyOffsetWithAmount_WhenNoCustomization_ReturnManyRandomDateTime()
    {
        //Arrange

        //Act
        var result = Dummy.Date.CreateManyOffset(10);

        //Assert
        result.Should().OnlyContain(x => x >= DateTimeOffset.MinValue && x <= DateTimeOffset.MaxValue);
    }


    [TestMethod]
    public void CreateDateOnly_WhenNoCustomization_ReturnRandomDateTime()
    {
        //Arrange

        //Act
        var result = Dummy.Date.CreateDateOnly();

        //Assert
        result.Should().BeOnOrAfter(DateOnly.MinValue);
        result.Should().BeOnOrBefore(DateOnly.MaxValue);
    }

    [TestMethod]
    public void CreateManyDateOnly_WhenNoCustomization_ReturnManyRandomDateTime()
    {
        //Arrange

        //Act
        var result = Dummy.Date.CreateManyDateOnly();

        //Assert
        result.Should().OnlyContain(x => x >= DateOnly.MinValue && x <= DateOnly.MaxValue);
    }

    [TestMethod]
    public void CreateManyDateOnlyWithAmount_WhenNoCustomization_ReturnManyRandomDateTime()
    {
        //Arrange

        //Act
        var result = Dummy.Date.CreateManyDateOnly(10);

        //Assert
        result.Should().OnlyContain(x => x >= DateOnly.MinValue && x <= DateOnly.MaxValue);
    }

    [TestMethod]
    public void BeforeNow_Always_CreateDatesBetweenFiveYearsAgoAndOneMinuteAgo()
    {
        //Arrange
        var now = Dummy.Create<DateTime>();
        TimeProvider.Freeze(now);

        //Act
        var result = Dummy.Date.BeforeNow().CreateMany(25).ToList();

        //Assert
        result.Should().OnlyContain(x => x >= now.AddYears(-5) && x <= now.AddMinutes(-1));
    }

    [TestMethod]
    public void BeforeNowOffset_Always_CreateDatesBetweenFiveYearsAgoAndOneMinuteAgo()
    {
        //Arrange
        var now = Dummy.Create<DateTimeOffset>();
        TimeProvider.Freeze(now);

        //Act
        var result = Dummy.Date.BeforeNowOffset().CreateMany(25).ToList();

        //Assert
        result.Should().OnlyContain(x => x >= now.AddYears(-5) && x <= now.AddMinutes(-1));
    }

    [TestMethod]
    public void BeforeToday_Always_CreateDatesBetweenFiveYearsAgoAndOneMinuteAgo()
    {
        //Arrange
        var now = Dummy.Create<DateTime>();
        TimeProvider.Freeze(now);

        //Act
        var result = Dummy.Date.BeforeToday().CreateMany(25).ToList();

        //Assert
        result.Should().OnlyContain(x => x >= now.AddYears(-5) && x <= now.AddDays(-1));
    }

    [TestMethod]
    public void BeforeTodayOffset_Always_CreateDatesBetweenFiveYearsAgoAndOneMinuteAgo()
    {
        //Arrange
        var now = Dummy.Create<DateTimeOffset>();
        TimeProvider.Freeze(now);

        //Act
        var result = Dummy.Date.BeforeTodayOffset().CreateMany(25).ToList();

        //Assert
        result.Should().OnlyContain(x => x >= now.AddYears(-5) && x <= now.AddDays(-1));
    }

    [TestMethod]
    public void AfterNow_Always_CreateDatesBetweenNowAndFiveYearsInTheFuture()
    {
        //Arrange
        var now = Dummy.Create<DateTime>();
        TimeProvider.Freeze(now);

        //Act
        var result = Dummy.Date.AfterNow().CreateMany(25).ToList();

        //Assert
        result.Should().OnlyContain(x => x >= now.AddMinutes(1) && x <= now.AddYears(5));
    }

    [TestMethod]
    public void AfterNowOffset_Always_CreateDatesBetweenNowAndFiveYearsInTheFuture()
    {
        //Arrange
        var now = Dummy.Create<DateTimeOffset>();
        TimeProvider.Freeze(now);

        //Act
        var result = Dummy.Date.AfterNowOffset().CreateMany(25).ToList();

        //Assert
        result.Should().OnlyContain(x => x >= now.AddMinutes(1) && x <= now.AddYears(5));
    }

    [TestMethod]
    public void AfterToday_Always_CreateDatesBetweenNowAndFiveYearsInTheFuture()
    {
        //Arrange
        var now = Dummy.Create<DateTime>();
        TimeProvider.Freeze(now);

        //Act
        var result = Dummy.Date.AfterToday().CreateMany(25).ToList();

        //Assert
        result.Should().OnlyContain(x => x >= now.AddDays(1) && x <= now.AddYears(5));
    }

    [TestMethod]
    public void AfterTodayOffset_Always_CreateDatesBetweenNowAndFiveYearsInTheFuture()
    {
        //Arrange
        var now = Dummy.Create<DateTimeOffset>();
        TimeProvider.Freeze(now);

        //Act
        var result = Dummy.Date.AfterTodayOffset().CreateMany(25).ToList();

        //Assert
        result.Should().OnlyContain(x => x >= now.AddDays(1) && x <= now.AddYears(5));
    }
}