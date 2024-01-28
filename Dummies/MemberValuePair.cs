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

            var realType = MemberInfo.GetMemberType();
            var valueType = _value.GetType();
            var value = _value;

            if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Func<>))
            {
                var method = valueType.GetMethod("Invoke")!;
                valueType = method.ReturnType;
                value = method.Invoke(_value, []);
            }

            return realType == valueType ? value : Convert.ChangeType(value, realType);
        }
        init => _value = value;
    }
    private readonly object? _value;

    public MemberValuePair(MemberInfo MemberInfo, object? Value)
    {
        this.MemberInfo = MemberInfo;
        this.Value = Value;
    }

    public void Deconstruct(out MemberInfo MemberInfo, out object? Value)
    {
        MemberInfo = this.MemberInfo;
        Value = this.Value;
    }
}

internal static class MemberInfoExtensions
{
    //TODO Use method from R4H .GetMemberType()
    internal static Type GetMemberType(this MemberInfo memberInfo)
    {
        if (memberInfo == null) throw new ArgumentNullException(nameof(memberInfo));

        if (memberInfo is PropertyInfo propertyInfo)
            return propertyInfo.PropertyType;
        if (memberInfo is FieldInfo fieldInfo)
            return fieldInfo.FieldType;

        throw new NotSupportedException($"MemberInfo of type {memberInfo.GetType()} is not supported.");
    }
}