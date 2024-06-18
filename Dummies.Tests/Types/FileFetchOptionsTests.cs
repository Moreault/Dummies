namespace Dummies.Tests.Types;

[TestClass]
public sealed class FileFetchOptionsTests : Tester
{
    //Taken from ToolBX.FileGuy
    public sealed record FileFetchOptions
    {
        public IReadOnlyList<string> FileExtensions
        {
            get => _fileExtensions;
            init => _fileExtensions = value?.ToImmutableList() ?? ImmutableList<string>.Empty;
        }
        private readonly IReadOnlyList<string> _fileExtensions = Array.Empty<string>();

        public SearchOption SearchKind { get; init; } = SearchOption.TopDirectoryOnly;

        public UriKind UriKind { get; init; } = UriKind.Absolute;

        public bool Equals(FileFetchOptions? other) => other != null && FileExtensions.SequenceEqual(other.FileExtensions) && SearchKind == other.SearchKind && UriKind == other.UriKind;

        public override int GetHashCode()
        {
            var hashCode = new HashCode();

            foreach (var extension in FileExtensions)
            {
                hashCode.Add(extension, StringComparer.OrdinalIgnoreCase);
            }

            hashCode.Add(SearchKind);
            hashCode.Add(UriKind);

            return hashCode.ToHashCode();
        }
    }

    [TestMethod]
    public void Create_WhenUsingBuild_ShouldCreate()
    {
        //Arrange

        //Act
        var options = Dummy.Build<FileFetchOptions>().With(x => x.FileExtensions, ImmutableList<string>.Empty).Create();

        //Assert
    }
}