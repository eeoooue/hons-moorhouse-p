using LibAlignment.SelectionStrategies;
using LibBioInfo;
using LibModification.AlignmentModifiers;
using LibModification;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Aligners.PopulationBased
{
    public class MewPlusLambdaEvolutionStrategyAligner : PopulationBasedAligner
    {
        public IAlignmentModifier MutationOperator = new GapShifter();
        public ISelectionStrategy SelectionStrategy = new TruncationSelectionStrategy();

        public int Mew = 5; // selection size
        public int Lambda = 20; // population size

        public MewPlusLambdaEvolutionStrategyAligner(IFitnessFunction objective, int iterations, int mew = 10, int lambda = 70) : base(objective, iterations, mew + lambda)
        {
            Mew = mew;
            Lambda = lambda;
        }

        public override string GetName()
        {
            return $"(Mew + Lambda) Evolution Strategy ({Mew}, {Lambda})";
        }

        public override void PerformIteration()
        {
            List<ScoredAlignment> candidates = ScorePopulation(Population);
            SelectionStrategy.PreprocessCandidateAlignments(candidates);
            List<Alignment> parents = SelectionStrategy.SelectCandidates(Mew);
            Population.Clear();
            List<Alignment> children = ProduceNewPopulation(parents);
            AddAlignmentsToPopulation(parents);
            AddAlignmentsToPopulation(children);
        }

        private void AddAlignmentsToPopulation(List<Alignment> alignments)
        {
            foreach(Alignment alignment in alignments)
            {
                Population.Add(alignment);
            }
        }

        public Alignment GetMutationOfParent(Alignment parent)
        {
            Alignment result = parent.GetCopy();
            MutationOperator.ModifyAlignment(result);
            return result;
        }

        public List<Alignment> ProduceNewPopulation(List<Alignment> parents)
        {
            List<Alignment> result = new List<Alignment>();

            int repetitions = Lambda / Mew;
            foreach (Alignment parent in parents)
            {
                for (int i = 0; i < repetitions; i++)
                {
                    Alignment child = GetMutationOfParent(parent);
                    result.Add(child);
                }
            }

            return result;
        }
    }
}
