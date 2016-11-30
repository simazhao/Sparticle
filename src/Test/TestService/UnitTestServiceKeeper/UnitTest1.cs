using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnitTestServiceKeeper.ServiceKeeperReference;

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

        [TestMethod]
        public void TestGetAddressN_SingleUser()
        {
            string current = string.Empty, prev = null;
            var client = new ServiceKeeperReference.ServiceKeeperClient();

            var step = 20;

            for (int i=0;i<step*5;i++)
            {
                prev = current;

                try
                {
                    var address = client.GetServiceAddress("calc");

                    Assert.IsNotNull(address);

                    Assert.IsNotNull(address.Address);

                    current = address.Address;

                    if (i % step == 0)
                    {
                        Assert.AreNotEqual(current, prev);
                    }
                    else
                    {
                        Assert.AreEqual(current, prev);
                    }
                }
                catch(CommunicationException ex)
                {

                }
            }

            client.Close();
        }

        [TestMethod]
        public void TestGetAddressN_MultiUser()
        {
            const int howMany = 20;
            Task<ListAddressInfo>[] tasks = new Task<ListAddressInfo>[howMany]; 

            for (int i=0;i<howMany;++i)
            {
                tasks[i] = Task.Factory.StartNew<ListAddressInfo>(testGetAddressN_one, i.ToString());
            }

            var taskRegister = Task.Factory.StartNew(TestRegister_One, "15");

            Task.WaitAll(tasks);

            ListAddressInfo allresult = new ListAddressInfo();

            foreach(var r in tasks)
            {
                allresult.AddRange(r.Result);
            }

            var sb = new StringBuilder();
            foreach (var r in allresult.OrderBy(addr => { return addr.BeginTime; }))
            {
                sb.AppendFormat("#{0}# [{1}-{2}] => {3}\r\n",
                    r.No, r.BeginTime.ToString("yyyyMMdd-HHmmss.fff"), r.EndTime.ToString("yyyyMMdd-HHmmss.fff"), r.Message);
            }
            var msgToWrite = sb.ToString();

            var filepath = string.Format(@"D:\test\unittest_servicekeeper\{0}.txt",
                DateTime.Now.ToString("dd-HHmmss"));

            try
            {
                using (var writer = new StreamWriter(filepath, false))
                {
                    writer.Write(msgToWrite);
                }
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        class GetAddressInfo
        {
            public string No { get; set; }

            public DateTime BeginTime { get; set; }

            public DateTime EndTime { get; set; }

            public string Message { get; set; }
        }

        class ListAddressInfo : List<GetAddressInfo>
        {

        }

        private ListAddressInfo testGetAddressN_one(object no)
        {
            var client = new ServiceKeeperReference.ServiceKeeperClient();

            var step = 20;
            var ret = new ListAddressInfo();

            for (int i = 0; i < step * 2; i++)
            {
                var info = new GetAddressInfo();
                info.No = no.ToString();
                
                try
                {
                    info.BeginTime = DateTime.Now;

                    var address = client.GetServiceAddress("calc");

                    info.EndTime = DateTime.Now;
                    info.Message = address.Address;
                }
                catch (Exception ex)
                {
                    info.EndTime = DateTime.Now;
                    info.Message = "error";
                }

                ret.Add(info);
            }

            client.Close();

            return ret;
        }

        private void TestRegister_One(object no)
        {
            var client = new ServiceKeeperReference.ServiceRegisterClient();

            var address = string.Format("http://localhost/calc{0}.svc", no.ToString());

            var reg =
                new ServiceRegisteRequest() { ServiceIdentity = "calc", Address = new ServiceAddress() { Address = address }, }
                ;

            client.Register(reg);

            client.Close();
        }
    }
}
