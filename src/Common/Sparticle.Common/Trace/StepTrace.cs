using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common.Trace
{
    public class StepTrace : IStepTrace
    {
        private IList<IStep> _steps = new List<IStep>();

        bool IStepTrace.SerializeResult { get; set; }

        IReadOnlyCollection<IStep> IStepTrace.Steps
        {
            get
            {
                return new ReadOnlyCollection<IStep>(_steps);
            }
        }

        void IStepTrace.AddStep(string message)
        {
            _steps.Add(new StepBase(message));
        }

        void IStepTrace.AddStep(IStep step)
        {
            _steps.Add(step);
        }
    }
}
