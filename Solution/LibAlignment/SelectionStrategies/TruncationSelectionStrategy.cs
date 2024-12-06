using LibAlignment.Helpers;
using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.SelectionStrategies
{
    internal class TruncationSelectionStrategy : ISelectionStrategy
    {
        AlignmentSelectionHelper Helper = new AlignmentSelectionHelper();
        List<ScoredAlignment> Candidates = new List<ScoredAlignment>();
        public int CurrentIndex = 0;

        public void PreprocessCandidateAlignments(List<ScoredAlignment> candidates)
        {
            Candidates = candidates;
            Helper.SortScoredAlignments(Candidates);
            CurrentIndex = 0;
        }

        public Alignment SelectCandidate()
        {
            ScoredAlignment choice = Candidates[CurrentIndex];
            CurrentIndex++;
            return choice.Alignment;
        }
    }
}
