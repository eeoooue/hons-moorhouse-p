using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSimilarity
{
    internal class RouletteSlice
    {
        public SimilarityLink Link;
        public double Weighting;

        public RouletteSlice(SimilarityLink link, double weighting)
        {
            Link = link;
            Weighting = Math.Max(1e-10, weighting);
        }
    }
}
