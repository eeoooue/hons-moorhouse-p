using LibBioInfo.ICrossoverOperators;
using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibScoring;
using LibBioInfo.IAlignmentModifiers;
using LibAlignment.Helpers;
using LibAlignment.SelectionStrategies;

namespace LibAlignment.Aligners.PopulationBased
{
    public class MewLambdaEvolutionaryAlgorithmAligner : IterativeAligner
    {
        public List<Alignment> Population = new List<Alignment>();
        public IAlignmentModifier MutationOperator = new GapShifter();
        public ISelectionStrategy SelectionStrategy = new TruncationSelectionStrategy();

        public int Mew = 5; // selection size
        public int Lambda = 20; // population size

        public MewLambdaEvolutionaryAlgorithmAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override string GetName()
        {
            return $"MewLambdaEvolutionaryAlgorithmAligner (selection={Mew}, population={Lambda})";
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
                CheckShowDebuggingInfo();
            }

            return CurrentAlignment;
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            IAlignmentModifier randomizer = new AlignmentRandomizer();

            Population.Clear();
            for (int i = 0; i < Lambda; i++)
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
