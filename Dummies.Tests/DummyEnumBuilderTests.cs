namespace Dummies.Tests;

[TestClass]
public sealed class DummyEnumBuilderTests : Tester
{
    public enum GarbageEnum
    {
        One,
        Two,
        Three,
        Four,
        Five
    }

    [TestMethod]
    public void Create_WhenIsVanilla_ReturnAnyEnumValue()
    {
        //Arrange

        //Act
        var result = Dummy.Enum<GarbageEnum>().Create();

        //Assert
        result.Should().BeOneOf(EnumUtils.ToList<GarbageEnum>());
    }

    [TestMethod]
    public void CreateManyWithoutCount_WhenIsVanilla_ReturnAnyEnumValue()
    {
        //Arrange

        //Act
        var result = Dummy.Enum<GarbageEnum>().CreateMany();

        //Assert
        foreach (var value in result)
            value.Should().BeOneOf(EnumUtils.ToList<GarbageEnum>());
    }

    [TestMethod]
    public void CreateMany_WhenIsVanilla_ReturnAnyEnumValue()
    {
        //Arrange

        //Act
        var result = Dummy.Enum<GarbageEnum>().CreateMany(100);

        //Assert
        result.Distinct().Should().BeEquivalentTo(EnumUtils.ToList<GarbageEnum>());
    }

    [TestMethod]
    public void OneOf_WhenIsEmpty_Throw()
    {
        //Arrange

        //Act
        var action = () => Dummy.Enum<GarbageEnum>().OneOf().Create();

        //Assert
        action.Should().Throw<ArgumentException>().WithParameterName("values");
    }

    [TestMethod]
    public void OneOf_WhenNotEmpty_AlwaysReturnOneOf()
    {
        //Arrange
        var values = new[] { GarbageEnum.Two, GarbageEnum.Four, GarbageEnum.Five };

        //Act
        var result = Dummy.Enum<GarbageEnum>().OneOf(values).CreateMany(100);

        //Assert
        result.Distinct().Should().BeEquivalentTo(new List<GarbageEnum> { GarbageEnum.Two, GarbageEnum.Four, GarbageEnum.Five });
    }

    [TestMethod]
    public void Exclude_WhenEmpty_ReturnAll()
    {
        //Arrange

        //Act
        var result = Dummy.Enum<GarbageEnum>().Exclude().CreateMany(100);

        //Assert
        result.Distinct().Should().BeEquivalentTo(EnumUtils.ToList<GarbageEnum>());
    }

    [TestMethod]
    public void Exclude_WhenNotEmpty_ReturnAllButValuesPassed()
    {
        //Arrange

        //Act
        var result = Dummy.Enum<GarbageEnum>().Exclude(GarbageEnum.Three).CreateMany(100);

        //Assert
        result.Distinct().Should().BeEquivalentTo(new List<GarbageEnum> { GarbageEnum.One, GarbageEnum.Two, GarbageEnum.Four, GarbageEnum.Five });

    }
}