using LibAlignment;
using LibScoring.ObjectiveFunctions;
using LibScoring.ScoringMatrices;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibBioInfo.IAlignmentModifiers;
using LibBioInfo;
using LibAlignment.Aligners.PopulationBased;

namespace MAli.AlignmentConfigs
{
    public class MewLambdaEvolutionaryAlgorithmAlignerConfig : AlignmentConfig
    {
        public override MewLambdaEvolutionaryAlgorithmAligner CreateAligner()
        {
            return GetSprint04Version();
        }

        private MewLambdaEvolutionaryAlgorithmAligner GetSprint04Version()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(matrix, 4, 1);

            const int maxIterations = 100;
            MewLambdaEvolutionaryAlgorithmAligner aligner = new MewLambdaEvolutionaryAlgorithmAligner(objective, maxIterations);
            aligner.Mew = 10;
            aligner.Lambda = aligner.Mew * 7;

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new MultiRowStochasticSwapOperator(),
                new SwapOperator(),
                new GapInserter(),
            };

            MultiOperatorModifier modifier = new MultiOperatorModifier(modifiers);
            aligner.MutationOperator = modifier;

            return aligner;
        }



        private MewLambdaEvolutionaryAlgorithmAligner GetVersion02()
        {
            IScoringMatrix blosum62 = new BLOSUM62Matrix();

            IObjectiveFunction sumOfPairs = new SumOfPairsObjectiveFunction(blosum62);
            IObjectiveFunction percentageTCCs = new TotallyConservedColumnsObjectiveFunction();

            List<IObjectiveFunction> objectives = new List<IObjectiveFunction>() { sumOfPairs, percentageTCCs };
            List<double> weights = new List<double>() { 0.1, 100.0 };

            IObjectiveFunction objective = new LinearCombinationOfWeightedObjectiveFunctions(objectives, weights);

            const int maxIterations = 100;
            MewLambdaEvolutionaryAlgorithmAligner aligner = new MewLambdaEvolutionaryAlgorithmAligner(objective, maxIterations);
            aligner.Mew = 10;
            aligner.Lambda = aligner.Mew * 7;

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new GapInserter(1),
                new GapShifter(),
                new BlockShuffler(),
            };

            MultiOperatorModifier modifier = new MultiOperatorModifier(modifiers);

            aligner.MutationOperator = modifier;


            return aligner;
        }


        private MewLambdaEvolutionaryAlgorithmAligner GetVersion01()
        {
            // this was used in the MAli v1.0 release

            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(matrix, 4, 1);
            const int maxIterations = 100;
            MewLambdaEvolutionaryAlgorithmAligner aligner = new MewLambdaEvolutionaryAlgorithmAligner(objective, maxIterations);
            aligner.Mew = 10;
            aligner.Lambda = aligner.Mew * 7;

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new GapInserter(1),
                new GapShifter(),
                new BlockShuffler(),
            };

            MultiOperatorModifier modifier = new MultiOperatorModifier(modifiers);

            aligner.MutationOperator = modifier;

            return aligner;
        }
    }
}
