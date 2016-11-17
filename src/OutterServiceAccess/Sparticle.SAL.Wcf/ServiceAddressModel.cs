using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.SAL.Wcf
{
    public class ServiceAddressModel
    {
        public string Address { get; set; }

        public string ServiceIdentity { get; set; }

        public IDictionary<string, string> PropertyList { get; set; }
    }
}
