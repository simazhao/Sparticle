using Sparticle.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.DTO.Example
{
    public class MakeRandomIntRequest : IRequestDto, ISelfCheck
    {
        public string WhoAreYou { get; set; }

        public int Seconds { get; set; }

        public bool Check(out string error)
        {
            error = null;

            if (string.IsNullOrEmpty(WhoAreYou))
            {
                error = "lost param";
                return false;
            }

            return true;
        }
    }
}
