namespace ToolBX.Dummies;

internal readonly struct TypeArrayKey : IEquatable<TypeArrayKey>
{
    public Type[] Types { get; }
    private readonly int _hashCode;

    public TypeArrayKey(Type[] types)
    {
        Types = types;
        var hash = new HashCode();
        foreach (var t in types)
            hash.Add(t);
        _hashCode = hash.ToHashCode();
    }

    public bool Equals(TypeArrayKey other)
    {
        if (Types.Length != other.Types.Length) return false;
        for (var i = 0; i < Types.Length; i++)
            if (Types[i] != other.Types[i]) return false;
        return true;
    }

    public override bool Equals(object? obj) => obj is TypeArrayKey other && Equals(other);
    public override int GetHashCode() => _hashCode;
}
