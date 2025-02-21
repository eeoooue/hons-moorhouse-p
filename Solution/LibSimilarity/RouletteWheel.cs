using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSimilarity
{
    public class RouletteWheel
    {
        public SimilarityLink PerformSelectionOn(List<SimilarityLink> options)
        {
            double threshhold = RollThreshhold(options);
            double total = 0;
            foreach (SimilarityLink option in options)
            {
                total += option.SimilarityScore;
                if (total >= threshhold)
                {
                    return option;
                }
            }

            int i = options.Count - 1;
            return options[i];
        }

        public double RollThreshhold(List<SimilarityLink> options)
        {
            double total = GetTotalScore(options);
            double roll = Randomizer.Random.NextDouble();
            return total * roll;
        }

        public double GetTotalScore(List<SimilarityLink> options)
        {
            double total = 0.0;
            foreach (SimilarityLink option in options)
            {
                total += option.SimilarityScore;
            }

            return total;
        }
    }
}
