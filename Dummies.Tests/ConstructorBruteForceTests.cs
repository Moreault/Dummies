namespace Dummies.Tests;

[TestClass]
public sealed class ConstructorBruteForceTests : Tester
{
    public sealed class Garbage
    {
        public static IReadOnlyList<int> CallOrder => _callOrder;
        private static readonly List<int> _callOrder = new();

        public static void Reset() => _callOrder.Clear();

        //1
        public Garbage(string a)
        {
            _callOrder.Add(1);
            throw new Exception();
        }

        //2
        public Garbage(string a, char b)
        {
            _callOrder.Add(2);
            throw new Exception();
        }

        //3
        private Garbage()
        {
            _callOrder.Add(3);
            throw new Exception();
        }

        //4
        private Garbage(char a)
        {
            _callOrder.Add(4);
            throw new Exception();
        }
    }

    [TestMethod]
    public void Create_WhenTypeHasPublicAndPrivateConstructors_CallThemInExpectedOrder()
    {
        //Arrange
        Garbage.Reset();

        //Act
        try
        {
            Dummy.Create<Garbage>();
        }
        catch
        {
            //ignore
        }

        //Assert
        Garbage.CallOrder.Should().ContainInOrder([1, 2, 3, 4]);
    }
}