﻿using LibBioInfo;
using LibModification;
using LibModification.AlignmentInitializers;
using LibModification.AlignmentModifiers;
using LibScoring;
using LibSimilarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Aligners
{
    public abstract class PopulationBasedAligner : IterativeAligner
    {
        public List<Alignment> Population = new List<Alignment>();
        public IAlignmentModifier RefinementPerturbOperator = new GapShifter();

        public int PopulationSize;

        protected PopulationBasedAligner(IFitnessFunction objective, int iterations, int populationSize) : base(objective, iterations)
        {
            PopulationSize = populationSize;
        }

        public override void InitializeForRefinement(Alignment alignment)
        {
            SimilarityGuide.SetSequences(alignment.Sequences);
            Population.Clear();
            Population.Add(alignment);

            while (Population.Count < PopulationSize)
            {
                Alignment member = alignment.GetCopy();
                RefinementPerturbOperator.ModifyAlignment(member);
                Population.Add(member);
            }

            CurrentBest = GetScoredAlignment(Population[0]);
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            SimilarityGuide.SetSequences(sequences);
            Population.Clear();
            while (Population.Count < PopulationSize)
            {
                Alignment alignment = Initializer.CreateInitialAlignment(sequences);
                Population.Add(alignment);
            }

            CurrentBest = GetScoredAlignment(Population[0]);
        }

        public List<ScoredAlignment> ScorePopulation(List<Alignment> population)
        {
            List<ScoredAlignment> candidates = new List<ScoredAlignment>();
            foreach (Alignment alignment in population)
            {
                double score = ScoreAlignment(alignment);
                ScoredAlignment candidate = new ScoredAlignment(alignment, score);
                candidates.Add(candidate);
                CheckNewBest(candidate);
            }

            SetFitnesses(candidates);

            return candidates;
        }

        public void SetFitnesses(List<ScoredAlignment> candidates)
        {
            double bestScore = GetBestScore(candidates);
            double worstScore = GetWorstScore(candidates);
            double range = bestScore - worstScore;

            foreach (ScoredAlignment candidate in candidates)
            {
                SetFitness(candidate, worstScore, range);
            }
        }

        public void SetFitness(ScoredAlignment candidate, in double worstScore, in double range)
        {
            double unscaledFitness = candidate.Score - worstScore;
            double scaled = unscaledFitness / range;
            candidate.Fitness = scaled;
        }

        public double GetBestScore(List<ScoredAlignment> candidates)
        {
            double bestScore = double.MinValue;
            foreach (ScoredAlignment candidate in candidates)
            {
                double score = candidate.Score;
                bestScore = Math.Max(score, bestScore);
            }

            return bestScore;
        }

        public double GetWorstScore(List<ScoredAlignment> candidates)
        {
            double worstScore = double.MaxValue;
            foreach (ScoredAlignment candidate in candidates)
            {
                double score = candidate.Score;
                worstScore = Math.Min(score, worstScore);
            }

            return worstScore;
        }
    }
}
