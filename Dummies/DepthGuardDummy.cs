namespace ToolBX.Dummies;

internal sealed class DepthGuardDummy : IDummy
{
    private readonly Dummy _dummy;
    internal ImmutableList<Type> TypeStack { get; }

    internal IReadOnlyList<ICustomization> Customizations => _dummy.Customizations;
    internal IReadOnlyDictionary<Type, List<object>> EnumExclusions => _dummy.EnumExclusions;

    public DummyOptions Options => _dummy.Options;
    public IDummyNumberBuilder Number => _dummy.Number;
    public IDummyDateTimeBuilder Date => _dummy.Date;
    public IDummyStringBuilder String => _dummy.String;
    public IDummyFileNameBuilder FileName => _dummy.FileName;
    public IDummyPathBuilder Path => _dummy.Path;

    internal DepthGuardDummy(Dummy dummy, ImmutableList<Type>? typeStack = null)
    {
        _dummy = dummy ?? throw new ArgumentNullException(nameof(dummy));
        TypeStack = typeStack ?? ImmutableList<Type>.Empty;
    }

    /// <summary>
    /// Returns a new guard with the given type pushed onto the type stack.
    /// </summary>
    internal DepthGuardDummy ForType(Type type) => new(_dummy, TypeStack.Add(type));

    /// <summary>
    /// Returns true if the number of occurrences of the given type in the current stack exceeds the maximum depth.
    /// </summary>
    internal bool IsRecursionLimitReached(Type type) => TypeStack.Count(t => t == type) > Options.MaximumDepth;

    public IDummyEnumBuilder<T> Enum<T>() where T : Enum => _dummy.Enum<T>();

    [RequiresUnreferencedCode("Creation of arbitrary types requires unreferenced code.")]
    [RequiresDynamicCode("Creation of arbitrary types may require runtime code generation.")]
    public T Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)] T>() => _dummy.Create<T>(TypeStack);

    [RequiresUnreferencedCode("Creation of arbitrary types requires unreferenced code.")]
    [RequiresDynamicCode("Creation of arbitrary types may require runtime code generation.")]
    public object Create([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)] Type type) => _dummy.Create(type, TypeStack);

    [RequiresUnreferencedCode("Creation of arbitrary types requires unreferenced code.")]
    [RequiresDynamicCode("Creation of arbitrary types may require runtime code generation.")]
    public IEnumerable<T> CreateMany<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)] T>() => _dummy.CreateMany<T>(_dummy.Options.DefaultCollectionSize, TypeStack);

    [RequiresUnreferencedCode("Creation of arbitrary types requires unreferenced code.")]
    [RequiresDynamicCode("Creation of arbitrary types may require runtime code generation.")]
    public IEnumerable<T> CreateMany<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)] T>(int amount) => _dummy.CreateMany<T>(amount, TypeStack);

    [RequiresUnreferencedCode("Creation of arbitrary types requires unreferenced code.")]
    [RequiresDynamicCode("Creation of arbitrary types may require runtime code generation.")]
    public IEnumerable<object> CreateMany([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)] Type type) => _dummy.CreateMany(type, _dummy.Options.DefaultCollectionSize, TypeStack);

    [RequiresUnreferencedCode("Creation of arbitrary types requires unreferenced code.")]
    [RequiresDynamicCode("Creation of arbitrary types may require runtime code generation.")]
    public IEnumerable<object> CreateMany([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)] Type type, int amount) => _dummy.CreateMany(type, amount, TypeStack);

    [RequiresUnreferencedCode("Creation of arbitrary types requires unreferenced code.")]
    [RequiresDynamicCode("Creation of arbitrary types may require runtime code generation.")]
    public IDummyBuilder<T> Build<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)] T>() => new DummyBuilder<T>(_dummy, TypeStack);

    public IDummy Customize(params ICustomization[] customizations) => _dummy.Customize(customizations);

    public IDummy Customize(IEnumerable<ICustomization> customizations) => _dummy.Customize(customizations);

    public IDummy Exclude<TEnum>(params TEnum[] values) where TEnum : Enum => _dummy.Exclude(values);

    public IDummy Exclude<TEnum>(IEnumerable<TEnum> values) where TEnum : Enum => _dummy.Exclude(values);

    public void Register<T>(T? instance) => _dummy.Register(instance);

    [RequiresUnreferencedCode("Creation of arbitrary types requires unreferenced code.")]
    [RequiresDynamicCode("Creation of arbitrary types may require runtime code generation.")]
    public T Freeze<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)] T>() => _dummy.Freeze<T>();

    [RequiresUnreferencedCode("Creation of arbitrary types requires unreferenced code.")]
    [RequiresDynamicCode("Creation of arbitrary types may require runtime code generation.")]
    public IEnumerable<T> CreateDistinct<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)] T>(int amount) => _dummy.CreateDistinct<T>(amount);
}
