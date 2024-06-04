namespace Dummies.Tests;

[TestClass]
public sealed class CustomizeTests : Tester
{
    public sealed class Unusable
    {
        public int Number
        {
            get => _number;
            init => throw new NotImplementedException();
        }
        private readonly int _number = Random.Shared.Next(1000);

        public string Explanation { get; init; } = string.Empty;
    }

    public sealed class UnusableCustomization : CustomizationBase
    {
        protected override IEnumerable<Type> Types { get; } = [typeof(Unusable)];

        protected override IDummyBuilder BuildMe(IDummy dummy, Type type) => dummy.Build<object>().FromFactory(() => new Unusable
        {
            Explanation = dummy.Create<string>()
        });
    }

    [TestMethod]
    public void Create_WhenNotUsingCustomizations_ThrowNotSupportedException()
    {
        //Arrange

        //Act
        //It throws because Number's init accessor throws and it uses it by default without a customization or specific instructions to not generate properties
        var action = () => Dummy.Create<Unusable>();

        //Assert
        action.Should().Throw<TargetInvocationException>();
    }

    [TestMethod]
    public void Create_WhenUsingCustomizationManually_DoNotThrow()
    {
        //Arrange
        Dummy.Customize(new UnusableCustomization());

        //Act
        var action = () => Dummy.Create<Unusable>();

        //Assert
        action.Should().NotThrow();
    }

    public sealed class Unusable<T>
    {
        public int Number
        {
            get => _number;
            init => throw new NotImplementedException();
        }
        private readonly int _number = Random.Shared.Next(1000);

        public T Value { get; init; } = default!;
    }

    public sealed class GenericUnusableCustomization : CustomizationBase
    {
        protected override IEnumerable<Type> Types { get; } = [typeof(Unusable<>)];

        protected override IDummyBuilder BuildMe(IDummy dummy, Type type) => dummy.Build<object>().FromFactory(() =>
        {
            var valueType = type.GetGenericArguments()[0];
            var value = dummy.Create(valueType);

            var instance = Activator.CreateInstance(type)!;
            var property = type.GetSingleProperty(x => x.Name == nameof(Unusable<int>.Value));

            property.SetValue(instance, value);

            return instance;
        });
    }

    [TestMethod]
    public void Create_WhenNotUsingCustomizationsForOpenGeneric_ThrowNotSupportedException()
    {
        //Arrange

        //Act
        //It throws because Number's init accessor throws and it uses it by default without a customization or specific instructions to not generate properties
        var action = () => Dummy.Create<Unusable<string>>();

        //Assert
        action.Should().Throw<TargetInvocationException>();
    }

    [TestMethod]
    public void Create_WhenUsingCustomizationManuallyForOpenGeneric_DoNotThrow()
    {
        //Arrange
        Dummy.Customize(new GenericUnusableCustomization());

        //Act
        var action = () => Dummy.Create<Unusable<string>>();

        //Assert
        action.Should().NotThrow();
    }
}