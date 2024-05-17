using ToolBX.Mathemancy;

namespace Dummies.Tests.Types;

[TestClass]
public sealed class Vector3OfFloatTests : Vector3Tester<float>;

public abstract class Vector3Tester<T> : Tester where T : struct, INumber<T>
{
    [TestMethod]
    public void WhenCreateWithoutBuilder_AssignXYAndZ()
    {
        //Arrange

        //Act
        var result = Dummy.Create<Vector3<T>>();

        //Assert
        result.X.Should().NotBe(default);
        result.Y.Should().NotBe(default);
        result.Z.Should().NotBe(default);
    }
}