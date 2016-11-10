using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public interface IStepTrace
    {
        IReadOnlyCollection<IStep> Steps { get; }

        void AddStep(IStep step);

        void AddStep(string message);
    }
}
