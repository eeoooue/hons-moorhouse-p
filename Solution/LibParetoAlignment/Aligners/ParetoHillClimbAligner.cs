using LibBioInfo;
using LibModification;
using LibModification.AlignmentInitializers;
using LibModification.AlignmentModifiers;
using LibModification.AlignmentModifiers.MultiRowStochastic;
using LibParetoAlignment.Helpers;
using LibScoring;
using LibSimilarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibParetoAlignment.Aligners
{
    public class ParetoHillClimbAligner : ParetoIterativeAligner
    {
        public IAlignmentModifier Modifier = new MultiRowStochasticSwapOperator();

        private FastNonDominatedSort FastNonDominatedSort = new FastNonDominatedSort();
        private CrowdingDistanceAssignment CrowdingDistanceAssignment = new CrowdingDistanceAssignment();

        List<TradeoffAlignment> Archive = new List<TradeoffAlignment>();
        ParetoHelper ParetoHelper = new ParetoHelper();

        private TradeoffAlignment CurrentSolution = null!;

        public ParetoHillClimbAligner(List<IFitnessFunction> objectives) : base(objectives)
        {

        }

        public override Alignment GetCurrentAlignment()
        {
            return CurrentSolution.Alignment;
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
            SimilarityGuide.SetSequences(sequences);
            Alignment alignment = Initializer.CreateInitialAlignment(sequences);
            InitializeForRefinement(alignment);
        }

        public override void InitializeForRefinement(Alignment alignment)
        {
            SimilarityGuide.SetSequences(alignment.Sequences);
            TradeoffAlignment tradeoff = EvaluateAlignment(alignment);
            AddSolutionToArchive(tradeoff);
        }

        public override void PerformIteration()
        {
            Alignment alignment = CurrentSolution.Alignment.GetCopy();

            Modifier.ModifyAlignment(alignment);
            TradeoffAlignment tradeoff = EvaluateAlignment(alignment);

            AddSolutionToArchive(tradeoff);
        }

        private void AddSolutionToArchive(TradeoffAlignment alignment)
        {
            Archive.Add(alignment);
            SortAndTrimArchive();
            CurrentSolution = Archive[0];
        }

        public void SortAndTrimArchive()
        {
            List<TradeoffAlignment> sorted = FastNonDominatedSort.SortTradeoffs(Archive);
            CrowdingDistanceAssignment.AssignDistances(sorted);
            CrowdedComparisonOperator.SortTradeoffs(sorted);

            Archive.Clear();

            int n = Math.Min(NumberOfTradeoffs, sorted.Count);

            for(int i=0; i<n; i++)
            {
                Archive.Add(sorted[i]);
            }
        }

        public override List<string> GetDebuggingInfo()
        {
            List<string> result = new List<string>();

            foreach(string item in GetAlignerInfo())
            {
                result.Add(item);
            }

            foreach (string item in GetSolutionInfo())
            {
                result.Add(item);
            }

            return result;
        }

        public override List<string> GetAlignerInfo()
        {
            List<string> result = new List<string>()
            {
                $"{GetName()}",
                $" - [Iterations] {IterationsCompleted}",
                $" - [Archive] {Archive.Count} solutions",
            };

            return result;
        }

        public override List<string> GetSolutionInfo()
        {
            TradeoffAlignment current = CurrentSolution;
            Alignment alignment = current.Alignment;

            List<string> result = new List<string>();

            result.Add($"Current Alignment:");
            result.Add($" - [Dimensions] ({alignment.Height} x {alignment.Width})");

            foreach(string score in current.Scores.Keys)
            {

                string line = $" - [Obj.] {score}: {Math.Round(current.GetScore(score), 3)}";
                result.Add(line);
            }

            return result;
        }
    }
}
