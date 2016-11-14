using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sparticle.Config.Types
{
    public class ActionAccessConfig : IHasConfigKey
    {
        [XmlAttribute("ActionName")]
        public string ActionName { get; set; }
        
        [XmlAttribute("AccessLevel")]
        public string AccessLevel { get; set; }

        [XmlAttribute("LogOption")]
        public string LogOption { get; set; }

        [XmlAttribute("Debug")]
        public bool ServiceDebug { get; set; }

        public static readonly string DefaultLogOption = string.Format("{0}|{1}", ((int)TraceLevel.All).ToString(), ((int)TraceLevel.Care).ToString());

        public ActionAccessConfig()
        {
            this.LogOption = DefaultLogOption;
            this.AccessLevel = Types.AccessLevel.NoLimit.ToString();
        }

        public string ConfigKey
        {
            get
            {
                return "ActionGlobalConfig." + this.ActionName;
            }
        }
    }
}
