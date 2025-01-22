using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring
{
    internal interface INormalizedFitnessFunction
    {
        public double GetBestPossibleFitness(Alignment alignment);

        public double GetWorstPossibleFitness(Alignment alignment);

        public double GetFitness(Alignment alignment);

        public string GetName();
    }
}
