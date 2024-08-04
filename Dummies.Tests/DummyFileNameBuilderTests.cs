namespace Dummies.Tests;

[TestClass]
public sealed class DummyFileNameBuilderTests : Tester
{
    [TestMethod]
    public void Create_WhenUsingDefaultSettings_CreateAValidRandomFileName()
    {
        //Arrange

        //Act
        var result = Dummy.FileName.Create();

        //Assert
        result.Should().MatchRegex(Regexes.Filename);
    }

    [TestMethod]
    public void Create_WithLengthExactly_CreateStringOfExactLength()
    {
        //Arrange
        var length = Dummy.Number.Between(10, 20).Create();

        //Act
        var result = Dummy.FileName.WithLength.Exactly(length).CreateMany();

        //Assert
        result.Select(Path.GetFileNameWithoutExtension).Should().OnlyContain(x => x.Length == length);
    }

    [TestMethod]
    [DataRow(10, 20)]
    [DataRow(15, 22)]
    [DataRow(22, 28)]
    public void Create_WithLengthBetween_CreateStringBetweenLengths(int min, int max)
    {
        //Arrange

        //Act
        var result = Dummy.FileName.WithLength.Between(min, max).CreateMany();

        //Assert
        result.Select(Path.GetFileNameWithoutExtension).Should().OnlyContain(x => x.Length >= min && x.Length <= max);
    }

    [TestMethod]
    [DataRow(10)]
    [DataRow(15)]
    [DataRow(22)]
    public void Create_WithLengthLessThan_CreateStringLessThanLength(int length)
    {
        //Arrange

        //Act
        var result = Dummy.FileName.WithLength.LessThan(length).CreateMany(25);

        //Assert
        result.Select(Path.GetFileNameWithoutExtension).Should().OnlyContain(x => x.Length >= 1 && x.Length < length);
    }

    [TestMethod]
    [DataRow(10)]
    [DataRow(15)]
    [DataRow(22)]
    public void Create_WithLengthLessThanOrEqualTo_CreateStringLessThanLength(int length)
    {
        //Arrange

        //Act
        var result = Dummy.FileName.WithLength.LessThanOrEqualTo(length).CreateMany(25);

        //Assert
        result.Select(Path.GetFileNameWithoutExtension).Should().OnlyContain(x => x.Length >= 1 && x.Length <= length);
    }

    [TestMethod]
    [DataRow("mp3")]
    [DataRow("ogg")]
    [DataRow("flac")]
    [DataRow("png")]
    public void Create_WithExtensionAlways_CreateFilenamesWithAllTheSameExtension(string extension)
    {
        //Arrange

        //Act
        var result = Dummy.FileName.WithExtension.Always(extension).CreateMany().ToList();

        //Assert
        result.Distinct().Should().HaveCount(result.Count);
        result.Select(x => Path.GetExtension(x).TrimStart('.')).Should().OnlyContain(x => x == extension);
    }

    [TestMethod]
    public void Create_WithExtensionOneOf_CreateFilenamesWithAllTheSameExtension()
    {
        //Arrange
        var extensions = new[] { "mp3", "bmp", "png", "docx" };

        //Act
        var result = Dummy.FileName.WithExtension.OneOf(extensions).CreateMany(50).ToList();

        //Assert
        result.Distinct().Should().HaveCount(result.Count);
        result.Select(x => Path.GetExtension(x).TrimStart('.')).Distinct().Should().BeEquivalentTo(extensions);
    }

    [TestMethod]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(4)]
    [DataRow(5)]
    public void Create_WithExtensionOfLength_CreateFileNameWithSameExtensionLengths(int length)
    {
        //Arrange

        //Act
        var result = Dummy.FileName.WithExtension.WithLength(length).CreateMany(12);

        //Assert
        result.Select(x => Path.GetExtension(x).TrimStart('.')).Should().OnlyContain(x => x.Length == length);
    }

    [TestMethod]
    [DataRow(2, 4)]
    [DataRow(3, 5)]
    [DataRow(4, 8)]
    [DataRow(5, 10)]
    public void Create_WithExtensionOfLengthBetween_CreateFileNameWithSameExtensionLengths(int min, int max)
    {
        //Arrange

        //Act
        var result = Dummy.FileName.WithExtension.WithLengthBetween(min, max).CreateMany(25);

        //Assert
        result.Select(x => Path.GetExtension(x).TrimStart('.')).Should().OnlyContain(x => x.Length >= min && x.Length <= max);
    }
}