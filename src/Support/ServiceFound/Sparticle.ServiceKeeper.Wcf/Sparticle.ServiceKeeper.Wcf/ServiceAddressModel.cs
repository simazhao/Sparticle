using Sparticle.ServiceKeeper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class ServiceAddressBucket
    {
        public string ServiceIdentity { get; set; }

        public List<AccessedObject<ServiceAddress>> AvaliableSerivces { get; set; }

        public List<AccessedObject<ServiceAddress>> UnAvaliableSerivces { get; set; }

        public int Head { get; set; }
    }

    internal class AccessedObject<TData>
    {
        public int AccessCount { get; set; }

        public TData Data { get; set; }
    }

}