namespace Dummies.Tests.Customizations.Collections;

[TestClass]
public sealed class ListCustomizationTests : Tester
{
    [TestMethod]
    public void Always_CreateWithMutlipleElements()
    {
        //Arrange
        var anus = Dummy.CreateMany<int>();

        //Act
        var result = Dummy.Create<List<int>>();

        //Assert
        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void WhenCreateIList_CreateList()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IList<string>>();

        //Assert
        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void WhenCreateIEnumerable_CreateList()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IEnumerable<string>>();

        //Assert
        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void WhenCreateIReadOnlyList_CreateList()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IReadOnlyList<string>>();

        //Assert
        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void WhenCreateIReadOnlyCollection_CreateList()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IReadOnlyCollection<string>>();

        //Assert
        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void WhenCreateICollection_CreateList()
    {
        //Arrange

        //Act
        var result = Dummy.Create<ICollection<string>>();

        //Assert
        result.Should().HaveCount(3);
    }

    [TestMethod]
    public void WhenCreateNonGenericIEnumerable_CreateList()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IEnumerable>();

        //Assert
        result.Should().NotBeNull();
    }
}