using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.ServiceKeeper.Interface
{
    public interface IServiceKeeper
    {
        ServiceAddress GetServiceAddress(string serviceIdentity);
    }
}
