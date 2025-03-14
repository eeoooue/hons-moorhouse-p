using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.SelectionStrategies
{
    public class RouletteSelectionStrategy : ISelectionStrategy
    {
        List<ScoredAlignment> Candidates = new List<ScoredAlignment>();

        public double TotalValue = 0;

        public void PreprocessCandidateAlignments(List<ScoredAlignment> candidates)
        {
            Candidates = candidates;
            TotalValue = CalculateTotalValue(Candidates);
        }

        public double CalculateTotalValue(List<ScoredAlignment> candidates)
        {
            double result = 0;
            foreach(ScoredAlignment candidate in candidates)
            {
                result += candidate.Fitness;
            }

            return result;
        }

        public List<Alignment> SelectCandidates(int n)
        {
            List<Alignment> result = new List<Alignment>();

            for (int i = 0; i < n; i++)
            {
                Alignment candidate = SelectCandidate();
                result.Add(candidate);
            }

            return result;
        }

        public Alignment SelectCandidate()
        {
            double roll = Randomizer.Random.NextDouble();
            double scaledValue = roll * TotalValue;

            double total = 0;
            foreach(ScoredAlignment candidate in Candidates)
            {
                total += candidate.Fitness;
                if (total > scaledValue)
                {
                    return candidate.Alignment;
                }
            }

            return Candidates[Candidates.Count-1].Alignment;
        }
    }
}
