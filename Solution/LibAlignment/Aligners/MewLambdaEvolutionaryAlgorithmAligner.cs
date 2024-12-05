using LibBioInfo.ICrossoverOperators;
using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibScoring;

namespace LibAlignment.Aligners
{
    public class MewLambdaEvolutionaryAlgorithmAligner : Aligner
    {
        public List<Alignment> Population = new List<Alignment>();


        public MewLambdaEvolutionaryAlgorithmAligner(IObjectiveFunction objective, int iterations) : base(objective, iterations)
        {
        }

        public override Alignment AlignSequences(List<BioSequence> sequences)
        {
            throw new NotImplementedException();
        }

        public override void Initialize(List<BioSequence> sequences)
        {
            throw new NotImplementedException();
        }

        public override void Iterate()
        {
            throw new NotImplementedException();
        }
    }
}
