using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring
{
    public class ScoredAlignment
    {
        public Alignment Alignment;
        public double Score;
        public double Fitness = 0.0;

        public ScoredAlignment(Alignment alignment, double score)
        {
            Alignment = alignment;
            Score = score;
        }

        public ScoredAlignment GetCopy()
        {
            Alignment alignment = Alignment.GetCopy();
            return new ScoredAlignment(alignment, Score);
        }
    }
}
