using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class ServiceAddressPoolModel
    {
        public Dictionary<string, ServiceAddressBucketModel> Buckets { get; set; }
    }

    internal class ServiceAddressBucketModel
    {
        public List<ServiceAddressNodeModel> AvaliableSerivces;

        public List<ServiceAddressNodeModel> UnAvaliableSerivces;
    }

    internal class ServiceAddressNodeModel
    {
        public string Address { get; set; }

        public string ClientIp { get; set; }

        public IDictionary<string, string> PropertyList { get; set; }
    }
}