using ToolBX.Dummies.Customizations;

namespace Dummies.Tests;

[TestClass]
public class DummyTests : Tester
{
    [TestMethod]
    public void WhenCreateInterfaceFromDotNetFramework_CreateProxyImplementingInterface()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IFormatProvider>();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo(typeof(IFormatProvider));
    }

    [TestMethod]
    public void WhenCreateNonGeneric_ReturnAsObject()
    {
        //Arrange

        //Act
        var result = Dummy.Create(typeof(string));

        //Assert
        result.Should().BeOfType<string>();
    }

    public sealed record BogusType
    {
        public string A { get; init; }
        public int B { get; init; }
        public int C { get; init; }
        public char D { get; init; }
    }

    [TestMethod]
    public void WhenUsingBuild_OverrideDefaultBehavior()
    {
        //Arrange

        //Act
        var result = Dummy.Build<BogusType>().With(x => x.A, "Hello").Create();

        //Assert
        result.A.Should().Be("Hello");
    }

    public sealed class BogusTypeCustomization : CustomizationBase<BogusType>
    {
        public override IDummyBuilder<BogusType> Build(Dummy dummy) => dummy.Build<BogusType>().With(x => x.B, 14);
    }

    [TestMethod]
    public void WhenUsingBuildOnTopOfCustomization_HonorBoth()
    {
        //Arrange
        Dummy.Customize(new BogusTypeCustomization());

        //Act
        var result = Dummy.Build<BogusType>().With(x => x.A, "Hello").Create();

        //Assert
        result.A.Should().Be("Hello");
        result.B.Should().Be(14);
    }

    public enum BogusEnum
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G
    }

    [TestMethod]
    public void WhenExcludingEnumValues_DoNotUseIt()
    {
        //Arrange
        Dummy.Exclude(BogusEnum.C, BogusEnum.E, BogusEnum.G);

        //Act
        var result = Dummy.CreateMany<BogusEnum>(20);

        //Assert
        result.Should().NotContain(BogusEnum.C);
        result.Should().NotContain(BogusEnum.E);
        result.Should().NotContain(BogusEnum.G);
    }

    [TestMethod]
    public void CreateManyNonGeneric_WhenEnumeratingMultipleTimes_ShouldAlwaysBeTheSameResult()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany(typeof(string));

        //Assert
        result.ToList().Should().BeEquivalentTo(result.ToList());
    }

    [TestMethod]
    public void CreateManyGeneric_WhenEnumeratingMultipleTimes_ShouldAlwaysBeTheSameResult()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<string>();

        //Assert
        result.ToList().Should().BeEquivalentTo(result.ToList());
    }

    public interface IGarbage
    {
        string A { get; init; }
        int B { get; init; }
        int C { get; init; }
        char D { get; set; }
        long Get();
        void Void();
    }

    [TestMethod]
    public void Create_WhenIsInterface_ThenMethodsWithReturnValuesShouldReturnDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IGarbage>();

        //Assert
        result.Get().Should().Be(0);
    }

    [TestMethod]
    public void Create_WhenIsInterface_ThenVoidMethodsShouldNotThrow()
    {
        //Arrange

        //Act
        var action = () => Dummy.Create<IGarbage>().Void();

        //Assert
        action.Should().NotThrow();
    }
}