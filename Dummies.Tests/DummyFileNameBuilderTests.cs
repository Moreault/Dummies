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
}