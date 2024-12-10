using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment.Helpers
{
    public class AlignmentSelectionHelper
    {
        public List<Alignment> SelectFittestParents(List<ScoredAlignment> candidates, int parentsToSelect)
        {
            SortScoredAlignments(candidates);

            List<Alignment> result = new List<Alignment>();
            for (int i = 0; i < parentsToSelect; i++)
            {
                result.Add(candidates[i].Alignment);
            }

            return result;
        }

        public void SortScoredAlignments(List<ScoredAlignment> alignments)
        {
            alignments.Sort((a, b) => b.Fitness.CompareTo(a.Fitness));
        }
    }
}
