using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Request.Context
{
    public class RequestChannel
    {
        public string UserAgent { get; set; }

        public string Referer { get; set; }

        public string Market { get; set; }
    }
}
