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

namespace LibAlignment.Aligners
{
    public class GeneticAlgorithmAligner : Aligner
    {
        public List<Alignment> Population = new List<Alignment>();
        public ICrossoverOperator CrossoverOperator = new RowBasedCrossoverOperator();
        public IAlignmentModifier MutationOperator = new PercentileGapShifter(0.05);
        public ISelectionStrategy SelectionStrategy = new TruncationSelectionStrategy();

        public int PopulationSize = 6;
        public int SelectionSize = 4;

        public GeneticAlgorithmAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override Alignment AlignSequences(List<BioSequence> sequences)
        {
            Initialize(sequences);
            CurrentAlignment = Population[0];
            AlignmentScore = ScoreAlignment(CurrentAlignment);

            for (int i=0; i<IterationsLimit; i++)
            {
                Iterate();
            }

            return CurrentAlignment;
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            AlignmentRandomizer randomizer = new AlignmentRandomizer();
            Population.Clear();
            for (int i = 0; i < PopulationSize; i++)
            {
                Alignment alignment = new Alignment(sequences);
                randomizer.ModifyAlignment(alignment);
                Population.Add(alignment);
            }
        }

        public override void Iterate()
        {
            List<ScoredAlignment> candidates = ScorePopulation(Population);

            SelectionStrategy.PreprocessCandidateAlignments(candidates);
            List<Alignment> parents = SelectionStrategy.SelectCandidates(SelectionSize);

            List<Alignment> children = BreedAlignments(parents, PopulationSize);
            MutateAlignments(children);
            Population = children;
        }

        public List<ScoredAlignment> ScorePopulation(List<Alignment> population)
        {
            List<ScoredAlignment> candidates = new List<ScoredAlignment>();
            foreach (Alignment alignment in Population)
            {
                double score = ScoreAlignment(alignment);
                ScoredAlignment candidate = new ScoredAlignment(alignment, score);
                candidates.Add(candidate);
                CheckNewBest(candidate);
            }

            return candidates;
        }

        public List<Alignment> BreedAlignments(List<Alignment> parents, int numberOfChildren)
        {
            List<Alignment> result = new List<Alignment>();

            while (result.Count < numberOfChildren)
            {
                List<Alignment> children = BreedRandomParents(parents);
                foreach(Alignment child in children)
                {
                    result.Add(child);
                }
            }

            return result;
        }

        public List<Alignment> BreedRandomParents(List<Alignment> parents)
        {
            int n = parents.Count;

            while (true)
            {
                int i = Randomizer.Random.Next(n);
                int j = Randomizer.Random.Next(n);

                if (i != j)
                {
                    Alignment a = parents[i];
                    Alignment b = parents[j];
                    return CrossoverOperator.CreateAlignmentChildren(a, b);
                }
            }
        }

        public void MutateAlignments(List<Alignment> alignments)
        {
            foreach (Alignment alignment in alignments)
            {
                MutationOperator.ModifyAlignment(alignment);
            }
        }
    }
}
