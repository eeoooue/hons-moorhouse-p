using LibAlignment;
using LibAlignment.Aligners;
using LibScoring.ObjectiveFunctions;
using LibScoring.ScoringMatrices;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAlignment.Aligners.SingleState;
using LibBioInfo.IAlignmentModifiers;
using LibBioInfo;

namespace MAli.AlignmentConfigs
{
    internal class IteratedLocalSearchConfig : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetILSAligner();
        }

        public IterativeAligner GetILSAligner()
        {
            IScoringMatrix blosum = new BLOSUM62Matrix();
            IObjectiveFunction sumOfPairsWithAffine = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(blosum);
            int iterations = 1000;

            IteratedLocalSearchAligner aligner = new IteratedLocalSearchAligner(sumOfPairsWithAffine, iterations);

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>();
            modifiers.Add(new MultiRowStochasticSwapOperator());
            modifiers.Add(new SwapOperator());
            modifiers.Add(new GapInserter());

            MultiOperatorModifier tweak = new MultiOperatorModifier(modifiers);

            aligner.TweakModifier = tweak;
            aligner.PerturbModifier = tweak;

            return aligner;
        }
    }
}
