using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.ServiceKeeper.Interface
{
    [DataContract]
    public class ServiceUnregisteRequest
    {
        [DataMember]
        public string ServiceIdentity { get; set; }

        [DataMember]
        public string Address { get; set; }
    }
}
