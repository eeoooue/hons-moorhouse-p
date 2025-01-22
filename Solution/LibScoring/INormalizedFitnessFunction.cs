using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring
{
    public interface INormalizedFitnessFunction
    {
        public double GetBestPossibleFitness(char[,] alignment);

        public double GetWorstPossibleFitness(char[,] alignment);

        public double GetFitness(char[,] alignment);

        public string GetName();
    }
}
