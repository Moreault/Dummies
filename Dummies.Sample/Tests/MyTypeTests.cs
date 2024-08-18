namespace Dummies.Sample.Tests;

[TestClass]
public sealed class MyTypeTests : Tester
{
    //Id is always numeric because even though it's a string, its auto-loaded customization makes it numeric
    [TestMethod]
    public void Id_Always_Numeric()
    {
        //Arrange

        //Act
        var result = Dummy.Create<MyType>();

        //Assert
        result.Id.ToIntOrThrow().Should().BeGreaterThan(0);
    }

    //Customizations are always applied unless you specifically ask it not to apply them
    [TestMethod]
    public void Create_WhenBuildingWithYearsActive_StillUseAutoCustomizationForId()
    {
        //Arrange

        //Act
        var result = Dummy.Build<MyType>().With(x => x.YearsActive, x => x.Number.Between(0, 5).Create()).Create();

        //Assert
        result.Id.ToIntOrThrow().Should().BeGreaterThan(0);
        result.YearsActive.Should().BeInRange(0, 5);
    }

    //You can ignore all customizations by using the WithoutCustomizations method
    [TestMethod]
    public void Create_WhenBuildingWithoutCustomizations_IdShouldBeText()
    {
        //Arrange

        //Act
        var result = Dummy.Build<MyType>().With(x => x.YearsActive, x => x.Number.Between(0, 5).Create()).WithoutCustomizations().Create();

        //Assert
        result.Id.ToInt().IsSuccess.Should().BeFalse();
        result.YearsActive.Should().BeInRange(0, 5);
    }
}