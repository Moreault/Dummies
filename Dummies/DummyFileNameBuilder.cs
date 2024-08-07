﻿using ArgumentNullException = System.ArgumentNullException;

namespace ToolBX.Dummies;

public interface IDummyFileNameBuilder : ISpecializedBuilder<string>
{
    IDummyFileNameLengthBuilder WithLength { get; }
    IDummyFileNameExtensionBuilder WithExtension { get; }
}

public interface IDummyFileNameLengthBuilder
{
    IDummyFileNameBuilder Exactly(int value);
    IDummyFileNameBuilder Between(int min, int max);
    IDummyFileNameBuilder LessThan(int value);
    IDummyFileNameBuilder LessThanOrEqualTo(int value);
}

public interface IDummyFileNameExtensionBuilder
{
    IDummyFileNameBuilder Always(string value);
    IDummyFileNameBuilder OneOf(params string[] value);
    IDummyFileNameBuilder WithLength(int length);
    IDummyFileNameBuilder WithLengthBetween(int min, int max);
}

internal sealed class DummyFileNameBuilder : SpecializedBuilder<string>, IDummyFileNameBuilder
{
    internal Func<int> Length = () => 21;

    internal Func<string> Extension = DummyFileNameExtensionBuilder.Default;

    public IDummyFileNameLengthBuilder WithLength => new DummyFileNameLengthBuilder(this);

    public IDummyFileNameExtensionBuilder WithExtension => new DummyFileNameExtensionBuilder(this);

    public DummyFileNameBuilder(IDummy dummy) : base(dummy)
    {
    }

    public override string Create() => $"{RandomStringGenerator.Generate(Length())}.{Extension().ToLowerInvariant()}";
}

internal sealed class DummyFileNameLengthBuilder : IDummyFileNameLengthBuilder
{
    private readonly DummyFileNameBuilder _builder;

    internal DummyFileNameLengthBuilder(DummyFileNameBuilder builder)
    {
        _builder = builder ?? throw new ArgumentNullException(nameof(builder));
    }

    public IDummyFileNameBuilder Exactly(int value)
    {
        _builder.Length = () => value;
        return _builder;
    }

    public IDummyFileNameBuilder Between(int min, int max)
    {
        _builder.Length = () => PseudoRandomNumberGenerator.Shared.Generate(min, max);
        return _builder;
    }

    public IDummyFileNameBuilder LessThan(int value) => Between(1, value - 1);

    public IDummyFileNameBuilder LessThanOrEqualTo(int value) => Between(1, value);
}

internal sealed class DummyFileNameExtensionBuilder : IDummyFileNameExtensionBuilder
{
    private readonly DummyFileNameBuilder _parent;

    internal static readonly Func<string> Default = () => RandomStringGenerator.Generate(1, 3);

    internal DummyFileNameExtensionBuilder(DummyFileNameBuilder parent)
    {
        _parent = parent ?? throw new ArgumentNullException(nameof(parent));
    }

    public IDummyFileNameBuilder Always(string value) => OneOf(value);

    public IDummyFileNameBuilder OneOf(params string[] value)
    {
        _parent.Extension = value.GetRandom;
        return _parent;
    }

    public IDummyFileNameBuilder WithLength(int length)
    {
        _parent.Extension = () => RandomStringGenerator.Generate(length);
        return _parent;
    }

    public IDummyFileNameBuilder WithLengthBetween(int min, int max)
    {
        _parent.Extension = () => RandomStringGenerator.Generate(min, max);
        return _parent;
    }
}