﻿namespace ToolBX.Dummies;

public interface IDummyBuilder
{
    IDummyBuilder<T> As<T>();
}

public interface IDummyBuilder<T> : IDummyBuilder
{
    /// <summary>
    /// Generates a single <see cref="T"/>.
    /// </summary>
    T Create();

    /// <summary>
    /// Generates multiple <see cref="T"/>. The amount is taken from <see cref="Dummy"/>>'s <see cref="Dummy.Options"/> property.
    /// </summary>
    IEnumerable<T> CreateMany();

    /// <summary>
    /// Generates a specific amount of <see cref="T"/>.
    /// </summary>
    IEnumerable<T> CreateMany(int amount);

    /// <summary>
    /// Generates a field or property using a value.
    /// </summary>
    IDummyBuilder<T> With<TMember>(Expression<Func<T, TMember>> member, TMember value);

    /// <summary>
    /// Generates a field or property using an expression which will be reevaluated every time the object is generated.
    /// </summary>
    IDummyBuilder<T> With<TMember>(Expression<Func<T, TMember>> member, Func<TMember> value);

    /// <summary>
    /// Generates a field or property using a value from the parent <see cref="Dummy"/>.
    /// </summary>
    IDummyBuilder<T> With<TMember>(Expression<Func<T, TMember>> member, Func<IDummy, TMember> value);

    /// <summary>
    /// Generates a field or property using an expression using the parent <see cref="Dummy"/> which will be reevaluated every time the object is generated.
    /// </summary>
    IDummyBuilder<T> With<TMember>(Expression<Func<T, TMember>> member, Func<IDummy, Func<TMember>> value);

    /// <summary>
    /// Sets field or property to default value.
    /// </summary>
    IDummyBuilder<T> Without<TMember>(Expression<Func<T, TMember>> member);

    /// <summary>
    /// Omit from automatic generation.
    /// </summary>
    IDummyBuilder<T> Omit<TMember>(Expression<Func<T, TMember>> member);

    /// <summary>
    /// Ignores any customization that would otherwise be automatically applied.
    /// </summary>
    IDummyBuilder<T> WithoutCustomizations();

    /// <summary>
    /// All properties with a public setter (or init) will be set to default.
    /// This is equivalent to calling <see cref="Without{TMember}"/> on every single property or field.
    /// </summary>
    IDummyBuilder<T> WithoutAutoProperties();

    /// <summary>
    /// All properties with a public setter (or init) and public fields will not be set automatically if left unspecified.
    /// This is equivalent to calling <see cref="Omit{TMember}"/> on every single property or field.
    /// </summary>
    IDummyBuilder<T> OmitAutoProperties();

    /// <summary>
    /// Specifies how to create the object. Use this when <see cref="Dummy"/> can't create an object on its own.
    /// </summary>
    IDummyBuilder<T> FromFactory(Func<T> factory, FactoryOptions? options = null);

    /// <summary>
    /// Will create a random object from one of the given types.
    /// </summary>
    IDummyBuilder<T> FromTypes(IEnumerable<Type> types);

    /// <summary>
    /// Will create a random object from one of the given types.
    /// </summary>
    IDummyBuilder<T> FromTypes(params Type[] types);

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
    private bool _omitAutoProperties;

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
        ThrowIfMemberIsReadOnly(memberExpression.Member.Name);
        _memberValues.Add(new MemberValuePair(memberExpression.Member, value));
        return this;
    }

    public IDummyBuilder<T> With<TMember>(Expression<Func<T, TMember>> member, Func<TMember> value)
    {
        if (member is null) throw new ArgumentNullException(nameof(member));
        if (value is null) throw new ArgumentNullException(nameof(value));
        var memberExpression = GetMemberExpression(member.Body);
        ThrowIfMemberIsReadOnly(memberExpression.Member.Name);
        _memberValues.Add(new MemberValuePair(memberExpression.Member, value));
        return this;
    }

    public IDummyBuilder<T> With<TMember>(Expression<Func<T, TMember>> member, Func<IDummy, TMember> value)
    {
        if (member is null) throw new ArgumentNullException(nameof(member));
        if (value is null) throw new ArgumentNullException(nameof(value));
        return With(member, value.Invoke(_dummy));
    }

    public IDummyBuilder<T> With<TMember>(Expression<Func<T, TMember>> member, Func<IDummy, Func<TMember>> value)
    {
        if (member is null) throw new ArgumentNullException(nameof(member));
        if (value is null) throw new ArgumentNullException(nameof(value));
        return With(member, value.Invoke(_dummy));
    }

    public IDummyBuilder<T> Without<TMember>(Expression<Func<T, TMember>> member)
    {
        if (member is null) throw new ArgumentNullException(nameof(member));
        var memberExpression = GetMemberExpression(member.Body);
        ThrowIfMemberIsReadOnly(memberExpression.Member.Name);
        _memberValues.Add(new MemberValuePair(memberExpression.Member, null));
        return this;
    }

    public IDummyBuilder<T> Omit<TMember>(Expression<Func<T, TMember>> member)
    {
        if (member is null) throw new ArgumentNullException(nameof(member));
        var memberExpression = GetMemberExpression(member.Body);
        ThrowIfMemberIsReadOnly(memberExpression.Member.Name);
        _memberValues.Add(new MemberValuePair(memberExpression.Member, MemberValuePair.Omit.Instance));
        return this;
    }

    private void ThrowIfMemberIsReadOnly(string memberName)
    {
        var property = typeof(T).GetSinglePropertyOrDefault(memberName);
        if (property is not null)
        {
            if (property.SetMethod is null || !property.SetMethod.IsPublic)
                throw new InvalidOperationException(string.Format(ExceptionMessages.PropertyMustBeMutable, memberName, typeof(T).GetHumanReadableName()));
        }
        else
        {
            var field = typeof(T).GetSingleFieldOrDefault(memberName);
            if (field is null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.NoFieldOrPropertyWithName, memberName, typeof(T).GetHumanReadableName()));

            if (!field.IsPublic)
                throw new InvalidOperationException(string.Format(ExceptionMessages.FieldIsNotPublic, memberName, typeof(T).GetHumanReadableName()));
        }

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

    public IDummyBuilder<T> OmitAutoProperties()
    {
        _omitAutoProperties = true;
        return this;
    }

    public IDummyBuilder<T> FromFactory(Func<T> factory, FactoryOptions? options = null)
    {
        options ??= FactoryOptions.Default;

        _omitAutoProperties = options.OmitAutoProperties;

        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        return this;
    }

    public IDummyBuilder<T> FromTypes(IEnumerable<Type> types)
    {
        if (types is null) throw new ArgumentNullException(nameof(types));
        return FromFactory(() => (T)_dummy.Create(types.GetRandom()));
    }

    public IDummyBuilder<T> FromTypes(params Type[] types) => FromTypes(types as IEnumerable<Type>);

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
        casted._omitAutoProperties = _omitAutoProperties;
        casted._factory = _factory is not null ? () => (T2)(object)_factory()! : null;
        return casted;
    }

    public T Create() => CreateMany(1).Single();

    private ICustomization? FindCustomization<TCustomization>() => FindCustomization(typeof(TCustomization));

    private ICustomization? FindCustomization(Type type)
    {
        var customizations = AutoCustomizations.Concat(_dummy.Customizations).ToArray();
        return customizations.LastOrDefault(x => x.Condition(type)) ??
               (type.IsGenericType ? customizations.LastOrDefault(x => x.Condition(type.GetGenericTypeDefinition())) : null);
    }

    public IEnumerable<T> CreateMany() => CreateMany(_dummy.Options.DefaultCollectionSize);

    public IEnumerable<T> CreateMany(int amount)
    {
        if (amount <= 0) throw new ArgumentException(string.Format(ExceptionMessages.CannotCreateNegativeOrZeroObjects, amount));

        if (_usesCustomizations)
        {
            var customization = FindCustomization<T>();
            IDummyBuilder<T> customizationBuilder = null!;
            if (customization is not null)
                customizationBuilder = customization.Build(_dummy, typeof(T)).As<T>();

            if (customizationBuilder is DummyBuilder<T> concreteBuilder)
            {
                _withoutAutoProperties = concreteBuilder._withoutAutoProperties;
                _omitAutoProperties = concreteBuilder._omitAutoProperties;
                var originals = _memberValues.ToList();
                var customizations = concreteBuilder._memberValues.ToList();
                _memberValues.AddRange(customizations);
                _memberValues.AddRange(originals);

                if (concreteBuilder._factory is not null)
                    _factory = concreteBuilder._factory;
            }
        }

        var deeperDummy = _dummy.Deeper();

        var output = new List<T>();
        for (var i = 0; i < amount; i++)
        {
            //Needs to be boxed in case it's a struct so that modifications to its properties after instantiation are kept
            object? instance = default(T)!;
            if (_dummy.CurrentDepth <= _dummy.Options.MaximumDepth)
            {
                if (_factory is null)
                {
                    if (typeof(T).IsAbstract)
                    {
                        instance = DynamicObjectGenerator.From(typeof(T));
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
                            .OrderByDescending(x => x.IsPublic).ThenBy(x => x.GetParameters().Length);

                        var instantiation = TryInstantiate(_dummy, constructors);
                        if (!instantiation.IsSuccess)
                            throw new InstantiationException(typeof(T));

                        instance = instantiation.Value;
                    }
                }
                else
                {
                    instance = _factory();
                }

                if (!typeof(T).IsEnum)
                {
                    foreach (var property in typeof(T).GetAllProperties(x => x.IsInstance() &&
                                 x.IsPublic() && x.CanRead && x.SetMethod != null && x.SetMethod.IsPublic &&
                                 !x.IsIndexer()))
                    {
                        var memberValue = _memberValues.LastOrDefault(x => x.MemberInfo.Name == property.Name);
                        if (memberValue is null)
                        {
                            if (_withoutAutoProperties)
                                property.SetValue(instance, default);
                            else if (!_omitAutoProperties)
                                property.SetValue(instance, deeperDummy.Create(property.PropertyType));
                        }
                        else if (!Equals(memberValue.Value, MemberValuePair.Omit.Instance))
                            property.SetValue(instance, memberValue.Value);
                    }

                    foreach (var field in typeof(T).GetAllFields(x => x.IsPublic && x.IsInstance()))
                    {
                        var memberValue = _memberValues.LastOrDefault(x => x.MemberInfo.Name == field.Name);
                        if (memberValue is null)
                        {
                            if (_withoutAutoProperties)
                                field.SetValue(instance, default);
                            else if (!_omitAutoProperties)
                                field.SetValue(instance, deeperDummy.Create(field.FieldType));
                        }
                        else if (!Equals(memberValue.Value, MemberValuePair.Omit.Instance))
                            field.SetValue(instance, memberValue.Value);
                    }
                }
            }

            output.Add((T)instance!);
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