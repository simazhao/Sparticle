using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestServiceKeeper
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {

        }

        [TestMethod]
        public void TestRegister()
        {
            var client = new ServiceKeeperReference.ServiceRegisterClient();

            foreach (var reg in PreparedData.registerRequests1)
            {
                client.Register(reg);
            }

            foreach (var reg in PreparedData.registerRequests2)
            {
                client.Register(reg);
            }

            client.Close();
        }

        [TestMethod]
        public void TestGetAddress()
        {
            var client = new ServiceKeeperReference.ServiceKeeperClient();

            var address = client.GetServiceAddress("calc");

            Assert.IsNotNull(address);

            Assert.IsNotNull(address.Address);

            client.Close();
        }
    }
}
