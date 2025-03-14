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
using System.Globalization;
using LibSimilarity;

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

        List<TradeoffAlignment> Population = new List<TradeoffAlignment>();

        private TradeoffAlignment CurrentSolution = null!;

        public int ParentPopSize = 30;
        public int PopulationSize = 50;

        public NSGA2Aligner(List<IFitnessFunction> objectives) : base(objectives)
        {

        }

        public override List<Alignment> CollectTradeoffSolutions()
        {
            List<Alignment> result = new List<Alignment>();
            List<TradeoffAlignment> selection = PickBestNFromArchive(NumberOfTradeoffs);
            foreach (TradeoffAlignment tradeoff in selection)
            {
                result.Add(tradeoff.Alignment);
            }

            return result;
        }

        public override Alignment GetCurrentAlignment()
        {
            return CurrentSolution.Alignment;
        }

        public override void PerformIteration()
        {
            Alignment alignment = CurrentSolution.Alignment.GetCopy();

            List<TradeoffAlignment> parents = PickBestNFromArchive(ParentPopSize);
            List<TradeoffAlignment> children = CreateChildTradeoffs(parents);
            ReplacePopulationWithParentsAndChildren(parents, children);
        }


        #region Initialization

        public override void Initialize(List<BioSequence> sequences)
        {
            SimilarityGuide.SetSequences(sequences);
            Population.Clear();
            while (Population.Count < PopulationSize)
            {
                Alignment alignment = Initializer.CreateInitialAlignment(sequences);
                TradeoffAlignment tradeoffMember = EvaluateAlignment(alignment);
                Population.Add(tradeoffMember);
            }
            CurrentSolution = Population[0];
        }

        public override void InitializeForRefinement(Alignment alignment)
        {
            SimilarityGuide.SetSequences(alignment.Sequences);
            Population.Clear();
            TradeoffAlignment tradeoff = EvaluateAlignment(alignment);
            Population.Add(tradeoff);

            while (Population.Count < PopulationSize)
            {
                Alignment member = alignment.GetCopy();
                RefinementPerturbOperator.ModifyAlignment(member);
                TradeoffAlignment tradeoffMember = EvaluateAlignment(member);
                Population.Add(tradeoffMember);
            }

            CurrentSolution = Population[0];
        }

        #endregion


        #region Non-Dominated Sorting

        public void SortPopulationByDominationRanks()
        {
            List<TradeoffAlignment> sorted = FastNonDominatedSort.SortTradeoffs(Population);

            Population.Clear();
            int n = Math.Min(PopulationSize, sorted.Count);
            for (int i = 0; i < n; i++)
            {
                Population.Add(sorted[i]);
            }

            CurrentSolution = Population[0];
        }

        #endregion


        #region Selection

        public List<TradeoffAlignment> PickBestNFromArchive(int n)
        {
            SortPopulationByDominationRanks();
            n = Math.Min(n, Population.Count);

            Stack<TradeoffAlignment> stack = CollectNTradeoffsByDominationRankOnly(n);

            int worstRank = stack.Pop().FrontRank;

            while (stack.Count > 0)
            {
                TradeoffAlignment ali = stack.Pop();
                if (ali.FrontRank != worstRank)
                {
                    stack.Push(ali);
                    break;
                }
            }

            int missingSolutions = n - stack.Count;

            List<TradeoffAlignment> result = PickNBestSolutionsOfRank(missingSolutions, worstRank);

            while (stack.Count > 0)
            {
                result.Add(stack.Pop());
            }

            return result;
        }

        public Stack<TradeoffAlignment> CollectNTradeoffsByDominationRankOnly(int n)
        {
            SortPopulationByDominationRanks();

            Stack<TradeoffAlignment> result = new Stack<TradeoffAlignment>();
            for (int i = 0; i < n; i++)
            {
                result.Push(Population[i]);
            }

            return result;
        }

        public List<TradeoffAlignment> PickNBestSolutionsOfRank(int n, int rank)
        {
            List<TradeoffAlignment> candidates = CollectSolutionsOfRank(rank);

            CrowdingDistanceAssignment.AssignDistances(candidates);
            CrowdedComparisonOperator.SortTradeoffs(candidates);

            List<TradeoffAlignment> result = new List<TradeoffAlignment>();

            for(int i=0; i<n; i++)
            {
                result.Add(candidates[i]);
            }

            return result;
        }


        public List<TradeoffAlignment> CollectSolutionsOfRank(int rank)
        {
            List<TradeoffAlignment> result = new List<TradeoffAlignment>();

            foreach (TradeoffAlignment alignment in Population)
            {
                if (alignment.FrontRank == rank)
                {
                    result.Add(alignment);
                }
            }

            return result;
        }

        #endregion


        #region Replacing population with selected parents & new children

        public void ReplacePopulationWithParentsAndChildren(List<TradeoffAlignment> parents, List<TradeoffAlignment> children)
        {
            Population.Clear();
            foreach (TradeoffAlignment parent in parents)
            {
                Population.Add(parent);
            }
            foreach (TradeoffAlignment child in children)
            {
                Population.Add(child);
            }
        }

        public List<TradeoffAlignment> CreateChildTradeoffs(List<TradeoffAlignment> parents)
        {
            int targetCount = PopulationSize - parents.Count;
            List<TradeoffAlignment> result = new List<TradeoffAlignment>();

            List<Alignment> children = CreateChildAlignments(parents);

            foreach (Alignment alignment in children)
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

                foreach (Alignment child in childPair)
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

        #endregion


        #region Debugging info

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
                $" - [Population] {Population.Count} solutions",
            };

            return result;
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

        #endregion
    }
}
