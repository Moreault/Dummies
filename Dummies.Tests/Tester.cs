namespace Dummies.Tests;

public abstract class Tester
{
    protected Dummy Dummy { get; private set; } = null!;

    [TestInitialize]
    public void TestInitializeBase()
    {
        Dummy = new Dummy();
        TestInitialize();
    }

    protected void TestInitialize()
    {

    }
}