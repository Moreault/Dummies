namespace Dummies.Tests;

[TestClass]
public sealed class DummyPathBuilderTests : Tester
{
    [TestMethod]
    public void Create_WhenUsingWindowsRoot_PathShouldHaveWindowsRoot()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithRoot.Windows().Create();

        //Assert
        result[..3].Should().MatchRegex(Regexes.WindowsRoot);
    }

    [TestMethod]
    public void Create_WhenUsingUnixRoot_PathShouldHaveUnixRoot()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithRoot.Unix().Create();

        //Assert
        result[..4].Should().Be("/mnt");
    }

    [TestMethod]
    public void Create_WhenUsingExactRoot_AlwaysReturnThatRoot()
    {
        //Arrange
        var root = Dummy.Create<string>();

        //Act
        var result = Dummy.Path.WithRoot.Always(root).CreateMany().ToList();

        //Assert
        result.Should().OnlyContain(x => x.StartsWith(root));
    }

    [TestMethod]
    public void Create_WhenUsingOneOf_AlwaysReturnOneOfTheOptionsOnly()
    {
        //Arrange
        string[] options = ["c:", "d:", "/mnt"];

        //Act
        var result = Dummy.Path.WithRoot.OneOf(options).CreateMany().ToList();

        //Assert
        result.Should().OnlyContain(x => x.StartsWith("c:/") || x.StartsWith("d:/") || x.StartsWith("/mnt"));
    }

    [TestMethod]
    public void Create_WhenUsingDepthZero_ShouldOnlyBeFileAtRoot()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithDepth.Exactly(0).Create();

        //Assert
        result.Remove(0, 3).Should().MatchRegex(Regexes.Filename);
    }

    [TestMethod]
    public void Create_WhenUsingDepthOne_ShouldOnlyBeOneFolderBetweenRootAndFile()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithDepth.Exactly(1).Create();

        //Assert
        result.Remove(0, 3).Split("/")[1].Should().MatchRegex(Regexes.Filename);
    }

    [TestMethod]
    public void Create_WhenUsingBetween_HaveVariableNumberOfDirectories()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithDepth.Between(0, 3).CreateMany(10);

        //Assert
        var rootless = result.Select(x => x.Remove(0, 3)).ToArray();
        foreach (var path in rootless)
            path.Split("/").Length.Should().BeInRange(0, 3);
    }

    [TestMethod]
    public void Create_WhenUsingLessThan_HaveVariableNumberOfDirectories()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithDepth.LessThan(3).CreateMany(10);

        //Assert
        var rootless = result.Select(x => x.Remove(0, 3)).ToArray();
        foreach (var path in rootless)
            path.Split("/").Length.Should().BeInRange(0, 2);
    }

    [TestMethod]
    public void Create_WhenUsingLessThanOrEqualTo_HaveVariableNumberOfDirectories()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithDepth.LessThanOrEqualTo(3).CreateMany(10);

        //Assert
        var rootless = result.Select(x => x.Remove(0, 3)).ToArray();
        foreach (var path in rootless)
            path.Split("/").Length.Should().BeInRange(0, 3);
    }

    [TestMethod]
    public void Create_WhenUsingACustomSeparator_UseOnlyThisSeparator()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithDepth.Exactly(3).WithSeparator('|').Create();

        //Assert
        result.Count(x => x == '|').Should().Be(4);
        result.Should().NotContain(Path.DirectorySeparatorChar.ToString());
        result.Should().NotContain(Path.AltDirectorySeparatorChar.ToString());
    }

    [TestMethod]
    public void Create_WhenUsingFileNameExactLength_AlwaysReturnSameFileName()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithFileName.WithLength.Exactly(10).CreateMany(5);

        //Assert
        result.Select(x => Path.GetFileNameWithoutExtension(x).Length).Should().OnlyContain(x => x == 10);
    }

    [TestMethod]
    public void Create_WhenUsingFileNameLengthBetween_AllFileNamesAreBetweenMinAndMax()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithFileName.WithLength.Between(9, 15).CreateMany(5);

        //Assert
        result.Select(x => Path.GetFileNameWithoutExtension(x).Length).Should().OnlyContain(x => x >= 9 && x <= 15);
    }

    [TestMethod]
    public void Create_WhenUsingFileNameLessThan_AllFileNamesAreLessThanSpecifiedButNeverZero()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithFileName.WithLength.LessThan(9).CreateMany(10);

        //Assert
        result.Select(x => Path.GetFileNameWithoutExtension(x).Length).Should().OnlyContain(x => x > 0 && x < 9);
    }

    [TestMethod]
    public void Create_WhenUsingFileNameLessThanOrEqualTo_AllFileNamesAreLessThanSpecifiedButNeverZero()
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithFileName.WithLength.LessThanOrEqualTo(9).CreateMany(10).ToList();

        //Assert
        result.Distinct().Should().HaveCount(10);
        result.Select(x => Path.GetFileNameWithoutExtension(x).Length).Should().OnlyContain(x => x > 0 && x <= 9);
    }

    [TestMethod]
    [DataRow(5)]
    [DataRow(10)]
    [DataRow(15)]
    public void Create_WhenUsingSegmentLengthWithExactValue_AllSegmentsShouldHaveSameLength(int length)
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithSegmentLength.Exactly(length).WithDepth.Exactly(5).Create().Split(Path.AltDirectorySeparatorChar).ToList();

        //Assert
        result.PopFirst();
        result.PopLast();
        result.Should().OnlyContain(x => x.Length == length);
    }

    [TestMethod]
    [DataRow(5, 10)]
    [DataRow(10, 15)]
    [DataRow(15, 20)]
    public void Create_WhenUsingSegmentLengthWithValueBetween_AllSegmentsShouldHaveLengthBetweenMinAndMax(int min, int max)
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithSegmentLength.Between(min, max).WithDepth.Exactly(5).Create().Split(Path.AltDirectorySeparatorChar).ToList();

        //Assert
        result.PopFirst();
        result.PopLast();
        result.Should().OnlyContain(x => x.Length >= min && x.Length <= max);
    }

    [TestMethod]
    [DataRow(5)]
    [DataRow(10)]
    [DataRow(15)]
    public void Create_WhenUsingSegmentLengthWithLessThan_AllSegmentsShouldBeLessThan(int length)
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithSegmentLength.LessThan(length).WithDepth.Exactly(5).Create().Split(Path.AltDirectorySeparatorChar).ToList();

        //Assert
        result.PopFirst();
        result.PopLast();
        result.Should().OnlyContain(x => x.Length >= 1 && x.Length < length);
    }

    [TestMethod]
    [DataRow(5)]
    [DataRow(10)]
    [DataRow(15)]
    public void Create_WhenUsingSegmentLengthWithLessThanOrEqualTo_AllSegmentsShouldBeLessThanOrEqualTo(int length)
    {
        //Arrange

        //Act
        var result = Dummy.Path.WithSegmentLength.LessThanOrEqualTo(length).WithDepth.Exactly(5).Create().Split(Path.AltDirectorySeparatorChar).ToList();

        //Assert
        result.PopFirst();
        result.PopLast();
        result.Should().OnlyContain(x => x.Length >= 1 && x.Length <= length);
    }
}