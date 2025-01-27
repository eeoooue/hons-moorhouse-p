using LibBioInfo;
using LibBioInfo.PairwiseAligners;
using LibModification.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.AlignmentModifiers
{
    public class HeuristicPairwiseModifier : AlignmentModifier
    {
        CharMatrixHelper CharMatrixHelper = new CharMatrixHelper();
        BiosequencePayloadHelper PayloadHelper = new BiosequencePayloadHelper();
        Bioinformatics Bioinformatics = new Bioinformatics();

        public HeuristicPairwiseModifier()
        {

        }

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            int i;
            int j;
            PickPairOfSequences(alignment, out i, out j);

            string seqAresidues;
            string seqBresidues;
            CollectSequenceResidues(alignment, i, j, out seqAresidues, out seqBresidues);

            string newSequenceALayout;
            string newSequenceBLayout;
            GetNewSequenceLayout(in seqAresidues, in seqBresidues, out newSequenceALayout, out newSequenceBLayout);

            throw new NotImplementedException();
        }

        public char[,] GetExpandedCanvas(Alignment alignment, int i, string anchorAlignment)
        {
            throw new NotImplementedException();
        }

        public List<int> GetCanvasRecipe(string originalPayload, string newPayload)
        {
            List<int> originalDistances = CollectDistancesBetweenResidues(originalPayload);
            List<int> suggestedDistances = CollectDistancesBetweenResidues(newPayload);
            List<int> derivedRecipe = new List<int>();

            int n = originalDistances.Count;

            int p = 0;
            for(int i=0; i<n; i++)
            {
                for(int j = 0; j< originalDistances[i]; j++)
                {
                    int index = p++;
                    derivedRecipe.Add(index);
                }

                int difference = Math.Max(0, suggestedDistances[i] - originalDistances[i]);
                for(int j = 0; j<difference; j++)
                {
                    derivedRecipe.Add(-1);
                }

                if (i < n - 1)
                {
                    derivedRecipe.Add(p++);
                }
            }

            return derivedRecipe;
        }


        public List<int> CollectDistancesBetweenResidues(string payload)
        {
            List<int> result = new List<int>();

            int distance = 0;
            for(int i=0; i<payload.Length; i++)
            {
                char x = payload[i];
                if (Bioinformatics.IsGapChar(x))
                {
                    distance++;
                }
                else
                {
                    result.Add(distance);
                    distance = 0;
                }
            }

            result.Add(distance);

            return result;
        }





        public void CollectSequenceResidues(Alignment alignment, int i, int j, out string residuesA, out string residuesB)
        {
            string sequenceA = CharMatrixHelper.GetCharRowAsString(alignment.CharacterMatrix, i);
            string sequenceB = CharMatrixHelper.GetCharRowAsString(alignment.CharacterMatrix, j);
            residuesA = PayloadHelper.ExtractResiduesFromString(sequenceA);
            residuesB = PayloadHelper.ExtractResiduesFromString(sequenceB);
        }

        public void GetNewSequenceLayout(in string residuesA, in string residuesB, out string layoutA, out string layoutB)
        {
            StringBuilder alignedSeqAbuilder = new StringBuilder();
            StringBuilder alignedSeqBbuilder = new StringBuilder();
            NeedlemanWunschPairwiseAligner aligner = new NeedlemanWunschPairwiseAligner(residuesA, residuesB);
            aligner.ExtractPairwiseAlignment(ref alignedSeqAbuilder, ref alignedSeqBbuilder);
            layoutA = alignedSeqAbuilder.ToString();
            layoutB = alignedSeqBbuilder.ToString();
        }

        public void PickPairOfSequences(Alignment alignment, out int i, out int j)
        {
            i = Randomizer.Random.Next(alignment.Height);
            j = i;
            while (i == j)
            {
                j = Randomizer.Random.Next(alignment.Height);
            }
        }
    }
}
