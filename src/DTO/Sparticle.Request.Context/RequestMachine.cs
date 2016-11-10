using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Request.Context
{
    public class RequestDevice
    {
        public string Ip { get; set; }

        public string DeviceGid { get; set; }

        public string DeviceType { get; set; }

        public string DeviceOS { get; set; }

        public string AppName { get; set; }

        public string AppVersion { get; set; }

        public string ServerVersion { get; set; }
    }
}
