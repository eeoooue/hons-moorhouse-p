using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring
{
    public abstract class NormalisedFitnessFunction : IFitnessFunction
    {
        public double GetFitness(in char[,] alignment)
        {
            double bestPossibleScore = GetBestPossibleScore(in alignment);
            double worstPossibleScore = GetWorstPossibleScore(in alignment);
            double fitnessRange = bestPossibleScore - worstPossibleScore;

            double score = ScoreAlignment(in alignment);
            double effectiveScore = score - worstPossibleScore;

            return effectiveScore / fitnessRange;
        }

        public abstract string GetName();

        public abstract double ScoreAlignment(in char[,] alignment);

        public abstract double GetBestPossibleScore(in char[,] alignment);

        public abstract double GetWorstPossibleScore(in char[,] alignment);
    }
}
