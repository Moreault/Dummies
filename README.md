![Dummies](https://github.com/Moreault/Dummies/blob/master/dummies.png)
# Dummies

:warning: This project is still in early development. It is not yet ready for production.

## Getting Started

```cs
var dummy = new Dummy();

var person = dummy.Create<Person>();

var people = dummy.CreateMany<Person>(10);
```

## Dummy

The `Dummy` class is the main entry point of the library. It is used to create dummy objects. It is smart enough to create most objects, including from interfaces and abstract classes, with little to no setup required.

## Customizations

Customizations can be applied to the `Dummy` class to change the way it creates objects. Dummies come preloaded with many customizations for common base types, such as `string`, `int`, `DateTime`, etc. Customizations can be applied to any type, including interfaces and abstract classes. These base customizations can also be overriden if they do not meet your needs.

### `ICustomization`
This non-generic `ICustomization` interface is generally used to deal with open generics but it is not limited to it. It can be used to customize any type.

```cs
public class MyCustomization : ICustomization
{
	//Notice that the types are open generics
	public IEnumerable<Type> Types { get; } = [typeof(MyType<>), typeof(IMyType<>)];

	public IDummyBuilder Build(Dummy dummy, Type type)
	{
		return dummy.Build<object>().FromFactory(() => 
		{
			...
		});
	}
}
```

### `ICustomization<T>`
This generic `ICustomization<T>` interface is used to customize a specific type. It can also be used for other types related to `T`, such as interfaces and abstract classes. For most use cases, you can actually inherit from `CustomizationBase<T>` instead of implementing this interface directly.

```cs
public class MyCustomization<T> : ICustomization<T>
{
	//You do still need to specify these (done automatically if you inherit from CustomizationBase<T>)
	public IEnumerable<Type> Types { get; } = [typeof(T)];

	IDummyBuilder ICustomization.Build(Dummy dummy, Type type) => Build(dummy);

    public IDummyBuilder<T> Build(Dummy dummy)
	{
		return dummy.Build<T>().With(x => x.MyProperty, "MyValue");
	}
}
```

### `CustomizationBase<T>`
This class implements `ICustomization<T>` and provides a default implementation for `ICustomization`. It is recommended (but not required) to inherit from this class instead of implementing `ICustomization<T>` directly.

```cs
public class MyCustomization<T> : CustomizationBase<T>
{
	//The Types property is automatically set to [typeof(T)] but it can also be overriden if you need it to apply to more than one type
	public override IDummyBuilder<T> Build(Dummy dummy)
	{
		return dummy.Build<T>().With(x => x.MyProperty, "MyValue");
	}
}
```

### `ListCustomizationBase`
This base customization is used to create list-like collections. Use it if you have a custom collections that can easily be converted from a `List<T>` instead of reinventing the wheel. This `ListCustomizationBase` is used for most list-like collection customizations internally.

```cs
public class MyCustomListCustomization : ListCustomizationBase
{
    public override IEnumerable<Type> Types { get; } = [typeof(MyCustomList<>), typeof(IMyCustomList<>)];

	//Assuming that your custom list has a constructor that takes an IEnumerable<T>
	//This could be a ToMyCustomList() extension method as well or whatever code is required to convert from an IEnumerable<T> to your custom list
    protected override object Convert<T>(IEnumerable<T> source) => new MyCustomList<T>(source);
}
```

### `DictionaryCustomizationBase`
This customization works much in the same way as `ListCustomizationBase` but for dictionary-like collections.

```cs
public class MyCustomDictionaryCustomization : DictionaryCustomizationBase
{
	public override IEnumerable<Type> Types { get; } = [typeof(MyCustomDictionary<,>), typeof(IMyCustomDictionary<,>)];

	//Assuming that your custom dictionary has a constructor that takes an IEnumerable<KeyValuePair<TKey, TValue>>
	//This could be a ToMyCustomDictionary() extension method as well or whatever code is required to convert from an IEnumerable<KeyValuePair<TKey, TValue>> to your custom dictionary
	protected override object Convert<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source) => new MyCustomDictionary<TKey, TValue>(source);
}
```

### The `[AutoCustomization]` attribute
This attribute can be used to apply a customization to any type globally. Just add the attribute to a class implementing `ICustomiztion` or `ICustomization<T>` and it will be used to generate any object of that type.

```cs
[AutoCustomization]
public class MyCustomization : ICustomization
{
    public IEnumerable<Type> Types { get; } = [typeof(MyType), typeof(IMyType)];

	public IDummyBuilder Build(Dummy dummy, Type type)
	{
		// Customize the object here
	}
}
```

Bear in mind that `[AutoCustomization]` should only be used when you want to apply a customization globally. If you want to apply a customization to a specific test case, you should use `Dummy.Customize` instead. `Dummy.Customize` will always take precedence over `[AutoCustomization]`.

## `Dummy.Build` and the `IDummyBuilder` interface
Whenever you use `Dummy.Build<T>`, you get an `IDummyBuilder<T>` instance. This interface is used to customize the object that will be created. It is also used to create the object itself when you call `IDummyBuilder<T>.Create` or `IDummyBuilder<T>.CreateMany`.

```cs
public class Person
{
	public string Name { get; set; }
	public int Age { get; set; }
}

var dummy = new Dummy();

var person = dummy.Build<Person>()
	.With(x => x.Name, "John")
	.With(x => x.Age, 42)
	.Create();

var people = dummy.Build<Person>()
	.With(x => x.Age, 42)
	.CreateMany(10);
```

When a property is not specified, it will be generated automatically based on that type's currently loaded `ICustomization`. It is also important to note that the Dummies framework allows you to build on top of customizations. This means that whenever you use `Build`, it will use that type's customization (if there is any) and apply your case-specific additions afterwards. You can also opt out of this by using the chainable `IDummyBuilder.WithoutCustomizations()` method.

```cs

public class Person
{
	public string Name { get; set; }
	public int Age { get; set; }
}

public class PersonCustomization : CustomizationBase<Person>
{
	public override IDummyBuilder<Person> Build(Dummy dummy)
	{
		return dummy.Build<Person>()
			.With(x => x.Name, "John");
	}
}

var dummy = new Dummy();

var person = dummy.Build<Person>()
	.With(x => x.Age, 42)
	.Create();

//person.Name == "John"
//person.Age == 42

//If you did this instead
var person = dummy.Build<Person>()
	.WithoutCustomizations()
	.With(x => x.Age, 42)
	.Create();

//person.Name == "(Guid)"
//person.Age == 42
```

### `With` and `Without` methods
These methods can be used to tell the builder what to do with each property or field. They are chainable and can be used multiple times. They can also be used to specify a value for a property or field that would otherwise be generated automatically.

```cs
public class Person
{
	public string Name { get; set; }
	public int Age { get; set; }
}

var dummy = new Dummy();

var person = dummy.Build<Person>()
	.With(x => x.Name, "John")
	.With(x => x.Age, 42)
	.Create();

//person.Name == "John"

var person = dummy.Build<Person>()
	.Without(x => x.Name)
	.With(x => x.Age, 42)
	.Create();

//person.Name == null
```

### FromFactory
`FromFactory` is generally used to specify a custom factory method to create the object such as when the object has no public constructor or when you want to use a specific constructor. By default, `FromFactory` will not generate values for any properties or fields since it assumes that you are providing all the generation logic needed. You can still use `With` and `Without` to specify what to do with each property or field and they will be applied "on top" of the `FromFactory` method.

```cs
public class Airplane
{
	public string Model { get; init; }
	public int Year { get; init; }
	public int MaxPassengers { get; init; }

	public Airplane(string model, int year)
	{
		Model = model;
		Year = year;
		MaxPassengers = 100;
	}

	public Airplane(string model, int year, int maxPassengers)
	{
		Model = model;
		Year = year;
		MaxPassengers = maxPassengers;
	}
}

var dummy = new Dummy();

var airplane = dummy.Build<Airplane>()
	.FromFactory(() => new Airplane("Boeing 747", 1969))
	.With(x => x.MaxPassengers, 200)
	.Create();
```

### `As<T>`
This is used to specify a type to cast the "internal" object to.

## Excluding enum values from generation
By default, Dummies will generate a random enum value for any enum type. You can exclude values from being generated by using the `Dummy.Exclude` method. This method is on both `Dummy` and `IDummyBuilder` and is also chainable in both cases.

```cs

public enum MyEnum
{
	One,
	Two,
	Three
}

var dummy = new Dummy();

var myEnum = dummy.Create<MyEnum>();

//myEnum will be either MyEnum.One, MyEnum.Two or MyEnum.Three

var myEnum = dummy.Exclude(MyEnum.One).Create<MyEnum>();

//myEnum will be either MyEnum.Two or MyEnum.Three

var myEnum = dummy.Exclude(MyEnum.One, MyEnum.Two).Create<MyEnum>();

//myEnum will be MyEnum.Three
```

The enum values will also be excluded from any other properties or fields that are of that enum type on all objects that are generated using that `Dummy` instance.

## The `IDummyNumberBuilder`
You can use the `Dummy.Number` property to have more control over how numbers are generated such as `LessThan`, `GreaterThan` and `Between`. Do keep in mind that using the `IDummyNumberGenerator` will bypass any customizations that are loaded for that type. To use number customizations, you should use `Dummy.Create<int>` (where `int` is the type of number you want to generate). This builder will also attempt to return unique numbers each time but it may give up on that if the range is very limited.

```cs

```

## The `DateTimeDummyBuilder`
You can use the `Dummy.DateTime` property to have more control over how `DateTime` objects are generated such as `Before`, `After` and `Between`. Do keep in mind that using the `IDateTimeDummyBuilder` will bypass any customizations that are loaded for that type. To use `DateTime` customizations, you should use `Dummy.Create<DateTime>`. This builder will also attempt to return unique `DateTime` objects each time but it may give up on that if the range is very limited between two dates.

```cs

```

## The `DummyStringBuilder`
You can use the `Dummy.String` property to have more control over how `string` objects are generated such as `WithLength`. Do keep in mind that using the `DummyStringBuilder` will bypass any customizations that are loaded for that type. To use `string` customizations, you should use `Dummy.Create<string>`. This builder will also attempt to return unique `string` objects each time but it may give up on that if your instructions make possibilities very limited.

```cs
var result = dummy.String.WithLength.Exactly(10).Create();

var result = dummy.String.WithLength.LessThan(24).Create();

var result = dummy.String.WithLength.Between(75, 100).CreateMany();
```