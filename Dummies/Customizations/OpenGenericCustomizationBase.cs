namespace ToolBX.Dummies.Customizations;

public abstract class OpenGenericCustomizationBase : ICustomization
{
    public Func<Type, bool> Condition => type => Types.Contains(type);

    protected abstract IEnumerable<Type> Types { get; }

    public IDummyBuilder Build(IDummy dummy, Type type)
    {
        if (dummy is null) throw new ArgumentNullException(nameof(dummy));
        if (type is null) throw new ArgumentNullException(nameof(type));

        var genericTypes = type.GetGenericArguments().ToArray();
        if (genericTypes.Length == 0) throw new NotSupportedException($"{nameof(OpenGenericCustomizationBase)} does not support non-generic object customization");

        var method = GetType().GetAllMethods(x => x.Name == nameof(FromFactory) && x.GetGenericArguments().Length == genericTypes.Length).Last().MakeGenericMethod(genericTypes);

        return dummy.Build<object>().FromFactory(() => method.Invoke(this, [dummy])!);
    }

    protected virtual object FromFactory<T>(IDummy dummy) => throw new NotImplementedException();
    protected virtual object FromFactory<T1, T2>(IDummy dummy) => throw new NotImplementedException();
    protected virtual object FromFactory<T1, T2, T3>(IDummy dummy) => throw new NotImplementedException();
    protected virtual object FromFactory<T1, T2, T3, T4>(IDummy dummy) => throw new NotImplementedException();
    protected virtual object FromFactory<T1, T2, T3, T4, T5>(IDummy dummy) => throw new NotImplementedException();
    protected virtual object FromFactory<T1, T2, T3, T4, T5, T6>(IDummy dummy) => throw new NotImplementedException();
    protected virtual object FromFactory<T1, T2, T3, T4, T5, T6, T7>(IDummy dummy) => throw new NotImplementedException();
    protected virtual object FromFactory<T1, T2, T3, T4, T5, T6, T7, T8>(IDummy dummy) => throw new NotImplementedException();
    protected virtual object FromFactory<T1, T2, T3, T4, T5, T6, T7, T8, T9>(IDummy dummy) => throw new NotImplementedException();
    protected virtual object FromFactory<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(IDummy dummy) => throw new NotImplementedException();
}