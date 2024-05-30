using System.Linq.Expressions;
using ToolBX.Dummies.Exceptions;
using ToolBX.Dummies.Generation;
using ToolBX.OutWarden;

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

    /// <summary>
    /// All properties with a public setter (or init) and public fields will not be set automatically if left unspecified.
    /// This is equivalent to calling <see cref="Without{TMember}"/> on every single property or field.
    /// </summary>
    IDummyBuilder<T> WithoutAutoProperties();

    /// <summary>
    /// Specifies how to create the object. Use this when <see cref="Dummy"/> can't create an object on its own.
    /// </summary>
    IDummyBuilder<T> FromFactory(Func<T> factory, FactoryOptions? options = null);

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

    private readonly DepthGuardDummy _dummy;

    private readonly List<MemberValuePair> _memberValues = [];

    private bool _usesCustomizations = true;

    private Func<T>? _factory;

    private bool _withoutAutoProperties;

    internal DummyBuilder(Dummy dummy, int currentDepth = 0)
    {
        _dummy = new DepthGuardDummy(dummy, currentDepth) ?? throw new ArgumentNullException(nameof(dummy));
    }

    internal DummyBuilder(DepthGuardDummy dummy)
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

    public IDummyBuilder<T> Without<TMember>(Expression<Func<T, TMember>> member)
    {
        if (member is null) throw new ArgumentNullException(nameof(member));
        var memberExpression = GetMemberExpression(member.Body);
        _memberValues.Add(new MemberValuePair(memberExpression.Member, null));
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
        throw new ArgumentException(ExceptionMessages.MemberExpressionUnsupported);
    }

    public IDummyBuilder<T> WithoutCustomizations()
    {
        _usesCustomizations = false;
        return this;
    }

    public IDummyBuilder<T> WithoutAutoProperties()
    {
        _withoutAutoProperties = true;
        return this;
    }

    public IDummyBuilder<T> FromFactory(Func<T> factory, FactoryOptions? options = null)
    {
        options ??= FactoryOptions.Default;
        if (!options.UseAutoProperties)
            WithoutAutoProperties();

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
        casted._withoutAutoProperties = _withoutAutoProperties;
        casted._factory = _factory is not null ? () => (T2)(object)_factory() : null;
        return casted;
    }

    public T Create() => CreateMany(1).Single();

    private ICustomization? FindCustomization<TCustomization>() => FindCustomization(typeof(TCustomization));

    private ICustomization? FindCustomization(Type type)
    {
        return _dummy.Customizations.SingleOrDefault(x => x.Condition(type)) ??
               AutoCustomizations.SingleOrDefault(x => x.Condition(type)) ??
               (type.IsGenericType ? AutoCustomizations.SingleOrDefault(x => x.Condition(type.GetGenericTypeDefinition())) : null);
    }

    public IEnumerable<T> CreateMany(int amount = 3)
    {
        if (amount <= 0) throw new ArgumentException(string.Format(ExceptionMessages.CannotCreateNegativeOrZeroObjects, amount));

        var customization = FindCustomization<T>();
        IDummyBuilder<T> customizationBuilder = null!;
        if (customization is not null)
            customizationBuilder = customization.Build(_dummy, typeof(T)).As<T>();

        if (customizationBuilder is DummyBuilder<T> concreteBuilder)
        {
            _withoutAutoProperties = concreteBuilder._withoutAutoProperties;
            var originals = _memberValues.ToList();
            var customizations = concreteBuilder._memberValues.ToList();
            _memberValues.AddRange(customizations);
            _memberValues.AddRange(originals);

            if (concreteBuilder._factory is not null)
                _factory = concreteBuilder._factory;
        }

        var deeperDummy = _dummy.Deeper();

        var output = new List<T>();
        for (var i = 0; i < amount; i++)
        {
            T instance = default!;
            if (_dummy.CurrentDepth < _dummy.Options.MaximumDepth)
            {
                if (_factory is null)
                {
                    if (typeof(T).IsAbstract)
                    {
                        instance = DynamicObjectGenerator.From<T>();
                    }
                    else if (typeof(T).IsEnum)
                    {
                        var hasExclusions = _dummy.EnumExclusions.TryGetValue(typeof(T), out var exclusions);
                        var possibleValues = hasExclusions
                            ? Enum.GetValues(typeof(T)).Cast<T>().Where(x => !exclusions!.Contains(x!)).ToArray()
                            : Enum.GetValues(typeof(T)).Cast<T>().ToArray();
                        instance = possibleValues.GetRandom();
                    }
                    else
                    {
                        var constructors = typeof(T).GetAllConstructors().Where(x => x.IsInstance())
                            .OrderByDescending(x => x.GetParameters().Length).ThenBy(x => x.IsPublic);

                        var instantiation = TryInstantiate(deeperDummy, constructors);
                        if (!instantiation.IsSuccess)
                            throw new InstantiationException(typeof(T));

                        instance = instantiation.Value;
                    }
                }
                else
                {
                    instance = _factory();
                }

                foreach (var property in typeof(T).GetAllProperties(x =>
                             x.IsPublic() && x.IsGet() && x.SetMethod != null && x.SetMethod.IsPublic &&
                             !x.IsIndexer()))
                {
                    var memberValue = _memberValues.LastOrDefault(x => x.MemberInfo == property);
                    if (memberValue is null)
                    {
                        if (!_withoutAutoProperties)
                            property.SetValue(instance, deeperDummy.Create(property.PropertyType));
                    }
                    else if (memberValue.Value is not null)
                        property.SetValue(instance, memberValue.Value);
                }

                foreach (var field in typeof(T).GetAllFields(x => x.IsPublic && x.IsInstance()))
                {
                    var memberValue = _memberValues.LastOrDefault(x => x.MemberInfo == field);
                    if (memberValue is null)
                    {
                        if (!_withoutAutoProperties)
                            field.SetValue(instance, deeperDummy.Create(field.FieldType));
                    }
                    else if (memberValue.Value is not null)
                        field.SetValue(instance, memberValue.Value);
                }
            }

            output.Add(instance);
        }

        return output;
    }

    private static Result<T> TryInstantiate(IDummy dummy, IEnumerable<ConstructorInfo> constructors)
    {
        foreach (var constructor in constructors)
        {
            var instantiation = TryInstantiate(dummy, constructor);
            if (instantiation.IsSuccess)
            {
                return instantiation;
            }
        }
        return Result<T>.Failure();
    }

    private static Result<T> TryInstantiate(IDummy dummy, ConstructorInfo constructor)
    {
        try
        {
            return Result<T>.Success((T)constructor.Invoke(constructor.GetParameters().Select(x => dummy.Create(x.ParameterType)).ToArray()));
        }
        catch
        {
            return Result<T>.Failure();
        }
    }
}