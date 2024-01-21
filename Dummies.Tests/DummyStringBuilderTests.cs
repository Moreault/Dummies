namespace Dummies.Tests;

[TestClass]
public sealed class DummyStringBuilderTests : Tester
{
    [TestMethod]
    [DataRow(2)]
    [DataRow(12)]
    [DataRow(24)]
    [DataRow(48)]
    public void WhenExactStringLength_ReturnStringOfExactLength(int length)
    {
        //Arrange

        //Act
        var result = Dummy.String.WithLength.Exactly(length).CreateMany(25);

        //Assert
        result.Should().OnlyContain(x => x.Length == length);
    }

}