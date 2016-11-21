using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.ServiceCollection.Interface
{
    [DataContract]
    public class ServiceAddressRequest
    {
        [DataContract]
        public enum MatchMode
        {
            [EnumMember]
            Ip = 1,

            [EnumMember]
            No,
        }

        [DataMember]
        public MatchMode MMode { get; set; }

        [DataMember]
        public string No { get; set; }

        [DataMember]
        public string ServiceGroup { get; set; }

        [DataMember]
        public string ServiceIdentity { get; set; }
    }
}
