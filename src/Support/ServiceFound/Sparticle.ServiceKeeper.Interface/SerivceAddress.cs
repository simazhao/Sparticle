using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.ServiceKeeper.Interface
{
    [DataContract]
    public class ServiceAddress
    {
        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public IDictionary<string, string> PropertyList { get; set; }
    }
}
