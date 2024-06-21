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

    public sealed record GarbageType
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
        var result = Dummy.Build<GarbageType>().With(x => x.A, "Hello").Create();

        //Assert
        result.A.Should().Be("Hello");
    }

    public sealed class BogusTypeCustomization : CustomizationBase<GarbageType>
    {
        public override IDummyBuilder<GarbageType> Build(IDummy dummy) => dummy.Build<GarbageType>().With(x => x.B, 14);
    }

    [TestMethod]
    public void WhenUsingBuildOnTopOfCustomization_HonorBoth()
    {
        //Arrange
        Dummy.Customize(new BogusTypeCustomization());

        //Act
        var result = Dummy.Build<GarbageType>().With(x => x.A, "Hello").Create();

        //Assert
        result.A.Should().Be("Hello");
        result.B.Should().Be(14);
    }

    public enum GarbageEnum
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
    public void Create_WhenEnum_AlwaysCreateDefinedValueByDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Create<GarbageEnum>();

        //Assert
        result.Should().BeDefined();
    }

    [TestMethod]
    public void WhenExcludingEnumValues_DoNotUseIt()
    {
        //Arrange
        Dummy.Exclude(GarbageEnum.C, GarbageEnum.E, GarbageEnum.G);

        //Act
        var result = Dummy.CreateMany<GarbageEnum>(20);

        //Assert
        result.Should().NotContain(GarbageEnum.C);
        result.Should().NotContain(GarbageEnum.E);
        result.Should().NotContain(GarbageEnum.G);
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

    public sealed class RecursiveGarbage
    {
        public int Id { get; init; }
        public RecursiveGarbage Friend { get; init; } = null!;
        public List<RecursiveGarbage> Friends { get; init; } = [];
        public BicursiveGarbage Relative { get; init; } = null!;
    }

    public sealed class BicursiveGarbage
    {
        public RecursiveGarbage Friend { get; init; } = null!;
    }

    [TestMethod]
    public void Create_WhenTypeIsRecursive_OnlyCreateObjectWithMaxDepth()
    {
        //Arrange
        DummyOptions.Global.MaximumDepth = 3;

        //Act
        var result = Dummy.Create<RecursiveGarbage>();

        //Assert
        result.Friend.Friend.Friend.Friend.Should().BeNull();
    }

    public sealed class GarbageWithPrivateConstructor
    {
        public int Id { get; }

        private GarbageWithPrivateConstructor(int id)
        {
            Id = id;
        }
    }

    [TestMethod]
    public void Create_WhenTypeOnlyHasPrivateConstructor_Create()
    {
        //Arrange

        //Act
        var result = Dummy.Create<GarbageWithPrivateConstructor>();

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
    }
}