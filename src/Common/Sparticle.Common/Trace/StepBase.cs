using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common.Trace
{
    public class StepBase : IStep
    {
        public DateTime BeginTime { get; set; }

        public string Message { get; set; }

        public StepBase()
            :this(string.Empty)
        {

        }

        public StepBase(string message)
        {
            BeginTime = DateTime.Now;

            Message = message;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", BeginTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), Message);
        }
    }
}
