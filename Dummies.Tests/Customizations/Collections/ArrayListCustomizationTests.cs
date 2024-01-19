namespace Dummies.Tests.Customizations.Collections;

[TestClass]
public sealed class ArrayListCustomizationTests : Tester
{
    [TestMethod]
    public void Always_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<ArrayList>();

        //Assert
        result.Count.Should().Be(3);
    }

    [TestMethod]
    public void WhenIsIEnumerable_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IEnumerable>();

        //Assert
        result.Count().Should().Be(3);
    }

    [TestMethod]
    public void WhenIsICollection_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<ICollection>();

        //Assert
        result.Count().Should().Be(3);
    }

    [TestMethod]
    public void WhenIsIList_CreateWithMutlipleElements()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IList>();

        //Assert
        result.Count().Should().Be(3);
    }
}

internal static class OldEnumerableExtensions
{
    internal static int Count(this IEnumerable source)
    {
        return Enumerable.Count(source.Cast<object?>());
    }
}