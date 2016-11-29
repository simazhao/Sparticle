using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestServiceKeeper.ServiceKeeperReference;

namespace UnitTestServiceKeeper
{
    static class PreparedData
    {
        internal static ServiceRegisteRequest[] registerRequests1 = new ServiceRegisteRequest[]
            {
                new ServiceRegisteRequest() {ServiceIdentity="calc", Address=new ServiceAddress() {Address = "http://localhost/calc1.svc" }, },
                new ServiceRegisteRequest() {ServiceIdentity="calc", Address=new ServiceAddress() {Address = "http://localhost/calc1.svc" }, },
                new ServiceRegisteRequest() {ServiceIdentity="calc", Address=new ServiceAddress() {Address = "http://localhost/calc2.svc" }, },
                new ServiceRegisteRequest() {ServiceIdentity="stat", Address=new ServiceAddress() {Address = "http://localhost/stat1.svc" }, },
            };

        internal static ServiceRegisteRequest[] registerRequests2 = new ServiceRegisteRequest[]
            {
                new ServiceRegisteRequest() {ServiceIdentity="stat", Address=new ServiceAddress() {Address = "http://localhost/stat2.svc" }, },
                new ServiceRegisteRequest() {ServiceIdentity="calc", Address=new ServiceAddress() {Address = "http://localhost/calc3.svc" }, },
                new ServiceRegisteRequest() {ServiceIdentity="view", Address=new ServiceAddress() {Address = "http://localhost/view1.svc" }, },
                new ServiceRegisteRequest() {ServiceIdentity="stat", Address=new ServiceAddress() {Address = "http://localhost/stat1.svc" }, },
            };
    }
}
