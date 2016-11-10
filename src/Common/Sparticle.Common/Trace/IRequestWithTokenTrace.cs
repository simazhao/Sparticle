using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public interface IRequestWithTokenTrace
    {
        string Nonce { get; set; }

        long Timestamp { get; set; }

        string Signature { get; set; }

        string ClientKey { get; set; }

        string UserKey { get; set; }
    }
}
