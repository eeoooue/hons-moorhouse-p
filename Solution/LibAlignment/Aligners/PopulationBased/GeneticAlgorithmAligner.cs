using LibAlignment.Helpers;
using LibAlignment.SelectionStrategies;
using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using LibBioInfo.ICrossoverOperators;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Aligners.PopulationBased
{
    public class GeneticAlgorithmAligner : PopulationBasedAligner
    {
        public ICrossoverOperator CrossoverOperator = new ColBasedCrossoverOperator();
        public ILegacyAlignmentModifier MutationOperator = new PercentileGapShifter(0.02);
        public ISelectionStrategy SelectionStrategy = new RouletteSelectionStrategy();

        public double MutationRate = 0.2;

        public GeneticAlgorithmAligner(IFitnessFunction objective, int iterations, int populationSize = 18) : base(objective, iterations, populationSize)
        {

        }

        public override string GetName()
        {
            return $"GeneticAlgorithmAligner (population={PopulationSize})";
        }

        public override void PerformIteration()
        {
            List<ScoredAlignment> candidates = ScorePopulation(Population);
            SelectionStrategy.PreprocessCandidateAlignments(candidates);

            Population.Clear();
            while (Population.Count < PopulationSize)
            {
                List<Alignment> children = BreedNewChildren();
                foreach (Alignment child in children)
                {
                    if (Randomizer.PercentageChanceEvent(MutationRate))
                    {
                        MutationOperator.ModifyAlignment(child);
                    }
                    Population.Add(child);
                }
            }
        }

        public List<Alignment> BreedNewChildren()
        {
            Alignment a = SelectionStrategy.SelectCandidate();
            Alignment b = SelectionStrategy.SelectCandidate();
            return CrossoverOperator.CreateAlignmentChildren(a, b);
        }
    }
}
