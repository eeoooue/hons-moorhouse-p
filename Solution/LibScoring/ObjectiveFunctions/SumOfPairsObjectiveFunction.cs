﻿using LibBioInfo;
using LibScoring.ScoringMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibScoring.ObjectiveFunctions
{
    public class SumOfPairsObjectiveFunction : IObjectiveFunction
    {
        ScoringMatrix Matrix;

        public SumOfPairsObjectiveFunction(ScoringMatrix matrix)
        {
            Matrix = matrix;
        }

        public double ScoreAlignment(Alignment alignment)
        {
            for(int i=0; i<alignment.Width; i++)
            {

            }

            return 0;
        }

        public double ScoreColumn(Alignment alignment, int i)
        {
            return 0;
        }
    }
}
