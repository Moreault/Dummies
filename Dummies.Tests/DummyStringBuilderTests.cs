namespace Dummies.Tests;

[TestClass]
public sealed class DummyStringBuilderTests : Tester
{
    [TestMethod]
    [DataRow(2)]
    [DataRow(12)]
    [DataRow(24)]
    [DataRow(48)]
    public void WithLengthExactly_WhenExactStringLength_ReturnStringOfExactLength(int length)
    {
        //Arrange

        //Act
        var result = Dummy.String.WithLength.Exactly(length).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x.Length == length);
    }

    [TestMethod]
    [DataRow(2)]
    [DataRow(12)]
    [DataRow(24)]
    [DataRow(48)]
    public void WithLengthExactlyAndDefaultCreate_WhenExactStringLength_ReturnStringOfExactLength(int length)
    {
        //Arrange

        //Act
        var result = Dummy.String.WithLength.Exactly(length).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x.Length == length);
    }

    [TestMethod]
    public void WithLengthBetween_Always_ReturnStringsOfLengthBetweenMinAndMax()
    {
        //Arrange

        //Act
        var result = Dummy.String.WithLength.Between(10, 30).CreateMany(25).OrderBy(x => x.Length).ToList();

        //Assert
        result.Distinct().Should().HaveCountGreaterThan(10);
        result.Should().OnlyContain(x => x.Length >= 10 && x.Length <= 30);
    }

    [TestMethod]
    public void WithLengthLessThan_Always_ReturnStringsOfLengthBetweenMinAndMax()
    {
        //Arrange

        //Act
        var result = Dummy.String.WithLength.LessThan(20).CreateMany(25).OrderBy(x => x.Length).ToList();

        //Assert
        result.Distinct().Should().HaveCountGreaterThan(10);
        result.Should().OnlyContain(x => x.Length < 20);
    }

    [TestMethod]
    public void WithLengthLessThanOrEqualTo_Always_ReturnStringsOfLengthBetweenMinAndMax()
    {
        //Arrange

        //Act
        var result = Dummy.String.WithLength.LessThanOrEqualTo(20).CreateMany(25).OrderBy(x => x.Length).ToList();

        //Assert
        result.Distinct().Should().HaveCountGreaterThan(10);
        result.Should().OnlyContain(x => x.Length <= 20);
    }

    [TestMethod]
    public void CreateWithoutSpecifics_Always_ReturnStringOfDefaultLength()
    {
        //Arrange
        const int defaultLength = 24;

        //Act
        var result = Dummy.String.CreateMany(25).ToList();

        //Assert
        result.Distinct().Should().HaveCountGreaterThan(10);
        result.Should().OnlyContain(x => x.Length == defaultLength);
    }
}