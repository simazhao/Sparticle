﻿using Sparticle.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.DTO.Example
{
    public class AddRequest : IRequestDto
    {
        public int Num1 { get; set; }

        public int Num2 { get; set; }
    }
}
