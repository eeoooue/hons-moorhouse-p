using LibBioInfo;
using LibBioInfo.PairwiseAligners;
using LibModification.Helpers;
using LibSimilarity;
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
        PairwiseAlignmentHelper PairwiseAlignmentHelper = new PairwiseAlignmentHelper();

        public override char[,] GetModifiedAlignmentState(Alignment alignment)
        {
            //if (Randomizer.CoinFlip())
            //{
            //    int extent = Randomizer.Random.Next(alignment.Width);
            //    CharMatrixHelper.SprinkleEmptyColumnsIntoAlignment(alignment, extent);
            //}

            int i;
            int j;
            PickPairOfSequences(alignment, out i, out j);
            return AlignPairOfSequences(alignment, i, j);
        }

        public char[,] AlignPairOfSequences(Alignment alignment, int i, int j)
        {
            string seqAresidues;
            string seqBresidues;
            PairwiseAlignmentHelper.CollectSequenceResidues(alignment, i, j, out seqAresidues, out seqBresidues);

            string newSequenceALayout;
            string newSequenceBLayout;
            PairwiseAlignmentHelper.GetNewSequenceLayout(alignment, in seqAresidues, in seqBresidues, out newSequenceALayout, out newSequenceBLayout);

            char[,] result = GetExpandedCanvas(alignment.CharacterMatrix, i, newSequenceALayout);
            ReplaceRowWithAlignment(ref result, newSequenceALayout, newSequenceBLayout, i, j);

            return CharMatrixHelper.RemoveEmptyColumns(in result);
        }

        public void ReplaceRowWithAlignment(ref char[,] matrix, string anchor, string aligned, int i, int j)
        {
            CharMatrixHelper.ClearAlignmentRow(ref matrix, j);

            int n = matrix.GetLength(1);

            int cursor = 0;
            for(int p = 0; p<n; p++)
            {
                if (cursor >= anchor.Length)
                {
                    return;
                }

                char target = anchor[cursor];
                char read = matrix[i, p];

                if (read == target)
                {
                    matrix[j, p] = aligned[cursor];
                    cursor++;
                }
            }
        }

        public char[,] GetExpandedCanvas(char[,] matrix, int i, string newPayload)
        {
            string originalPayload = CharMatrixHelper.GetCharRowAsString(matrix, i);
            List<int> recipe = GetCanvasRecipe(originalPayload, newPayload);

            int m = matrix.GetLength(0);
            int n = recipe.Count;

            char[,] result = new char[m, n];

            for(int destIndex=0; destIndex < n; destIndex++)
            {
                int sourceIndex = recipe[destIndex];
                if (sourceIndex == -1)
                {
                    FillColumnWithGaps(ref result, destIndex);
                }
                else
                {
                    CopyColumnAcross(in matrix, ref result, sourceIndex, destIndex);
                }
            }

            return result;
        }

        public void FillColumnWithGaps(ref char[,] destination, int destIndex)
        {
            int m = destination.GetLength(0);
            for(int i=0; i<m; i++)
            {
                destination[i, destIndex] = '-';
            }
        }

        public void CopyColumnAcross(in char[,] source, ref char[,] destination, int sourceIndex, int destIndex)
        {
            int m = destination.GetLength(0);
            for (int i = 0; i < m; i++)
            {
                destination[i, destIndex] = source[i, sourceIndex];
            }
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
