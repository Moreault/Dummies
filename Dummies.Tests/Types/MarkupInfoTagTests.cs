namespace Dummies.Tests.Types;

[TestClass]
public sealed class MarkupInfoTagTests : Tester
{
    public record MarkupTagInfo
    {
        public required MarkupTag Tag { get; init; }
        public int StartIndex { get; init; }
        public int EndIndex { get; init; }

        public string Value => Tag.Value;
        public IReadOnlyList<MarkupParameter> Attributes => Tag.Attributes;
        public TagKind Kind => Tag.Kind;

        public bool IsClosing => Kind is TagKind.Closing or TagKind.SelfClosing or TagKind.Processing;

        public virtual bool Equals(MarkupTagInfo? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Tag, other.Tag) &&
                   StartIndex == other.StartIndex &&
                   EndIndex == other.EndIndex;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Tag, StartIndex, EndIndex);
        }
    }

    public record MarkupTag
    {
        public required string Name
        {
            get => _name;
            init
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                _name = value;
            }
        }
        private readonly string _name = null!;

        public string Value { get; init; } = string.Empty;
        public IReadOnlyList<MarkupParameter> Attributes { get; init; } = Array.Empty<MarkupParameter>();

        public required TagKind Kind { get; init; }

        public virtual bool Equals(MarkupTag? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return (string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(other.Name) || string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase)) &&
                   (string.IsNullOrWhiteSpace(Value) && string.IsNullOrWhiteSpace(other.Value) || string.Equals(Value, other.Value, StringComparison.InvariantCultureIgnoreCase)) &&
                   Attributes.SequenceEqual(other.Attributes) &&
                   Kind == other.Kind;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Value, Attributes, Kind);
        }

        public override string ToString()
        {
            var namePart = string.IsNullOrWhiteSpace(Value) ? Name : $"{Name}={Value}";
            return !Attributes.Any() ? namePart : $"{namePart} {string.Join(' ', Attributes)}";
        }
    }

    public record MarkupParameter
    {
        public required string Name
        {
            get => _name;
            init => _name = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(value)) : value;
        }
        private readonly string _name = null!;

        public string Value { get; init; } = string.Empty;

        public virtual bool Equals(MarkupParameter? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return (string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(other.Name) || string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase)) &&
                   (string.IsNullOrWhiteSpace(Value) && string.IsNullOrWhiteSpace(other.Value) || string.Equals(Value, other.Value, StringComparison.InvariantCultureIgnoreCase));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Value);
        }

        public override string ToString() => string.IsNullOrWhiteSpace(Value) ? Name : $"{Name}={Value}";
    }

    public enum TagKind
    {
        Opening,
        Closing,
        SelfClosing,
        Processing,
    }

    [TestMethod]
    public void Create_WhenMarkupTagInfo_DoNotThrow()
    {
        //Arrange

        //Act
        var action = () => Dummy.Create<MarkupTagInfo>();

        //Assert
        action.Should().NotThrow();
    }

    [TestMethod]
    public void Create_WhenTryingToSetReadOnlyProperty_Throw()
    {
        //Arrange

        //Act
        var action = () => Dummy.Build<MarkupTagInfo>().With(x => x.Kind, TagKind.Opening).Create();

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage(string.Format(ExceptionMessages.PropertyMustBeMutable, nameof(MarkupTagInfo.Kind), nameof(MarkupTagInfo)));
    }
}