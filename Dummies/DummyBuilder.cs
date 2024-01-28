using System.Linq.Expressions;
using ToolBX.Dummies.Generation;

namespace ToolBX.Dummies;

public interface IDummyBuilder
{
    IDummyBuilder<T> As<T>();
}

public interface IDummyBuilder<T> : IDummyBuilder
{
    T Create();
    IEnumerable<T> CreateMany(int amount = 3);
    IDummyBuilder<T> With<TMember>(Expression<Func<T, TMember>> member, TMember value);
    IDummyBuilder<T> With<TMember>(Expression<Func<T, TMember>> member, Func<TMember> value);
    IDummyBuilder<T> Without<TMember>(Expression<Func<T, TMember>> member);

    /// <summary>
    /// Ignores any customization that would otherwise be automatically applied.
    /// </summary>
    IDummyBuilder<T> WithoutCustomizations();

    //TODO Make sure that properties are not automatically set (unless explicitly specified in the _memberValues) when using a factory.
    IDummyBuilder<T> FromFactory(Func<T> factory);

    /// <summary>
    /// Excludes the specified values from being generated for the specified enum type.
    /// </summary>
    IDummyBuilder<T> Exclude<TEnum>(params TEnum[] values) where TEnum : Enum;

    /// <summary>
    /// Excludes the specified values from being generated for the specified enum type.
    /// </summary>
    IDummyBuilder<T> Exclude<TEnum>(IEnumerable<TEnum> values) where TEnum : Enum;
}

internal sealed class DummyBuilder<T> : IDummyBuilder<T>
{
    private static ImmutableList<ICustomization> AutoCustomizations => _autoCustomizations.Value;
    // ReSharper disable once InconsistentNaming
    private static readonly Lazy<ImmutableList<ICustomization>> _autoCustomizations = new(() => Types.Where(x => x.HasAttribute<AutoCustomizationAttribute>() && !x.IsAbstract && x.Implements<ICustomization>()).Select(x => (ICustomization)Activator.CreateInstance(x)!).ToImmutableList());

    private readonly Dummy _dummy;

    private readonly List<MemberValuePair> _memberValues = new();

    private bool _usesCustomizations = true;

    private Func<T>? _factory;

    internal DummyBuilder(Dummy dummy)
    {
        _dummy = dummy ?? throw new ArgumentNullException(nameof(dummy));
    }

    public IDummyBuilder<T> With<TMember>(Expression<Func<T, TMember>> member, TMember? value)
    {
        if (member is null) throw new ArgumentNullException(nameof(member));
        var memberExpression = GetMemberExpression(member.Body);
        _memberValues.Add(new MemberValuePair(memberExpression.Member, value));
        return this;
    }

    public IDummyBuilder<T> With<TMember>(Expression<Func<T, TMember>> member, Func<TMember> value)
    {
        if (member is null) throw new ArgumentNullException(nameof(member));
        if (value is null) throw new ArgumentNullException(nameof(value));
        var memberExpression = GetMemberExpression(member.Body);
        _memberValues.Add(new MemberValuePair(memberExpression.Member, value));
        return this;
    }

    private MemberExpression GetMemberExpression(Expression body)
    {
        if (body is MemberExpression member)
        {
            return member;
        }
        if (body is UnaryExpression unaryExpression)
        {
            return (MemberExpression)unaryExpression.Operand;
        }
        //TODO Message
        throw new ArgumentException();
    }

    public IDummyBuilder<T> Without<TMember>(Expression<Func<T, TMember>> member) => With(member, default(TMember)!);

    public IDummyBuilder<T> WithoutCustomizations()
    {
        _usesCustomizations = false;
        return this;
    }

    public IDummyBuilder<T> FromFactory(Func<T> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        return this;
    }

    public IDummyBuilder<T> Exclude<TEnum>(params TEnum[] values) where TEnum : Enum => Exclude(values as IEnumerable<TEnum>);

    public IDummyBuilder<T> Exclude<TEnum>(IEnumerable<TEnum> values) where TEnum : Enum
    {
        _dummy.Exclude(values);
        return this;
    }

    public IDummyBuilder<T2> As<T2>()
    {
        var casted = new DummyBuilder<T2>(_dummy);
        casted._memberValues.AddRange(_memberValues);
        casted._usesCustomizations = _usesCustomizations;
        casted._factory = _factory is not null ? () => (T2)(object)_factory() : null;
        return casted;
    }

    public T Create() => CreateMany(1).Single();

    private ICustomization? FindCustomization<TCustomization>() => FindCustomization(typeof(TCustomization));

    private ICustomization? FindCustomization(Type type)
    {
        var customization = _dummy.Customizations.SingleOrDefault(x => x.Types.Contains(type)) ??
               AutoCustomizations.SingleOrDefault(x => x.Types.Contains(type)) ??
               (type.IsGenericType ? AutoCustomizations.SingleOrDefault(x => x.Types.Contains(type.GetGenericTypeDefinition())) : null);

        if (customization == null && type.IsArray)
            customization = AutoCustomizations.SingleOrDefault(x => x.Types.Any(t => t == typeof(Array)));

        return customization;
    }

    public IEnumerable<T> CreateMany(int amount = 3)
    {
        //TODO Actual message
        if (amount <= 0) throw new ArgumentException();

        var customization = FindCustomization<T>();
        IDummyBuilder<T> customizationBuilder = null!;
        if (customization is ICustomization<T> typedCustomization)
            customizationBuilder = typedCustomization.Build(_dummy);
        else if (customization is not null)
            customizationBuilder = customization.Build(_dummy, typeof(T)).As<T>();

        if (customizationBuilder is DummyBuilder<T> concreteBuilder)
        {
            var originals = _memberValues.ToList();
            var customizations = concreteBuilder._memberValues.ToList();
            _memberValues.AddRange(customizations);
            _memberValues.AddRange(originals);

            if (concreteBuilder._factory is not null)
                _factory = concreteBuilder._factory;
        }

        var output = new List<T>();
        for (var i = 0; i < amount; i++)
        {
            T instance;
            if (_factory is null)
            {
                if (typeof(T).IsAbstract)
                {
                    instance = DynamicObjectGenerator.From<T>();
                }
                else if (typeof(T).IsEnum)
                {
                    var hasExclusions = _dummy.EnumExclusions.TryGetValue(typeof(T), out var exclusions);
                    var possibleValues = hasExclusions ?
                        Enum.GetValues(typeof(T)).Cast<T>().Where(x => !exclusions!.Contains(x!)).ToArray() :
                        Enum.GetValues(typeof(T)).Cast<T>().ToArray();
                    instance = possibleValues.GetRandom();
                }
                else
                {
                    //TODO Support creating types that have constructors no matter how complex
                    var constructor = typeof(T).GetConstructors().Where(x => x.IsInstance()).OrderBy(x => x.GetParameters().Length).ThenBy(x => x.IsPublic).First();

                    instance = (T)constructor.Invoke(constructor.GetParameters().Select(x => _dummy.Create(x.ParameterType)).ToArray());
                }
            }
            else
            {
                instance = _factory();
            }

            foreach (var property in typeof(T).GetAllProperties(x => x.IsPublic() && x.IsGet() && x.SetMethod != null && x.SetMethod.IsPublic && !x.IsIndexer()))
            {
                var memberValue = _memberValues.LastOrDefault(x => x.MemberInfo == property);
                if (memberValue is null)
                    property.SetValue(instance, _dummy.Create(property.PropertyType));
                else if (_factory is null)
                    property.SetValue(instance, memberValue.Value);
            }

            foreach (var field in typeof(T).GetAllFields(x => x.IsPublic && x.IsInstance()))
            {
                var memberValue = _memberValues.LastOrDefault(x => x.MemberInfo == field);
                if (memberValue is null)
                    field.SetValue(instance, _dummy.Create(field.FieldType));
                else if (_factory is null)
                    field.SetValue(instance, memberValue.Value);
            }

            output.Add(instance);
        }

        return output;
    }
}