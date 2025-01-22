using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring
{
    public interface IFitnessFunction
    {
        public double GetFitness(char[,] alignment);

        public string GetName();
    }
}
