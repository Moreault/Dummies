namespace ToolBX.Dummies;

internal sealed record MemberValuePair
{
    public MemberInfo MemberInfo { get; init; }

    public object? Value
    {
        get
        {
            if (_value is null)
                return null;

            if (_value.Equals(Omit.Instance))
                return Omit.Instance;

            var realType = MemberInfo.GetMemberType();
            var valueType = _value.GetType();
            var value = _value;

            if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Func<>))
            {
                var method = valueType.GetMethod("Invoke")!;
                valueType = method.ReturnType;
                value = method.Invoke(_value, []);
            }

            if (realType.IsAssignableFrom(valueType))
                return value;

            return Convert.ChangeType(value, realType);
        }
        init => _value = value;
    }
    private readonly object? _value;

    internal readonly record struct Omit
    {
        internal static readonly Omit Instance = new();
    }

    public MemberValuePair(MemberInfo memberInfo, object? value)
    {
        MemberInfo = memberInfo;
        Value = value;
    }
}