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

    [TestCleanup]
    public void TestCleanupBase()
    {
        ((GlobalDummyOptions)DummyOptions.Global).Reset();
    }

    protected void TestInitialize()
    {

    }
}