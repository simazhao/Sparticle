using Sparticle.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.DTO.Example
{
    public class MakeRandomIntRequest : IRequestDto
    {
        public int Seconds { get; set; }
    }
}
