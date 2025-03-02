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
        public override ParetoIterativeAligner CreateAligner()
        {
            List<IFitnessFunction> objectives = GetObjectives();

            NSGA2Aligner aligner = new NSGA2Aligner(objectives);
            aligner.MutationOperator = GetModifier();

            return aligner;
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

        public IAlignmentModifier GetModifier()
        {
            List<IAlignmentModifier> modifiers = new List<IAlignmentModifier>()
            {
                new GuidedSwap(),
                new GuidedGapInserter(),
                new BlockShuffler(),
                new GapShuffler(),
            };

            IAlignmentModifier result = new MultiOperatorModifier(modifiers);

            return result;
        }
    }
}
