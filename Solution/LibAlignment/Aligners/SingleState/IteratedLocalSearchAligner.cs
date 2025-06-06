﻿using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibModification;
using LibModification.AlignmentModifiers;
using LibModification.CrossoverOperators;
using LibModification.AlignmentModifiers.MultiRowStochastic;

namespace LibAlignment.Aligners.SingleState
{
    public class IteratedLocalSearchAligner : SingleStateAligner
    {
        public IAlignmentModifier PerturbModifier = new MultiRowStochasticSwapOperator();
        public IAlignmentModifier TweakModifier = new MultiRowStochasticSwapOperator();

        private int ResetPoint = 0;
        private ScoredAlignment HomeBase = null!;
        protected ScoredAlignment S = null!;

        public IteratedLocalSearchAligner(IFitnessFunction objective, int iterations) : base(objective, iterations)
        {

        }

        public override string GetName()
        {
            return $"Iterated Local Search : (next restart @ {ResetPoint}, home base score = {HomeBase.Score})";
        }

        public override void PerformIteration()
        {
            if (IterationsCompleted == ResetPoint)
            {
                ContestHomeBase(S);
                S = GetPerturbationOfH();
                MarkUpcomingResetPoint();
            }

            Alignment r = S.Alignment.GetCopy();
            TweakModifier.ModifyAlignment(r);

            ScoredAlignment candidate = GetScoredAlignment(r);
            ContestS(candidate);
        }

        protected override void AdditionalSetup()
        {
            S = CurrentBest.GetCopy();
            HomeBase = S.GetCopy();
            MarkUpcomingResetPoint();
        }

        private void ContestS(ScoredAlignment candidate)
        {
            if (candidate.Score > S.Score)
            {
                S = candidate;
                CheckNewBest(S);
            }
        }

        private ScoredAlignment GetPerturbationOfH()
        {
            Alignment homeCopy = HomeBase.Alignment.GetCopy();
            PerturbModifier.ModifyAlignment(homeCopy);
            return GetScoredAlignment(homeCopy);
        }

        private void MarkUpcomingResetPoint()
        {
            int range = 200;
            int roll = Randomizer.Random.Next(1, range + 1);
            ResetPoint = IterationsCompleted + roll;
        }

        private void ContestHomeBase(ScoredAlignment candidate)
        {
            if (candidate.Score > HomeBase.Score)
            {
                HomeBase = candidate;
            }
        }
    }
}
