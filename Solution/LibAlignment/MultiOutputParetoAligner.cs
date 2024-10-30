using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment
{
    public abstract class MultiOutputParetoAligner
    {
        public List<IObjectiveFunction> Objectives;
        public Alignment CurrentAlignment;
    }
}
