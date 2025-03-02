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
        public IAlignmentModifier MutationOperator = new MultiRowStochasticSwapOperator();

        public ICrossoverOperator CrossoverOperatorA = new RowBasedCrossoverOperator();
        public ICrossoverOperator CrossoverOperatorB = new ColBasedCrossoverOperator();

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
            Archive.Clear();
            while (Archive.Count < PopulationSize)
            {
                Alignment alignment = Initializer.CreateInitialAlignment(sequences);
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
            List<TradeoffAlignment> children = CreateChildTradeoffs(parents);
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


        public List<TradeoffAlignment> CreateChildTradeoffs(List<TradeoffAlignment> parents)
        {
            int targetCount = PopulationSize - parents.Count;
            List<TradeoffAlignment> result = new List<TradeoffAlignment>();

            List<Alignment> children = CreateChildAlignments(parents);

            foreach(Alignment alignment in children)
            {
                TradeoffAlignment childTradeoff = EvaluateAlignment(alignment);
                result.Add(childTradeoff);
            }


            return result;
        }

        public List<Alignment> CreateChildAlignments(List<TradeoffAlignment> parents)
        {
            int targetCount = PopulationSize - parents.Count;
            List<Alignment> children = new List<Alignment>();

            while (children.Count < targetCount)
            {
                Alignment a = PickRandomTradeoff(parents).Alignment;
                Alignment b = a;
                while (a == b)
                {
                    b = PickRandomTradeoff(parents).Alignment;
                }

                List<Alignment> childPair = CreateChildrenOf(a, b);

                foreach(Alignment child in childPair)
                {
                    if (RollToMutateChild())
                    {
                        MutationOperator.ModifyAlignment(child);
                    }

                    bool notExistingParent = DuplicationChecker.SolutionNotInList(child, parents);
                    bool notExistingChild = DuplicationChecker.SolutionNotInList(child, children);
                    bool novelSolution = notExistingParent && notExistingChild;

                    if (novelSolution && children.Count < targetCount)
                    {
                        children.Add(child);
                    }
                }
            }

            return children;
        }


        public List<Alignment> CreateChildrenOf(Alignment a, Alignment b)
        {
            if (Randomizer.CoinFlip())
            {
                return CrossoverOperatorA.CreateAlignmentChildren(a, b);
            }
            else
            {
                return CrossoverOperatorB.CreateAlignmentChildren(a, b);
            }
        }

        public bool RollToMutateChild()
        {
            int roll = Randomizer.Random.Next(1, 7);
            return roll == 6;
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
