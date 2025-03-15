using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibScoring;
using LibAlignment.SelectionStrategies;
using LibModification;
using LibModification.AlignmentModifiers;

namespace LibAlignment.Aligners.PopulationBased
{
    public class MewLambdaEvolutionaryAlgorithmAligner : PopulationBasedAligner
    {
        public IAlignmentModifier MutationOperator = new GapShifter();
        public ISelectionStrategy SelectionStrategy = new TruncationSelectionStrategy();

        public int Mew = 5; // selection size
        public int Lambda { get { return PopulationSize; } } // population size

        public MewLambdaEvolutionaryAlgorithmAligner(IFitnessFunction objective, int iterations, int mew = 10, int lambda = 70) : base(objective, iterations, lambda)
        {
            Mew = mew;
        }

        public override string GetName()
        {
            return $"MewLambdaEvolutionaryAlgorithmAligner (selection={Mew}, population={Lambda})";
        }

        public override void PerformIteration()
        {
            List<ScoredAlignment> candidates = ScorePopulation(Population);
            SelectionStrategy.PreprocessCandidateAlignments(candidates);
            List<Alignment> parents = SelectionStrategy.SelectCandidates(Mew);
            Population.Clear();
            Population = ProduceNewPopulation(parents);
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
