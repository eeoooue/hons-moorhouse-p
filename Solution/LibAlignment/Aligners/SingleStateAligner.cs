using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Aligners
{
    public abstract class SingleStateAligner : IterativeAligner
    {
        protected SingleStateAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {

        }
    }
}
