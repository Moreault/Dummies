namespace Dummies.Tests.Customizations.ComplexTypes;

[TestClass]
public sealed class EqualityComparerOfIntCustomizationTests : EqualityComparerCustomizationTester<int>;

[TestClass]
public sealed class EqualityComparerOfStringCustomizationTests : EqualityComparerCustomizationTester<string>;

public abstract class EqualityComparerCustomizationTester<T> : Tester
{
    [TestMethod]
    public void Equals_WhenComparingWithSameValues_ShouldBeTrue()
    {
        //Arrange
        var comparer = Dummy.Create<EqualityComparer<T>>();
        var value1 = Dummy.Create<T>();
        var value2 = value1;

        //Act
        var result = comparer.Equals(value1, value2);

        //Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void Equals_WhenComparingWithSameValuesUsingInterface_ShouldBeTrue()
    {
        //Arrange
        var comparer = Dummy.Create<IEqualityComparer<T>>();
        var value1 = Dummy.Create<T>();
        var value2 = value1;

        //Act
        var result = comparer.Equals(value1, value2);

        //Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void Equals_WhenComparingWithDifferentValues_ShouldBeFalse()
    {
        //Arrange
        var comparer = Dummy.Create<EqualityComparer<T>>();
        var value1 = Dummy.Create<T>();
        var value2 = Dummy.Create<T>();

        //Act
        var result = comparer.Equals(value1, value2);

        //Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void Equals_WhenComparingWithDifferentValuesUsingInterface_ShouldBeFalse()
    {
        //Arrange
        var comparer = Dummy.Create<IEqualityComparer<T>>();
        var value1 = Dummy.Create<T>();
        var value2 = Dummy.Create<T>();

        //Act
        var result = comparer.Equals(value1, value2);

        //Assert
        result.Should().BeFalse();
    }
}