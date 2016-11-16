using Sparticle.Request.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public interface IRequestTrace
    {
        RequestContext RequestContext { get; set; }

        IRequestWithTokenTrace RequestToken { get; set; }

        string User { get; set; }

        string Method { get; set; }

        string Parameters { get; set; }
    }
}
