using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSimilarity
{
    public static class RouletteWheel
    {
        public static SimilarityLink PerformSelectionOn(List<SimilarityLink> options)
        {
            List<RouletteSlice> slices = CreateRouletteSlices(options);

            double threshhold = RollThreshhold(slices);
            double total = 0;
            foreach (RouletteSlice slice in slices)
            {
                total += slice.Weighting;
                if (total >= threshhold)
                {
                    return slice.Link;
                }
            }

            int i = slices.Count - 1;
            return slices[i].Link;
        }

        public static List<RouletteSlice> CreateRouletteSlices(List<SimilarityLink> links)
        {
            double minValue = GetMinimumValue(links);
            List<RouletteSlice> result = new List<RouletteSlice>();
            foreach(SimilarityLink link in links)
            {
                double weight = link.SimilarityScore - minValue;
                RouletteSlice slice = new RouletteSlice(link, weight);
                result.Add(slice);
            }

            return result;
        }

        public static double GetMinimumValue(List<SimilarityLink> links)
        {
            double result = double.MaxValue;
            foreach(SimilarityLink link in links)
            {
                result = Math.Min(result, link.SimilarityScore);
            }

            return result;
        }


        public static double RollThreshhold(List<RouletteSlice> options)
        {
            double total = GetTotalScore(options);
            double roll = Randomizer.Random.NextDouble();
            return total * roll;
        }

        public static double GetTotalScore(List<RouletteSlice> options)
        {
            double total = 0.0;
            foreach (RouletteSlice option in options)
            {
                total += option.Weighting;
            }

            return total;
        }
    }
}
