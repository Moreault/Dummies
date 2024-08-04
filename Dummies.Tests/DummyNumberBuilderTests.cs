namespace Dummies.Tests;

[TestClass]
public sealed class DummyNumberBuilderOfByteTests : DummyNumberBuilderTester<byte>;

[TestClass]
public sealed class DummyNumberBuilderOfSByteTests : DummyNumberBuilderTester<sbyte>;

[TestClass]
public sealed class DummyNumberBuilderOfShortTests : DummyNumberBuilderTester<short>;

[TestClass]
public sealed class DummyNumberBuilderOfUShortTests : DummyNumberBuilderTester<ushort>;

[TestClass]
public sealed class DummyNumberBuilderOfIntTests : DummyNumberBuilderTester<int>;

[TestClass]
public sealed class DummyNumberBuilderOfUintTests : DummyNumberBuilderTester<uint>;

[TestClass]
public sealed class DummyNumberBuilderOfLongTests : DummyNumberBuilderTester<long>;

[TestClass]
public sealed class DummyNumberBuilderOfULongTests : DummyNumberBuilderTester<ulong>;

[TestClass]
public sealed class DummyNumberBuilderOfFloatTests : DummyNumberBuilderTester<float>;

[TestClass]
public sealed class DummyNumberBuilderOfDoubleTests : DummyNumberBuilderTester<double>;

[TestClass]
public sealed class DummyNumberBuilderOfDecimalTests : DummyNumberBuilderTester<decimal>;

[TestClass]
public abstract class DummyNumberBuilderTester<T> : Tester where T : INumber<T>, IMinMaxValue<T>
{
    [TestMethod]
    public void LessThan_Always_ReturnNumbersThatAreLessThanValue()
    {
        //Arrange
        var value = T.CreateSaturating(50);

        //Act
        var result = Dummy.Number.LessThan(value).CreateMany(20);

        //Assert
        result.Should().OnlyContain(x => x.CompareTo(value) < 0);
    }

    [TestMethod]
    public void LessThanWithoutAmount_Always_ReturnNumbersThatAreLessThanValue()
    {
        //Arrange
        var value = T.CreateSaturating(50);

        //Act
        var result = Dummy.Number.LessThan(value).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x.CompareTo(value) < 0);
    }

    [TestMethod]
    public void LessThanOrEqualTo_Always_ReturnNumbersThatAreLessThanOrEqualToValue()
    {
        //Arrange
        var value = T.CreateSaturating(50);

        //Act
        var result = Dummy.Number.LessThanOrEqualTo(value).CreateMany(20);

        //Assert
        result.Should().OnlyContain(x => x.CompareTo(value) <= 0);
    }

    [TestMethod]
    public void LessThanOrEqualToWithoutAmount_Always_ReturnNumbersThatAreLessThanOrEqualToValue()
    {
        //Arrange
        var value = T.CreateSaturating(50);

        //Act
        var result = Dummy.Number.LessThanOrEqualTo(value).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x.CompareTo(value) <= 0);
    }

    [TestMethod]
    public void GreaterThan_Always_ReturnNumbersThatAreGreaterThanValue()
    {
        //Arrange
        var value = T.CreateSaturating(50);

        //Act
        var result = Dummy.Number.GreaterThan(value).CreateMany(20);

        //Assert
        result.Should().OnlyContain(x => x.CompareTo(value) > 0);
    }

    [TestMethod]
    public void GreaterThanWithoutAmount_Always_ReturnNumbersThatAreGreaterThanValue()
    {
        //Arrange
        var value = T.CreateSaturating(50);

        //Act
        var result = Dummy.Number.GreaterThan(value).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x.CompareTo(value) > 0);
    }

    [TestMethod]
    public void GreaterThanOrEqualTo_Always_ReturnNumbersThatAreGreaterThanOrEqualToValue()
    {
        //Arrange
        var value = T.CreateSaturating(50);

        //Act
        var result = Dummy.Number.GreaterThanOrEqualTo(value).CreateMany(20);

        //Assert
        result.Should().OnlyContain(x => x.CompareTo(value) >= 0);
    }

    [TestMethod]
    public void GreaterThanOrEqualToWithoutAmount_Always_ReturnNumbersThatAreGreaterThanOrEqualToValue()
    {
        //Arrange
        var value = T.CreateSaturating(50);

        //Act
        var result = Dummy.Number.GreaterThanOrEqualTo(value).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x.CompareTo(value) >= 0);
    }

    [TestMethod]
    public void Between_Always_ReturnNumbersBetweenBoundaries()
    {
        //Arrange
        var min = T.CreateSaturating(25);
        var max = T.CreateSaturating(100);

        //Act
        var result = Dummy.Number.Between(min, max).CreateMany(20);

        //Assert
        result.Should().OnlyContain(x => x.CompareTo(min) >= 0 && x.CompareTo(max) <= 0);
    }

    [TestMethod]
    public void BetweenWithoutAmount_Always_ReturnNumbersBetweenBoundaries()
    {
        //Arrange
        var min = T.CreateSaturating(25);
        var max = T.CreateSaturating(100);

        //Act
        var result = Dummy.Number.Between(min, max).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x.CompareTo(min) >= 0 && x.CompareTo(max) <= 0);
    }

    [TestMethod]
    public void Create_Always_GenerateNumberBetweenMinAndMax()
    {
        //Arrange
        var min = T.MinValue;
        var max = T.MaxValue;

        //Act
        var result = Dummy.Number.Create<T>();

        //Assert
        result.Should().BeInRange(min, max);
    }

    [TestMethod]
    public void CreateManyWithoutAmount_Always_GenerateNumberBetweenMinAndMax()
    {
        //Arrange
        var min = T.MinValue;
        var max = T.MaxValue;

        //Act
        var result = Dummy.Number.CreateMany<T>();

        //Assert
        result.Should().OnlyContain(x => x.IsGreaterThanOrEqualTo(min) && x.IsLesserThanOrEqualTo(max));
    }

    [TestMethod]
    public void CreateManyWithAmount_Always_GenerateNumberBetweenMinAndMax()
    {
        //Arrange
        var min = T.MinValue;
        var max = T.MaxValue;

        //Act
        var result = Dummy.Number.CreateMany<T>(20);

        //Assert
        result.Should().OnlyContain(x => x.IsGreaterThanOrEqualTo(min) && x.IsLesserThanOrEqualTo(max));
    }

    [TestMethod]
    public void Create_WhenMaxIsSmallerThanMin_Throw()
    {
        //Arrange
        var min = Dummy.Create<int>();
        var max = Dummy.Number.LessThan(min).Create();

        //Act
        var action = () => Dummy.Number.Between(min, max).Create();

        //Assert
        action.Should().Throw<ArgumentException>().WithMessage(string.Format(ExceptionMessages.MaxMustBeGreaterThanMin, min, max));
    }
}