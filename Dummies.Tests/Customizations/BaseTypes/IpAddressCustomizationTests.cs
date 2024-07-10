using System.Net;

namespace Dummies.Tests.Customizations.BaseTypes;

[TestClass]
public sealed class IpAddressCustomizationTests : CustomizationTester<IpAddressCustomization>
{
    [TestMethod]
    public void WhenCreate_CreatePositiveIntGreaterThanOne()
    {
        //Arrange

        //Act
        var result = Dummy.Create<IPAddress>();

        //Assert
        result.Should().NotBeNull();
    }

    [TestMethod]
    public void WhenCreateMany_ReturnMany()
    {
        //Arrange

        //Act
        var result = Dummy.CreateMany<IPAddress>().ToList();

        //Assert
        result.Should().HaveCount(3);
    }
}