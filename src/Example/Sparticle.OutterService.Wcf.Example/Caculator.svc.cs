using Sparticle.OutterService.Wcf.Example.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Sparticle.OutterService.Wcf.Example
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Caculator : ICaculator
    {
        public CalcResult Add(int num1, int num2)
        {
            return new CalcResult { Number = num1 + num2 };
        }

        public CalcResult Div(int div, int divd)
        {
            // may throw divbyzero exception, but we just want to throw.
            return new CalcResult { Number = div / divd };
        }
    }
}
