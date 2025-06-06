﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public interface IScoringMatrix
    {
        public abstract int ScorePair(char a, char b);

        public string GetName();

        public abstract List<char> GetResidues();

        public double GetBestPairwiseScorePossible();

        public double GetWorstPairwiseScorePossible();
    }
}
