using LibBioInfo;
using LibModification;
using LibModification.AlignmentModifiers;
using LibParetoAlignment.Helpers;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment.Aligners
{
    public class ParetoHillClimbAligner : ParetoIterativeAligner
    {
        IAlignmentModifier Modifier = new MultiRowStochasticSwapOperator();

        public int ArchiveGoalSize = 10;

        Queue<TradeoffAlignment> Archive = new Queue<TradeoffAlignment>();
        ParetoHelper ParetoHelper = new ParetoHelper();

        public ParetoHillClimbAligner(List<IFitnessFunction> objectives) : base(objectives)
        {

        }

        public override Alignment GetCurrentAlignment()
        {
            return Archive.Peek().Alignment;
        }

        public override string GetName()
        {
            return "(Pareto) Hill Climb Aligner";
        }

        public override List<Alignment> CollectTradeoffSolutions()
        {
            List<Alignment> result = new List<Alignment>();
            List<TradeoffAlignment> population = Archive.ToList();
            foreach(TradeoffAlignment tradeoff in population)
            {
                result.Add(tradeoff.Alignment);
            }

            return result;
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            Alignment alignment = new Alignment(sequences);
            TradeoffAlignment tradeoff = EvaluateAlignment(alignment);
            AddSolutionToArchive(tradeoff);
        }

        public override void InitializeForRefinement(Alignment alignment)
        {
            TradeoffAlignment tradeoff = EvaluateAlignment(alignment);
            AddSolutionToArchive(tradeoff);
        }

        public override void PerformIteration()
        {
            TradeoffAlignment current = Archive.Peek();
            Alignment alignment = current.Alignment.GetCopy();
            Modifier.ModifyAlignment(alignment);
            TradeoffAlignment tradeoff = EvaluateAlignment(alignment);
            if (ShouldAddSolutionToArchive(tradeoff))
            {
                AddSolutionToArchive(tradeoff);
            }
        }

        private void AddSolutionToArchive(TradeoffAlignment alignment)
        {
            if (Archive.Count == ArchiveGoalSize)
            {
                Archive.Dequeue();
            }

            Archive.Enqueue(alignment);
        }

        public bool ShouldAddSolutionToArchive(TradeoffAlignment alignment)
        {
            if (Archive.Count < ArchiveGoalSize)
            {
                return true;
            }

            List<TradeoffAlignment> population = Archive.ToList();

            return ParetoHelper.SolutionIsNonDominated(alignment, population);
        }
    }
}
