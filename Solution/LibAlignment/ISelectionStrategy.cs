using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAlignment
{
    public interface ISelectionStrategy
    {
        public void PreprocessCandidateAlignments(List<ScoredAlignment> candidates);

        public List<Alignment> SelectCandidates(int n);

        public Alignment SelectCandidate();
    }
}
