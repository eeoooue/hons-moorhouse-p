using LibBioInfo;
using LibModification.AlignmentModifiers.MultiRowStochastic;
using LibModification;
using LibParetoAlignment.Helpers;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibModification.AlignmentModifiers;
using LibModification.CrossoverOperators;

namespace LibParetoAlignment.Aligners
{
    public class NSGA2Aligner : ParetoIterativeAligner
    {
        public IAlignmentModifier RefinementPerturbOperator = new GapShifter();
        public ICrossoverOperator CrossoverOperator = new RowBasedCrossoverOperator();
        public IAlignmentModifier MutationOperator = new MultiRowStochasticSwapOperator();

        private FastNonDominatedSort FastNonDominatedSort = new FastNonDominatedSort();
        private CrowdingDistanceAssignment CrowdingDistanceAssignment = new CrowdingDistanceAssignment();

        List<TradeoffAlignment> Archive = new List<TradeoffAlignment>();

        private TradeoffAlignment CurrentSolution = null!;

        public int ParentPopSize = 30;
        public int PopulationSize = 50;

        public NSGA2Aligner(List<IFitnessFunction> objectives) : base(objectives)
        {

        }

        public override List<Alignment> CollectTradeoffSolutions()
        {
            List<Alignment> result = new List<Alignment>();
            List<TradeoffAlignment> population = PickBestNFromArchive(NumberOfTradeoffs);
            foreach (TradeoffAlignment tradeoff in population)
            {
                result.Add(tradeoff.Alignment);
            }

            return result;
        }

        public override List<string> GetDebuggingInfo()
        {
            List<string> result = new List<string>();

            foreach (string item in GetAlignerInfo())
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
                $" - [Tradeoffs] {NumberOfTradeoffs}",
                $" - [Iterations] {IterationsCompleted}",
                $" - [Population] {Archive.Count} solutions",
            };

            return result;
        }

        public override Alignment GetCurrentAlignment()
        {
            return CurrentSolution.Alignment;
        }

        public override string GetName()
        {
            return $"(Pareto) Non-Dominated Sorting Genetic Algorithm (NSGA-II)";
        }

        public override List<string> GetSolutionInfo()
        {
            TradeoffAlignment current = CurrentSolution;
            Alignment alignment = current.Alignment;

            List<string> result = new List<string>();

            result.Add($"Current Alignment:");
            result.Add($" - [Dimensions] ({alignment.Height} x {alignment.Width})");

            foreach (string score in current.Scores.Keys)
            {

                string line = $" - [Obj.] {score}: {Math.Round(current.GetScore(score), 3)}";
                result.Add(line);
            }

            return result;
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            AlignmentRandomizer randomizer = new AlignmentRandomizer();

            Archive.Clear();
            while (Archive.Count < PopulationSize)
            {
                Alignment alignment = new Alignment(sequences);
                randomizer.ModifyAlignment(alignment);
                TradeoffAlignment tradeoffMember = EvaluateAlignment(alignment);
                AddSolutionToArchive(tradeoffMember);
            }

            SortAndTrimArchive();
        }

        public override void InitializeForRefinement(Alignment alignment)
        {
            Archive.Clear();
            TradeoffAlignment tradeoff = EvaluateAlignment(alignment);
            AddSolutionToArchive(tradeoff);

            while (Archive.Count < PopulationSize)
            {
                Alignment member = alignment.GetCopy();
                RefinementPerturbOperator.ModifyAlignment(member);
                TradeoffAlignment tradeoffMember = EvaluateAlignment(member);
                AddSolutionToArchive(tradeoffMember);
            }

            SortAndTrimArchive();
        }

        public override void PerformIteration()
        {
            Alignment alignment = CurrentSolution.Alignment.GetCopy();

            List<TradeoffAlignment> parents = PickBestNFromArchive(ParentPopSize);
            List<TradeoffAlignment> children = CreateChildren(parents);
            ReplacePopulationWithParentsAndChildren(parents, children);
            SortAndTrimArchive();
        }

        public void ReplacePopulationWithParentsAndChildren(List<TradeoffAlignment> parents, List<TradeoffAlignment> children)
        {
            Archive.Clear();
            foreach(TradeoffAlignment parent in parents)
            {
                AddSolutionToArchive(parent);
            }
            foreach(TradeoffAlignment child in children)
            {
                AddSolutionToArchive(child);
            }
        }


        public List<TradeoffAlignment> CreateChildren(List<TradeoffAlignment> parents)
        {
            int targetCount = PopulationSize - parents.Count;
            List<TradeoffAlignment> children = new List<TradeoffAlignment>();

            while (children.Count < targetCount)
            {
                Alignment a = PickRandomTradeoff(parents).Alignment;
                Alignment b = a;
                while (a == b)
                {
                    b = PickRandomTradeoff(parents).Alignment;
                }

                Alignment child;
                if (Randomizer.CoinFlip())
                {
                    child = a.GetCopy();
                    MutationOperator.ModifyAlignment(child);
                }
                else if (Randomizer.CoinFlip())
                {
                    child = b.GetCopy();
                    MutationOperator.ModifyAlignment(child);
                }
                else
                {
                    List<Alignment> pair = CrossoverOperator.CreateAlignmentChildren(a, b);
                    int i = Randomizer.CoinFlip() ? 0 : 1;
                    child = pair[i];
                }

                TradeoffAlignment childTradeoff = EvaluateAlignment(child);
                children.Add(childTradeoff);
            }

            return children;
        }

        public TradeoffAlignment PickRandomTradeoff(List<TradeoffAlignment> tradeoffs)
        {
            int i = Randomizer.Random.Next(tradeoffs.Count);
            return tradeoffs[i];
        }



        public List<TradeoffAlignment> PickBestNFromArchive(int n)
        {
            n = Math.Min(n, Archive.Count);

            List<TradeoffAlignment> result = new List<TradeoffAlignment>();
            for (int i=0; i<n; i++)
            {
                result.Add(Archive[i]);
            }

            return result;
        }



        private void AddSolutionToArchive(TradeoffAlignment alignment)
        {
            Archive.Add(alignment);
        }

        public void SortAndTrimArchive()
        {
            List<TradeoffAlignment> sorted = FastNonDominatedSort.SortTradeoffs(Archive);
            CrowdingDistanceAssignment.AssignDistances(sorted);
            CrowdedComparisonOperator.SortTradeoffs(sorted);

            Archive.Clear();

            int n = Math.Min(PopulationSize, sorted.Count);

            for (int i = 0; i < n; i++)
            {
                Archive.Add(sorted[i]);
            }

            CurrentSolution = Archive[0];
        }
    }
}
