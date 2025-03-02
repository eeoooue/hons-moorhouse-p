using LibBioInfo;
using LibModification;
using LibModification.AlignmentInitializers;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment
{
    public abstract class ParetoIterativeAligner
    {
        public List<IFitnessFunction> Objectives;

        public IAlignmentInitializer Initializer = new RelativeOffsetInitializer();

        public int IterationsCompleted { get; protected set; } = 0;

        public int IterationsLimit { get; set; } = 0;

        public int NumberOfTradeoffs { get; set; } = 10;

        public ParetoIterativeAligner(List<IFitnessFunction> objectives)
        {
            Objectives = objectives;
        }

        public abstract List<string> GetDebuggingInfo();

        public abstract List<string> GetAlignerInfo();

        public abstract List<string> GetSolutionInfo();

        public abstract Alignment GetCurrentAlignment();

        public abstract List<Alignment> CollectTradeoffSolutions();

        public abstract string GetName();

        public abstract void Initialize(List<BioSequence> sequences);

        public abstract void InitializeForRefinement(Alignment alignment);

        public abstract void PerformIteration();

        public void Iterate()
        {
            PerformIteration();
            IterationsCompleted++;
        }

        public TradeoffAlignment EvaluateAlignment(Alignment alignment)
        {
            TradeoffAlignment result = new TradeoffAlignment(alignment);
            foreach(IFitnessFunction objective in Objectives)
            {
                string name = objective.GetName();
                double score = objective.GetFitness(alignment.CharacterMatrix);
                result.SetScore(name, score);
            }

            return result;
        }
    }
}
