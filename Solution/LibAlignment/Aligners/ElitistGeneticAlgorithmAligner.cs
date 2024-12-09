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
    public class ElitistGeneticAlgorithmAligner : Aligner
    {
        public List<Alignment> Population = new List<Alignment>();
        public ICrossoverOperator CrossoverOperator = new ColBasedCrossoverOperator();
        public IAlignmentModifier MutationOperator = new GapShifter();

        public ISelectionStrategy TruncationSelection = new TruncationSelectionStrategy();
        public ISelectionStrategy SelectionStrategy = new RouletteSelectionStrategy();

        public AlignmentSelectionHelper SelectionHelper = new AlignmentSelectionHelper();

        public int PopulationSize = 18;
        public int SelectionSize = 6;

        public ElitistGeneticAlgorithmAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override Alignment AlignSequences(List<BioSequence> sequences)
        {
            Initialize(sequences);
            CurrentAlignment = Population[0];
            AlignmentScore = ScoreAlignment(CurrentAlignment);

            while (IterationsCompleted < IterationsLimit)
            {
                Iterate();
                IterationsCompleted++;
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
            TruncationSelection.PreprocessCandidateAlignments(candidates);
            SelectionStrategy.PreprocessCandidateAlignments(candidates);

            List<Alignment> elites = TruncationSelection.SelectCandidates(SelectionSize);
            Population.Clear();
            foreach (Alignment elite in elites)
            {
                Population.Add(elite);
            }

            while (Population.Count < PopulationSize)
            {
                List<Alignment> children = BreedNewChildren();
                foreach(Alignment child in children)
                {
                    MutationOperator.ModifyAlignment(child);
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
    }
}
