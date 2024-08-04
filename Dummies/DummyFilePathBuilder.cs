using Path = System.IO.Path;

namespace ToolBX.Dummies;

public interface IDummyPathBuilder : ISpecializedBuilder<string>
{
    IDummyPathRootBuilder WithRoot { get; }
    IDummyPathDepthBuilder WithDepth { get; }
    IDummyPathSegmentLengthBuilder WithSegmentLength { get; }

    IDummyPathFileNameBuilder WithFileName { get; }

    /// <summary>
    /// Character used to separate the path. By default, <see cref="Path.AltDirectorySeparatorChar"/> is used.
    /// </summary>
    IDummyPathBuilder WithSeparator(char separator);
}

internal sealed class DummyPathBuilder : SpecializedBuilder<string>, IDummyPathBuilder
{
    private char _separator = Path.AltDirectorySeparatorChar;

    internal Func<string> Root = DummyPathRootBuilder.GenerateWindowsRoot;

    internal Func<int> Depth = () => 3;

    internal Func<int> SegmentLength = () => 4;

    internal Func<string> Filename = () => RandomStringGenerator.Generate(10, 24);

    internal Func<string> FileExtension = () => RandomStringGenerator.Generate(1, 3);

    public IDummyPathRootBuilder WithRoot => new DummyPathRootBuilder(this);
    public IDummyPathDepthBuilder WithDepth => new DummyPathDepthBuilder(this);
    public IDummyPathSegmentLengthBuilder WithSegmentLength => new DummyPathSegmentLengthBuilder(this);
    public IDummyPathFileNameBuilder WithFileName => new DummyPathFileNameBuilder(this);

    public DummyPathBuilder(IDummy dummy) : base(dummy)
    {
    }

    public IDummyPathBuilder WithSeparator(char separator)
    {
        _separator = separator;
        return this;
    }

    public override string Create()
    {
        var sb = new StringBuilder();

        sb.Append(Root());
        sb.Append(_separator);

        var depth = Depth();
        for (var i = 0; i < depth; i++)
        {
            sb.Append(RandomStringGenerator.Generate(SegmentLength()));
            sb.Append(_separator);
        }

        var filename = Filename();
        if (!string.IsNullOrWhiteSpace(filename))
        {
            sb.Append(filename);

            var extension = FileExtension();
            if (!string.IsNullOrWhiteSpace(extension))
                sb.Append($".{extension}");
        }

        return sb.ToString();
    }
}

public interface IDummyPathRootBuilder
{
    IDummyPathBuilder Always(string value);
    IDummyPathBuilder OneOf(params string[] value);
    IDummyPathBuilder Windows();
    IDummyPathBuilder Unix();
}

internal sealed class DummyPathRootBuilder : IDummyPathRootBuilder
{
    //a and b are typically reserved for floppy drives
    private const string WindowsDriveLetters = "cdefghijklmnopqrstuvwxyz";

    private readonly DummyPathBuilder _parent;

    public DummyPathRootBuilder(DummyPathBuilder parent)
    {
        _parent = parent;
    }

    internal static string GenerateWindowsRoot() => $"{WindowsDriveLetters.GetRandom()}:";

    public IDummyPathBuilder Always(string value)
    {
        _parent.Root = () => value;
        return _parent;
    }

    public IDummyPathBuilder OneOf(params string[] value)
    {
        _parent.Root = value.GetRandom;
        return _parent;
    }

    public IDummyPathBuilder Windows()
    {
        _parent.Root = GenerateWindowsRoot;
        return _parent;
    }

    public IDummyPathBuilder Unix() => Always("/mnt");
}

public interface IDummyPathDepthBuilder
{
    IDummyPathBuilder Exactly(int value);
    IDummyPathBuilder Between(int min, int max);
    IDummyPathBuilder LessThan(int value);
    IDummyPathBuilder LessThanOrEqualTo(int value);
}

internal sealed class DummyPathDepthBuilder : IDummyPathDepthBuilder
{
    private readonly DummyPathBuilder _parent;

    public DummyPathDepthBuilder(DummyPathBuilder parent)
    {
        _parent = parent;
    }

    public IDummyPathBuilder Exactly(int value)
    {
        _parent.Depth = () => value;
        return _parent;
    }

    public IDummyPathBuilder Between(int min, int max)
    {
        _parent.Depth = () => PseudoRandomNumberGenerator.Shared.Generate(min, max);
        return _parent;
    }

    public IDummyPathBuilder LessThan(int value) => Between(0, value - 1);

    public IDummyPathBuilder LessThanOrEqualTo(int value) => Between(0, value);
}

public interface IDummyPathSegmentLengthBuilder
{
    IDummyPathBuilder Exactly(int value);
    IDummyPathBuilder Between(int min, int max);
    IDummyPathBuilder LessThan(int value);
    IDummyPathBuilder LessThanOrEqualTo(int value);
}

internal sealed class DummyPathSegmentLengthBuilder : IDummyPathSegmentLengthBuilder
{
    private readonly DummyPathBuilder _parent;

    public DummyPathSegmentLengthBuilder(DummyPathBuilder parent)
    {
        _parent = parent;
    }

    public IDummyPathBuilder Exactly(int value)
    {
        _parent.SegmentLength = () => value;
        return _parent;
    }

    public IDummyPathBuilder Between(int min, int max)
    {
        _parent.SegmentLength = () => PseudoRandomNumberGenerator.Shared.Generate(min, max);
        return _parent;
    }

    public IDummyPathBuilder LessThan(int value) => Between(1, value - 1);

    public IDummyPathBuilder LessThanOrEqualTo(int value) => Between(1, value);
}


public interface IDummyPathFileNameBuilder
{
    IDummyPathFileNameLengthBuilder WithLength { get; }
    IDummyPathFileNameExtensionBuilder WithExtension { get; }
}

internal sealed class DummyPathFileNameBuilder : IDummyPathFileNameBuilder
{
    public IDummyPathFileNameLengthBuilder WithLength => new DummyPathFileNameLengthBuilder(_parent);
    public IDummyPathFileNameExtensionBuilder WithExtension => new DummyPathFileNameExtensionBuilder(_parent);

    private readonly DummyPathBuilder _parent;

    public DummyPathFileNameBuilder(DummyPathBuilder parent)
    {
        _parent = parent;
    }
}

public interface IDummyPathFileNameLengthBuilder
{
    IDummyPathBuilder Exactly(int value);
    IDummyPathBuilder Between(int min, int max);
    IDummyPathBuilder LessThan(int value);
    IDummyPathBuilder LessThanOrEqualTo(int value);
}

internal sealed class DummyPathFileNameLengthBuilder : IDummyPathFileNameLengthBuilder
{
    private readonly DummyPathBuilder _parent;

    public DummyPathFileNameLengthBuilder(DummyPathBuilder parent)
    {
        _parent = parent;
    }

    public IDummyPathBuilder Exactly(int value)
    {
        _parent.Filename = () => RandomStringGenerator.Generate(value);
        return _parent;
    }

    public IDummyPathBuilder Between(int min, int max)
    {
        _parent.Filename = () => RandomStringGenerator.Generate(min, max);
        return _parent;
    }

    public IDummyPathBuilder LessThan(int value) => Between(1, value - 1);

    public IDummyPathBuilder LessThanOrEqualTo(int value) => Between(1, value);
}

public interface IDummyPathFileNameExtensionBuilder
{
    IDummyPathBuilder Always(string value);
    IDummyPathBuilder OneOf(params string[] value);
    IDummyPathBuilder OneOf(IEnumerable<string> value);
    IDummyPathBuilder WithLength(int length);
    IDummyPathBuilder WithLengthBetween(int min, int max);
}

internal sealed class DummyPathFileNameExtensionBuilder : IDummyPathFileNameExtensionBuilder
{
    private readonly DummyPathBuilder _parent;

    public DummyPathFileNameExtensionBuilder(DummyPathBuilder parent)
    {
        _parent = parent;
    }

    public IDummyPathBuilder Always(string value)
    {
        _parent.FileExtension = () => value;
        return _parent;
    }

    public IDummyPathBuilder OneOf(params string[] value) => OneOf(value as IEnumerable<string>);

    public IDummyPathBuilder OneOf(IEnumerable<string> value)
    {
        _parent.FileExtension = value.GetRandom;
        return _parent;
    }

    public IDummyPathBuilder WithLength(int length)
    {
        _parent.FileExtension = () => RandomStringGenerator.Generate(length);
        return _parent;
    }

    public IDummyPathBuilder WithLengthBetween(int min, int max)
    {
        _parent.FileExtension = () => RandomStringGenerator.Generate(min, max);
        return _parent;
    }
}