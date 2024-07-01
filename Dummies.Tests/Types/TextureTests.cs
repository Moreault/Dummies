namespace Dummies.Tests.Types;

[TestClass]
public sealed class TextureTests : Tester
{
    public interface ITexture
    {
        Size<int> Size { get; }
    }

    [TestMethod]
    public void Create_WhenCallingGetOnlyOnInterface_ShouldReturnDefault()
    {
        //Arrange
        var texture = Dummy.Create<ITexture>();

        //Act
        var result = texture.Size;

        //Assert
        result.Should().Be(default);
    }
}