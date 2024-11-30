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
        public ICrossoverOperator CrossoverOperator = new OnePointCrossoverOperator();

        public int PopulationSize = 6;
        public int SelectionSize = 4;

        public GeneticAlgorithmAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override Alignment AlignSequences(List<BioSequence> sequences)
        {
            Initialize(sequences);
            for(int i=0; i<IterationsLimit; i++)
            {
                Iterate();
            }

            return CurrentAlignment!;
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            InitializePopulation(sequences);
            CurrentAlignment = Population[0];
        }

        public override void Iterate()
        {
            List<Alignment> selectedParents = SelectFittestParents(Population, SelectionSize);
            List<Alignment> children = BreedAlignments(selectedParents, PopulationSize);
            MutateAlignments(children);
            Population = children;
            CurrentAlignment = SelectFittestMember(Population);
            AlignmentScore = ScoreAlignment(CurrentAlignment);
        }

        public void MutateAlignments(List<Alignment> alignments)
        {
            GapShifter shifter = new GapShifter();
            foreach(Alignment alignment in alignments)
            {
                shifter.ModifyAlignment(alignment);
            }
        }

        public void InitializePopulation(List<BioSequence> sequences)
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

        public List<double> ScoreAlignments(List<Alignment> alignments)
        {
            List<double> result = new List<double>();
            foreach(Alignment alignment in alignments)
            {
                double score = ScoreAlignment(alignment);
                result.Add(score);
            }

            return result;
        }

        public Alignment SelectFittestMember(List<Alignment> alignments)
        {
            Alignment result = alignments[0];
            double bestScore = ScoreAlignment(alignments[0]);

            for(int i=1; i<alignments.Count; i++)
            {
                double score = ScoreAlignment(alignments[i]);
                if (score > bestScore)
                {
                    result = alignments[i];
                    bestScore = score;
                }
            }

            return result;
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

        public List<Alignment> SelectFittestParents(List<Alignment> parents, int parentsToSelect)
        {
            List<Alignment> arr = parents.ToList();
            List<double> scores = ScoreAlignments(parents);

            int n = parents.Count;

            for (int i = 0; i < n; i++)
            {
                int positionOfNextBest = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (scores[j] > scores[positionOfNextBest])
                    {
                        positionOfNextBest = j;
                    }
                }

                if (positionOfNextBest != i)
                {
                    Alignment previous = arr[i];
                    Alignment newBest = arr[positionOfNextBest];
                    arr[i] = newBest;
                    arr[positionOfNextBest] = previous;
                }
            }

            List<Alignment> result = new List<Alignment>();
            for(int i=0; i<parentsToSelect; i++)
            {
                result.Add(arr[i]);
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
    }
}
