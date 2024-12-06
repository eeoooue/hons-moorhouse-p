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
    public class TruncationSelectionStrategy : ISelectionStrategy
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

        public List<Alignment> SelectCandidates(int n)
        {
            List<Alignment> result = new List<Alignment>();

            for(int i=0; i<n; i++)
            {
                Alignment candidate = SelectCandidate();
                result.Add(candidate);
            }

            return result;
        }

        public Alignment SelectCandidate()
        {
            if (CurrentIndex >= Candidates.Count)
            {
                CurrentIndex = 0;
            }

            ScoredAlignment choice = Candidates[CurrentIndex];
            CurrentIndex++;
            return choice.Alignment;
        }
    }
}
