using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Security.Attack
{
    public class ReplayAttackParam
    {
        public string ClientId { get; set; }
        
        public string Nonce { get; set; }
        
        public long TimeStamp { get; set; }
        
        public string Signature { get; set; }
        
        public string Message { get; set; } 
    }
}
