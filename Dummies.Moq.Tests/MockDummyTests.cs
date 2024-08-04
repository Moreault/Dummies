using Moq;
using MoqDummy;

namespace Dummies.Moq.Tests;

[TestClass]
public class MockDummyTests
{
    public interface IService1
    {
        Guid Id { get; }

        void Run();
    }

    public class Garbage
    {
        private readonly IService1 _service1;

        private readonly string _name;

        public Garbage(IService1 service1, string name)
        {
            _service1 = service1;
            _name = name;
        }

        public void DoStuff()
        {
            _service1.Run();


        }
    }


    [TestMethod]
    public void TestMethod1()
    {
        var dummy = new MockDummy();
        var instance = dummy.Create<Garbage>();

        instance.DoStuff();
        
        dummy.GetMock<IService1>().Verify(x => x.Run(), Times.AtLeastOnce);
    }
}