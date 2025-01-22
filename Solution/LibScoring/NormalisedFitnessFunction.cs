using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring
{
    public abstract class NormalisedFitnessFunction : IFitnessFunction
    {
        public double GetFitness(char[,] alignment)
        {
            double bestPossibleScore = GetBestPossibleScore(alignment);
            double worstPossibleScore = GetWorstPossibleScore(alignment);
            double fitnessRange = bestPossibleScore - worstPossibleScore;

            double score = ScoreAlignment(alignment);
            double effectiveScore = score - worstPossibleScore;

            return effectiveScore / fitnessRange;
        }

        public abstract string GetName();

        public abstract double ScoreAlignment(char[,] alignment);

        public abstract double GetBestPossibleScore(char[,] alignment);

        public abstract double GetWorstPossibleScore(char[,] alignment);
    }
}
