![Dummies](https://github.com/Moreault/Dummies/blob/master/dummies.png)
# Dummies

:warning: This project is still in pre-release

Dummies makes it easier to write unit tests by automating part of, or the entirety of, the creation of objects. It is flexible and allows you to customize how some objects are generated. It draws a lot of inspiration from [AutoFixture](https://github.com/AutoFixture/AutoFixture) and the two look a lot alike on the surface. If you are familiar with AutoFixture, then Dummies should come naturally to you.

## Getting Started

```cs
var dummy = new Dummy();

var person = dummy.Create<Person>();

var people = dummy.CreateMany<Person>(10);
```

## Dummy

The `Dummy` class is the main entry point of the library. It is used to create dummy objects. It is smart enough to create most objects, including from interfaces and abstract classes, with little to no setup required.

## Customizing generation

Customizations may be applied to a `Dummy` instance to control the way it generates objects of a given type. They may also be applied project-wide using the `[AutoCustomization]` attribute.

The following examples are taken from the Sample project.

### `ICustomization`
Base interface for all customizations. For most cases, you should use one of the abstract classes which do most of the heavy lifting for common use cases. Use `ICustomization` when none of the base customization classes cover your needs.

```cs
public class MyCustomization : ICustomization
{
    public Func<Type, bool> Condition => x == typeof(MyType);

	public IDummyBuilder Build(Dummy dummy, Type type)
	{
		return dummy.Build<object>().FromFactory(() => 
		{
			...
		});
	}
}
```

### CustomizationBase
Again, this one should only be used directly when you're working with very uncommon cases that are not covered by more "specialized" customizations.

### CustomizationBase<T>
Preferred way of handling customizations when you are not testing generic types (such as an `IEnumerable<T>` for instance).

Note that the example does not use the `FromFactory` method. Indeed, `FromFactory` is not always required. The `Build` method only expects a `IDummyBuilder<T>` to be returned. 
Not using the `Factory` method ensures that other properties and fields will use default (or customized) generation. 
If you come from AutoFixture, you may be surprised to learn that Dummy's `FromFactory` method omits all properties and fields by default. 

```cs
//Follows the assumption that MyType's Id is a numeric string
[AutoCustomization]
public sealed class MyTypeCustomization : CustomizationBase<MyType>
{
    protected override IEnumerable<Type> AdditionalTypes { get; } = [typeof(IMyType)];

    public override IDummyBuilder<MyType> Build(IDummy dummy) => dummy.Build<MyType>().With(x => x.Id, x => x.Create<int>().ToString());
}
```

### OpenGenericCustomizationBase
Use this base customization when working with open generics.

```cs
//When the generic type argument (Id) is a string, make it numeric but don't do any special manipulation if it's any other type
[AutoCustomization]
public sealed class MyGenericTypeCustomization : OpenGenericCustomizationBase
{
    protected override IEnumerable<Type> Types { get; } = [typeof(MyGenericType<>), typeof(IMyGenericType<>)];
    
    protected override object FromFactory<T>(IDummy dummy)
    {
        if (typeof(T) == typeof(string))
            return new MyGenericType<string> { Id = dummy.Create<int>().ToString() };
        return new MyGenericType<T> { Id = dummy.Create<T>() };
    }
}
```

There are overloads to the `FromFactory` method with up to ten generic arguments.

```cs
[AutoCustomization]
public sealed class MyGenericTypeCustomization : OpenGenericCustomizationBase
{
    protected override IEnumerable<Type> Types { get; } = [typeof(MyGenericType<,>), typeof(IMyGenericType<,>)];
    
    protected override object FromFactory<T1, T2>(IDummy dummy)
    {
        ...
    }
}
```

If you need more than ten generic arguments or if you have other needs in terms of open generics, I would recommend using `ICustomization` or `CustomizationBase`.

### GenericCollectionCustomizationBase
Base customization for generic collections. You typically shouldn't need to inherit this directly but it could happen if you're working with a complex collection. 

```cs
public abstract class ListCustomizationBase : GenericCollectionCustomizationBase
{
    protected override object Factory(IDummy dummy, Type type)
    {
        var genericType = type.GetGenericArguments().Single();
        var list = CreateEnumerable(dummy, genericType);
        return GetType().GetSingleMethod(x => x.Name == nameof(Convert) && !x.IsAbstract).MakeGenericMethod(genericType).Invoke(this, [list])!;
    }

    protected abstract object Convert<T>(IEnumerable<T> source);
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

### ArrayCustomizationBase
This one might not have many applications outside of `System.Array` but it's still kept open for use just in case.

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

### GenericStackCustomizationBase
Internally, this is just the `ListCustomizationBase`. Use when working with stack-like collections.

### IntegerCustomizationBase
This is used to generate generic integers. Here is how it is implemented for `Int16` for instance.

```cs
public sealed class Int16Customization : IntegerCustomizationBase<short>;
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

It's also important to note that you can even override default customizations provided by Dummies by using `[AutoCustomization]`.

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

### The `WithoutCustomizations` method
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

### `With`, `Omit` and `Without` methods
These methods can be used to tell the builder what to do with each property or field. They are chainable and can be used multiple times. They can also be used to specify a value for a property or field that would otherwise be generated automatically.

```cs
public class Person
{
	public string Name { get; set; } = "Default";
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

var person = dummy.Build<Person>()
	.Omit(x => x.Name)
	.With(x => x.Age, 42)
	.Create();

//person.Name == "Default"
```

### `Omit` vs `Without`
They can be the same thing in many cases but essentially, `Omit` means that the property or field will not be set _at all_. In other words, if your property or field has a default value that you set manually at instantiation then it will keep this default value. It tells Dummy to just not do anything with it.

`Without`, on the other hand, sets that property or field to `default(T)` regardless of any value you set manually at instation. It is the equivalent of using `.With(x => x.PropertyOrField, null)`.

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
This is used to cast the `IDummyBuilder` to generic `IDummyBuilder<T>` type. This is generally recommended when you have a (non-generic) `IDummyBuilder` interface to work with which is often the case when working with customizations. It could also be used when you might need to change an interface to a concrete type.

```cs
var builder = customization.Build(_dummy, typeof(T)).As<T>();
```

### FromTypes
Use this if you want to create a random type implementing or inheriting a base type.

```cs
public interface IGarbage;
public sealed record GarbageOne : IGarbage;
public sealed record GarbageTwo : IGarbage;
public sealed record GarbageThree : IGarbage;

...

var result = Dummy.Build<IGarbage>().FromTypes(typeof(GarbageOne), typeof(GarbageTwo), typeof(GarbageThree)).CreateMany(10).ToList();
```

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
//Generates a number between 5 and 10
var result = Dummy.Number.Between(5, 10).Create();

//Geneates any number less than 100 (including negative numbers)
var result = Dummy.Number.LessThan(100).Create();

//Generates any number greater than or equal to 50
var result = Dummy.Number.GreaterThanOrEqualTo(50).Create();
```

## The `DateTimeDummyBuilder`
You can use the `Dummy.DateTime` property to have more control over how `DateTime` objects are generated such as `Before`, `After` and `Between`. Do keep in mind that using the `IDateTimeDummyBuilder` will bypass any customizations that are loaded for that type. To use `DateTime` customizations, you should use `Dummy.Create<DateTime>`. This builder will also attempt to return unique `DateTime` objects each time but it may give up on that if the range is very limited between two dates.

This builder also allows `DateTimeOffset` and `DateOnly` manipulations.

It also includes the concept of "Now" and "Today" as relative values. These are compatible with ToolBX.TimeProvider.

```cs
//Generates a DateOnly, DateTime or DateTimeOffset (depending on types used) between date1 and date2
var result = Dummy.Date.Between(date1, date2).Create();

//Generates a date set after the date passed
var result = Dummy.Date.After(date).Create();

//Generates a date set before the date passed
var result = Dummy.Date.Before(date).Create();

//Generates a DateTime set before "Now"
var result = Dummy.Date.BeforeNow().Create();

//Generates a DateTime set before "Today" (as in, "Today" is excluded from the potential results)
var result = Dummy.Date.BeforeToday().Create();

//Generates a DateTime set after "Now"
var result = Dummy.Date.AfterNow().Create();

//Generates a DateTime set after "Today" (as in, "Today" is excluded from the potential results)
var result = Dummy.Date.AfterToday().Create();
```

## The `DummyStringBuilder`
You can use the `Dummy.String` property to have more control over how `string` objects are generated such as `WithLength`. Do keep in mind that using the `DummyStringBuilder` will bypass any customizations that are loaded for that type. To use `string` customizations, you should use `Dummy.Create<string>`. This builder will also attempt to return unique `string` objects each time but it may give up on that if your instructions make possibilities very limited.

```cs
var result = dummy.String.WithLength.Exactly(10).Create();

var result = dummy.String.WithLength.LessThan(24).Create();

var result = dummy.String.WithLength.Between(75, 100).CreateMany();
```

## The `DummyEnumBuilder`
It can be used similarly to the `Exclude` method except that it's a one-time generator whereas `Exclude` is meant to be excluded for an entire builder's lifetime. Bear in mind that using the `Exclude` method and the `DummyEnumBuilder` together may yield unexpected results. 

```cs
//Without additional instructions, this is the equivalent of calling Dummy.Create<T>()
var result = dummy.Enum()<T>.Create();

//Will always only return either One or Three
var result = dummy.Enum()<T>.OneOf(SomeEnum.One, SomeEnum.Three).Create();

//Will always only return either One or Three
var result = dummy.Enum()<T>.Exclude(SomeEnum.Two).Create();
```