using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Request
{
    // todo: 查阅参数验证的文章
    public class WithTokenRequest : IRequestDto
    {
        public string Nonce { get; set; }

        public long Timestamp { get; set; }

        public string Signature { get; set; }

        public string ClientKey { get; set; }

        public string UserKey { get; set; }

        public string SelectANo { get; set; }
    }
}
