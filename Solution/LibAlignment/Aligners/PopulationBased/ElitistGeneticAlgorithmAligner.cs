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
    public class ElitistGeneticAlgorithmAligner : PopulationBasedAligner
    {
        public ICrossoverOperator CrossoverOperator = new ColBasedCrossoverOperator();
        public IAlignmentModifier MutationOperator = new GapShifter();
        public ISelectionStrategy SelectionStrategy = new RouletteSelectionStrategy();

        public double MutationRate = 0.50;
        public int SelectionSize = 6;

        public ElitistGeneticAlgorithmAligner(IFitnessFunction objective, int iterations, int populationSize = 18) : base(objective, iterations, populationSize)
        {

        }

        public override string GetName()
        {
            return $"ElitistGeneticAlgorithmAligner (population={PopulationSize}, selection={SelectionSize})";
        }

        public override void PerformIteration()
        {
            ISelectionStrategy truncationSelection = new TruncationSelectionStrategy();

            List<ScoredAlignment> candidates = ScorePopulation(Population);
            truncationSelection.PreprocessCandidateAlignments(candidates);
            SelectionStrategy.PreprocessCandidateAlignments(candidates);

            List<Alignment> elites = truncationSelection.SelectCandidates(SelectionSize);
            Population.Clear();
            foreach (Alignment elite in elites)
            {
                Population.Add(elite);
            }

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
