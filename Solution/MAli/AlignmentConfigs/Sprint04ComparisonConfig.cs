using LibAlignment;
using LibAlignment.Aligners;
using LibAlignment.Aligners.PopulationBased;
using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.AlignmentConfigs
{
    internal class Sprint04ComparisonConfig : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            throw new NotImplementedException();
        }

        public IAlignmentModifier GetOperator()
        {
            throw new NotImplementedException();
        }
        public IObjectiveFunction GetObjective()
        {
            throw new NotImplementedException();
        }

        public PopulationBasedAligner GetPopulationBasedAligner()
        {
            throw new NotImplementedException();
        }

        public SingleStateAligner GetSingleStateAligner()
        {
            throw new NotImplementedException();
        }
    }
}
