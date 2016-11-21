using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sparticle.ServiceCollection.Wcf
{
    public class ServiceCollectionConfig
    {
        public List<ServiceForMachine> Services { get; set; }
    }

    public class ServiceForMachine
    {
        [XmlAttribute]
        public string Identity { get; set; }

        [XmlAttribute]
        public string Desc { get; set; }

        [XmlAttribute]
        public string Group { get; set; }

        private List<ServiceForMachineProp> _proplist = new List<ServiceForMachineProp>();
        [XmlArrayItem("Add")]
        public List<ServiceForMachineProp> PropList { get { return _proplist; } set { _proplist = value; } }


        [XmlArrayItem("Machine")]
        public List<ServiceForMachineItem> Machines { get; set; }
    }

    public class ServiceForMachineProp
    {
        [XmlAttribute]
        public string Key { get; set; }

        [XmlAttribute]
        public string Value { get; set; }
    }

    public class ServiceForMachineItem
    {
        [XmlAttribute]
        public string ClientIp { get; set; }

        [XmlAttribute]
        public string No { get; set; }

        [XmlAttribute]
        public string ServiceAddress { get; set; }
    }
}