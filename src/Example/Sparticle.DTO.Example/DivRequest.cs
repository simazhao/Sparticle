using Sparticle.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.DTO.Example
{
    public class DivRequest : IRequestDto
    {
        public int Divider { get; set; }

        public int Dividend { get; set; }
    }
}
