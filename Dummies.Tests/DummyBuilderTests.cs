﻿namespace Dummies.Tests;

[TestClass]
public sealed class DummyBuilderTests : Tester
{
    public sealed record Garbage
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public long Experience { get; init; }
        public short Level { get; init; }
        public float Health { get; init; }
        public double Mana { get; init; }
        public decimal Gold { get; init; } = 125;
        public sbyte Symbol { get; init; }
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeSByte_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Symbol, -Dummy.Create<sbyte>()).Create();

        //Assert
        result.Symbol.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeShort_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Level, -Dummy.Create<short>()).Create();

        //Assert
        result.Level.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeInt_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Experience, -Dummy.Create<long>()).Create();

        //Assert
        result.Experience.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeFloat_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Health, -Dummy.Create<float>()).Create();

        //Assert
        result.Health.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeDouble_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Mana, -Dummy.Create<double>()).Create();

        //Assert
        result.Mana.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeDecimal_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Gold, -Dummy.Create<decimal>()).Create();

        //Assert
        result.Gold.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeSByteAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Symbol, () => -Dummy.Create<sbyte>()).Create();

        //Assert
        result.Symbol.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeShortAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Level, () => -Dummy.Create<short>()).Create();

        //Assert
        result.Level.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeIntAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Experience, () => -Dummy.Create<long>()).Create();

        //Assert
        result.Experience.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeFloatAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Health, () => -Dummy.Create<float>()).Create();

        //Assert
        result.Health.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeDoubleAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Mana, () => -Dummy.Create<double>()).Create();

        //Assert
        result.Mana.Should().BeNegative();
    }

    [TestMethod]
    public void WhenUsingWithOnNegativeDecimalAsFunc_ReturnActualNumberWithoutThrowing()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Gold, () => -Dummy.Create<decimal>()).Create();

        //Assert
        result.Gold.Should().BeNegative();
    }

    public interface IGarbage;
    public sealed record GarbageOne : IGarbage;
    public sealed record GarbageTwo : IGarbage;
    public sealed record GarbageThree : IGarbage;

    [TestMethod]
    public void FromTypes_WhenPassingNull_Throw()
    {
        //Arrange

        //Act
        var action = () => Dummy.Build<IGarbage>().FromTypes(null!);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("types");
    }

    [TestMethod]
    public void FromTypes_WhenTypesIsEmpty_Throw()
    {
        //Arrange

        //Act
        var action = () => Dummy.Build<IGarbage>().FromTypes([]).Create();

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void FromTypes_WhenContainsOnlyOneType_AlwaysCreateThatOneType()
    {
        //Arrange

        //Act
        var result = Dummy.Build<IGarbage>().FromTypes(typeof(GarbageTwo)).CreateMany(10).ToList();

        //Assert
        result.DistinctBy(x => x.GetType()).Count().Should().Be(1);
    }

    [TestMethod]
    public void FromTypes_WhenContainsMultipleTypes_ReturnsARandomType()
    {
        //Arrange

        //Act
        var result = Dummy.Build<IGarbage>().FromTypes(typeof(GarbageOne), typeof(GarbageTwo), typeof(GarbageThree)).CreateMany(10).ToList();

        //Assert
        result.DistinctBy(x => x.GetType()).Count().Should().BeGreaterThan(1);
    }

    [TestMethod]
    public void WithUsingValue_WhenExpressionIsNull_Throw()
    {
        //Arrange
        Expression<Func<Garbage, string>> member = null!;
        var value = Dummy.Create<string>();

        //Act
        var action = () => Dummy.Build<Garbage>().With(member, value);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(member));
    }

    [TestMethod]
    public void WithUsingValue_WhenValueIsNull_GenerateWithNull()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Name, (string)null!).Create();

        //Assert
        result.Name.Should().BeNull();
    }

    [TestMethod]
    public void WithUsingValue_WhenExpressionAndValueAreNotNull_GenerateWithSpecifiedValue()
    {
        //Arrange
        var value = Dummy.Create<string>();

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Name, value).CreateMany().ToList();

        //Assert
        result.Should().OnlyContain(x => x.Name == value);
        result.Distinct().Should().HaveCount(result.Count);
    }

    [TestMethod]
    public void WithUsingFunc_WhenExpressionIsNull_Throw()
    {
        //Arrange
        Expression<Func<Garbage, string>> member = null!;


        //Act
        var action = () => Dummy.Build<Garbage>().With(member, () => Dummy.Create<string>()).Create();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(member));
    }

    [TestMethod]
    public void WithUsingFunc_WhenValueExpressionIsNull_Throw()
    {
        //Arrange
        Func<string> value = null!;

        //Act
        var action = () => Dummy.Build<Garbage>().With(x => x.Name, value).Create();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(value));
    }

    [TestMethod]
    public void WithUsingFunc_WhenValueExpressionIsNotConstant_GenerateWithDifferentValueEverytime()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Name, () => Coin.Flip("One", "Two")).CreateMany(15).ToList();

        //Assert
        result.DistinctBy(x => x.Name).Should().HaveCount(2);
    }

    [TestMethod]
    public void WithUsingFunc_WhenValueExpressionIsConstant_AlwaysGenerateWithValue()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Name, () => "Constant").CreateMany(15).ToList();

        //Assert
        result.DistinctBy(x => x.Name).Should().HaveCount(1);
    }

    [TestMethod]
    public void Without_WhenExpressionIsNull_Throw()
    {
        //Arrange

        //Act
        var action = () => Dummy.Build<Garbage>().Without((Expression<Func<Garbage, string>>)null!);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("member");
    }

    [TestMethod]
    public void Without_WhenExpressionIsNotNull_GenerateWithoutSpecifiedProperty()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().Without(x => x.Name).CreateMany();

        //Assert
        result.Should().OnlyContain(x => string.IsNullOrWhiteSpace(x.Name));
    }

    [TestMethod]
    public void WithUsingDummyValue_WhenMemberIsNull_Throw()
    {
        //Arrange
        Expression<Func<Garbage, string>> member = null!;

        //Act
        var action = () => Dummy.Build<Garbage>().With(member, x => x.String.WithLength.Exactly(14).Create()).Create();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(member));
    }

    [TestMethod]
    public void WithUsingDummyValue_WhenValueIsNull_Throw()
    {
        //Arrange
        Func<IDummy, long> value = null!;

        //Act
        var action = () => Dummy.Build<Garbage>().With(x => x.Experience, value).Create();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(value));
    }

    [TestMethod]
    public void WithUsingDummyValue_Always_CreateWithConstantValue()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Gold, x => x.Number.WithDecimals().Between<decimal>(1, 100).Create()).CreateMany(25).ToList();

        //Assert
        result.Should().OnlyContain(x => x.Gold >= 1 && x.Gold <= 100);
        result.DistinctBy(x => x.Gold).Should().HaveCount(1);
    }

    [TestMethod]
    public void WithUsingDummyFunc_WhenMemberIsNull_Throw()
    {
        //Arrange
        Expression<Func<Garbage, string>> member = null!;

        //Act
        var action = () => Dummy.Build<Garbage>().With(member, x => () => x.String.WithLength.Exactly(14).Create()).Create();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(member));
    }

    [TestMethod]
    public void WithUsingDummyFunc_WhenFuncIsNull_Throw()
    {
        //Arrange
        Func<IDummy, Func<long>> value = null!;

        //Act
        var action = () => Dummy.Build<Garbage>().With(x => x.Experience, value).Create();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(value));
    }

    [TestMethod]
    public void WithUsingDummyFunc_Always_CreateADifferentValue()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().With(x => x.Gold, x => () => x.Number.WithDecimals().Between<decimal>(1, 100).Create()).CreateMany(25).ToList();

        //Assert
        result.Should().OnlyContain(x => x.Gold >= 1 && x.Gold <= 100);
        result.DistinctBy(x => x.Gold).Should().HaveCount(25);
    }

    [TestMethod]
    public void Omit_WhenMemberIsNull_Throw()
    {
        //Arrange
        Expression<Func<Garbage, decimal>> member = null!;

        //Act
        var action = () => Dummy.Build<Garbage>().Omit(member).Create();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(member));
    }

    [TestMethod]
    public void Omit_WhenMemberHasDefaultValue_LeaveDefaultValue()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().Omit(x => x.Gold).CreateMany();

        //Assert
        result.Should().OnlyContain(x => x.Gold == 125);
    }

    [TestMethod]
    public void CreateMany_WhenAmountIsNegative_Throw()
    {
        //Arrange
        var amount = -Dummy.Create<int>();

        //Act
        var action = () => Dummy.Build<Garbage>().CreateMany(amount);

        //Assert
        action.Should().Throw<ArgumentException>().WithMessage(string.Format(ExceptionMessages.CannotCreateNegativeOrZeroObjects, amount));
    }

    public sealed record FieldedGarbage
    {
        public string Name = "Seb";
        public int Id = 200;
    }

    [TestMethod]
    public void With_WhenIsField_AssignToFields()
    {
        //Arrange

        //Act
        var result = Dummy.Build<FieldedGarbage>().With(x => x.Name, "Roger").With(x => x.Id, 98).Create();

        //Assert
        result.Should().Be(new FieldedGarbage
        {
            Name = "Roger",
            Id = 98
        });
    }

    [TestMethod]
    public void Omit_WhenIsField_IgnoreField()
    {
        //Arrange

        //Act
        var result = Dummy.Build<FieldedGarbage>().Omit(x => x.Name).With(x => x.Id, 5).Create();

        //Assert
        result.Should().Be(new FieldedGarbage
        {
            Name = "Seb",
            Id = 5
        });
    }

    [TestMethod]
    public void Without_WhenIsField_SetFieldToDefaultValue()
    {
        //Arrange

        //Act
        var result = Dummy.Build<FieldedGarbage>().Without(x => x.Name).With(x => x.Id, 104).Create();

        //Assert
        result.Should().Be(new FieldedGarbage
        {
            Name = null!,
            Id = 104
        });
    }

    [TestMethod]
    public void OmitAutoProperties_WhenIsFieldedObject_OmitAll()
    {
        //Arrange

        //Act
        var result = Dummy.Build<FieldedGarbage>().OmitAutoProperties().Create();

        //Assert
        result.Should().Be(new FieldedGarbage
        {
            Name = "Seb",
            Id = 200
        });
    }

    [TestMethod]
    public void WithoutAutoProperties_WhenIsFieldedObject_AllPropertiesShouldBeDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Build<FieldedGarbage>().WithoutAutoProperties().Create();

        //Assert
        result.Should().Be(new FieldedGarbage
        {
            Name = null!,
            Id = 0
        });
    }

    [TestMethod]
    public void With_WhenCombinedWithOmitAutoProperties_DoNotOmitSpecifiedProperties()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().OmitAutoProperties().With(x => x.Experience, 200).With(x => x.Name, "Seb").Create();

        //Assert
        result.Should().Be(new Garbage
        {
            Name = "Seb",
            Experience = 200
        });
    }

    [TestMethod]
    public void With_WhenCombinedWithWithoutAutoProperties_DoNotSetSSpecifiedPropertiesToDefault()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().WithoutAutoProperties().With(x => x.Gold, 200).Create();

        //Assert
        result.Should().Be(new Garbage
        {
            Name = null!,
            Gold = 200,
            Id = 0,
            Experience = 0,
            Health = 0,
            Level = 0,
            Mana = 0,
            Symbol = 0
        });
    }

    public sealed record CustomizedGarbage
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Experience { get; init; }
        public int Level { get; init; }
    }

    [AutoCustomization]
    public sealed class CustomizedGarbageCustomization : CustomizationBase<CustomizedGarbage>
    {
        public override IDummyBuilder<CustomizedGarbage> Build(IDummy dummy) => dummy.Build<CustomizedGarbage>().FromFactory(() => new CustomizedGarbage
        {
            Name = "User",
            Level = -dummy.Number.Between(1, 20).Create(),
            Experience = -dummy.Number.Between(0, 90).Create()
        });
    }

    [TestMethod]
    public void WithoutCustomizations_WhenNoCustomizationWasLoaded_BuildAsUsual()
    {
        //Arrange

        //Act
        var result = Dummy.Build<Garbage>().WithoutCustomizations().Create();

        //Assert
        result.Name.Should().NotBeNullOrWhiteSpace();
        result.Level.Should().BeGreaterOrEqualTo(1);
        result.Experience.Should().BeGreaterOrEqualTo(1);
        result.Gold.Should().BeGreaterOrEqualTo(1);
        result.Health.Should().BeGreaterOrEqualTo(1);
        result.Id.Should().BeGreaterOrEqualTo(1);
        result.Symbol.Should().BeGreaterOrEqualTo(1);
    }

    [TestMethod]
    public void WithoutCustomizations_WhenCustomizationsForTypeIsLoaded_IgnoreIt()
    {
        //Arrange

        //Act
        var result = Dummy.Build<CustomizedGarbage>().WithoutCustomizations().Create();

        //Assert
        result.Name.Should().NotBe("User");
        result.Level.Should().BeGreaterOrEqualTo(1);
        result.Experience.Should().BeGreaterOrEqualTo(1);
        result.Id.Should().BeGreaterOrEqualTo(1);
    }

    [TestMethod]
    public void WithoutCustomizations_WhenAddingSpecifics_IgnoreCustomization()
    {
        //Arrange

        //Act
        var result = Dummy.Build<CustomizedGarbage>().WithoutCustomizations().With(x => x.Name, "Seb").With(x => x.Id, 99).Create();

        //Assert
        result.Name.Should().Be("Seb");
        result.Level.Should().BeGreaterOrEqualTo(1);
        result.Experience.Should().BeGreaterOrEqualTo(1);
        result.Id.Should().Be(99);
    }

    [TestMethod]
    public void With_WhenBuildingCustomizedObject_OverrideSpecifiedProperties()
    {
        //Arrange

        //Act
        var result = Dummy.Build<CustomizedGarbage>().With(x => x.Name, "Seb").With(x => x.Id, 99).Create();

        //Assert
        result.Name.Should().Be("Seb");
        result.Level.Should().BeInRange(-20, -1);
        result.Experience.Should().BeInRange(-90, 0);
        result.Id.Should().Be(99);
    }

    public sealed record GarbageWithEnum
    {
        public string Name { get; init; } = null!;
        public GarbageEnum Value { get; init; }
    }

    public enum GarbageEnum
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }

    [TestMethod]
    public void ExcludeEnum_Always_ExcludeThoseValuesFromCreation()
    {
        //Arrange

        //Act
        var result = Dummy.Build<GarbageWithEnum>().Exclude(GarbageEnum.Two, GarbageEnum.Five).CreateMany(50).ToList();

        //Assert
        result.Should().OnlyContain(x => x.Value == GarbageEnum.One || x.Value == GarbageEnum.Three || x.Value == GarbageEnum.Four || x.Value == GarbageEnum.Six);
    }
}