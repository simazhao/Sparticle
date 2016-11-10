using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Request
{
    public class TokenRefreshRequest : IRequestDto
    {
        public string ClientId { get; set; }

        public string Token { get; set; }
    }
}
