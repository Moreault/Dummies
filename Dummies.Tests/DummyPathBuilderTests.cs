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
}