using LibAlignment;
using LibAlignment.Aligners;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAlignment.Aligners.SingleState;
using LibAlignment.Aligners.PopulationBased;
using LibBioInfo;
using LibScoring.FitnessFunctions;
using LibModification;
using LibModification.AlignmentModifiers;
using LibBioInfo.ScoringMatrices;

namespace MAli.AlignmentConfigs
{
    internal class DevelopmentConfig : AlignmentConfig
    {
        public override IterativeAligner CreateAligner()
        {
            return GetAligner();
        }

        public IFitnessFunction GetObjective()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IFitnessFunction objectiveA = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix, 4, 1);
            IFitnessFunction objectiveB = new TotallyConservedColumnsFitnessFunction();
            IFitnessFunction objectiveC = new NonGapsFitnessFunction();

            List<IFitnessFunction> functions = new List<IFitnessFunction>();
            functions.Add(objectiveA);
            functions.Add(objectiveB);

            List<double> weights = new List<double>() {0.90, 0.10 };

            WeightedCombinationOfFitnessFunctions weightedCombo = new WeightedCombinationOfFitnessFunctions(functions, weights);

            return weightedCombo;
        }


        private IterativeAligner GetAligner()
        {
            IFitnessFunction objective = GetObjective();

            const int maxIterations = 100;

            IteratedLocalSearchAligner aligner = new IteratedLocalSearchAligner(objective, maxIterations);

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new SwapOperator(),
                new GapInserter(),
                new MultiRowStochasticSwapOperator(),
                new HeuristicPairwiseModifier(),
            };

            aligner.TweakModifier = new MultiOperatorModifier(modifiers);
            aligner.PerturbModifier = new MultiRowStochasticSwapOperator();

            return aligner;
        }

    }
}
