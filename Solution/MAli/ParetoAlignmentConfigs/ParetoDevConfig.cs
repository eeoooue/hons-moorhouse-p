using LibAlignment.Aligners.SingleState;
using LibBioInfo;
using LibBioInfo.ScoringMatrices;
using LibModification;
using LibModification.AlignmentModifiers;
using LibModification.AlignmentModifiers.Guided;
using LibModification.AlignmentModifiers.MultiRowStochastic;
using LibParetoAlignment;
using LibParetoAlignment.Aligners;
using LibScoring;
using LibScoring.FitnessFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAli.ParetoAlignmentConfigs
{
    public class ParetoDevConfig : ParetoAlignmentConfig
    {

        public IFitnessFunction GetObjectiveOne()
        {
            IScoringMatrix matrix = new PAM250Matrix();
            IFitnessFunction result = new SumOfPairsWithAffineGapPenaltiesFitnessFunction(matrix);
            return result;
        }

        public IFitnessFunction GetObjectiveTwo()
        {
            IFitnessFunction result = new TotallyConservedColumnsFitnessFunction();
            return result;
        }

        public IFitnessFunction GetObjectiveThree()
        {
            IFitnessFunction result = new NonGapsFitnessFunction();
            return result;
        }

        public List<IFitnessFunction> GetObjectives()
        {
            List<IFitnessFunction> result = new List<IFitnessFunction>()
            {
                GetObjectiveOne(),
                GetObjectiveTwo(),
                GetObjectiveThree(),
            };

            return result;
        }


        public IAlignmentModifier GetModifier()
        {
            //List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            //{
            //    new SwapOperator(),
            //    new GapInserter(),
            //    new MultiRowStochasticSwapOperator(),
            //    new HeuristicPairwiseModifier(),
            //};

            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new GuidedSwap(),
                new GuidedGapInserter(),
                new GuidedResidueShifter(),
                // new GuidedRelativeScroll(),

                new SwapOperator(),
                new GapInserter(),
                new ResidueShifter(),

                // new GapShifter(),
                // new MultiRowStochasticSwapOperator(),
                new HeuristicPairwiseModifier(),
                // new ResidueShifter(),
                //new SmartBlockPermutationOperator(new PAM250Matrix()),
                //new SmartBlockScrollingOperator(new PAM250Matrix()),
            };

            IAlignmentModifier result = new MultiOperatorModifier(modifiers);

            return result;
        }

        public override ParetoIterativeAligner CreateAligner()
        {
            List<IFitnessFunction> objectives = GetObjectives();
            ParetoHillClimbAligner aligner = new ParetoHillClimbAligner(objectives);
            aligner.Modifier = GetModifier();

            return aligner;
        }
    }
}
