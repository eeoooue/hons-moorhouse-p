﻿using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.ScoringMatrices
{
    public class IdentityMatrix : IScoringMatrix
    {
        public double GetBestPairwiseScorePossible()
        {
            return 1;
        }

        public string GetName()
        {
            return "Identity Matrix";
        }

        public List<char> GetResidues()
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return alphabet.ToList();
        }

        public double GetWorstPairwiseScorePossible()
        {
            return 0;
        }

        public int ScorePair(char a, char b)
        {
            if (a == Bioinformatics.GapCharacter || b == Bioinformatics.GapCharacter)
            {
                return 0;
            }

            if (a == b)
            {
                return 1;
            }

            return 0;
        }
    }
}
