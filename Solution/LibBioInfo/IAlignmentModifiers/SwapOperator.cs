using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.IAlignmentModifiers
{
    public enum SwapDirection
    {
        Left,
        Right
    }
    
    public class SwapOperator : IAlignmentModifier
    {
        public void ModifyAlignment(Alignment alignment)
        {
            int i = Randomizer.Random.Next(alignment.Height);
            int j = Randomizer.Random.Next(alignment.Width);
            int k = Randomizer.Random.Next(1, alignment.Width / 2);
            
            if (Randomizer.CoinFlip())
            {
                Swap(alignment, i, j, k, SwapDirection.Left);
            }
            else
            {
                Swap(alignment, i, j, k, SwapDirection.Right);
            }

            alignment.CharacterMatrixIsUpToDate = false;
            alignment.CheckResolveEmptyColumns();
        }

        public void Swap(Alignment alignment, int i, int j, int k, SwapDirection direction)
        {
            // affects ith sequence only
            // from column j, 

            if (direction == SwapDirection.Left)
            {
                SwapLeft(alignment, i, j, k);
            }
            else
            {
                SwapRight(alignment, i, j, k);
            }
        }

        public void SwapLeft(Alignment alignment, int i, int j, int k)
        {
            bool[,] mirrored = GetHorizontallyMirroredState(alignment.State);
            alignment.SetState(mirrored);
            SwapRight(alignment, i, j, k);
            bool[,] newState = GetHorizontallyMirroredState(alignment.State);
            alignment.SetState(newState);
        }

        public bool[,] GetHorizontallyMirroredState(bool[,] state)
        {
            int m = state.GetLength(0);
            int n = state.GetLength(1);

            bool[,] result = new bool[m, n];

            for(int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    int j2 = -1 + n - j;
                    result[i, j] = state[i, j2];
                }
            }

            return result;
        }

        public void SwapRight(Alignment alignment, int i, int j, int k)
        {
            int residuesRemoved = DeleteUpToNResiduesRightwards(alignment, i, j, k);
            PlaceNResiduesRightwards(alignment, i, j, residuesRemoved);
            alignment.CharacterMatrixIsUpToDate = false;
        }

        public void PrintAlignmentStateRow(Alignment alignment, int i)
        {
            for(int j=0; j<alignment.Width; j++)
            {
                char x = alignment.State[i, j] ? '-' : 'X';
                Console.Write($"{x}");
            }
            Console.WriteLine();
        }

        public void PlaceNResiduesRightwards(Alignment alignment, int i, int startPos, int count)
        {
            // Console.WriteLine($"Placing {count} residues, starting at {startPos} ");
            if (count == 0)
            {
                return;
            }

            for(int j=startPos; j<alignment.Width; j++)
            {
                if (alignment.State[i, j])
                {
                    alignment.State[i, j] = false;
                    count--;
                    // Console.WriteLine($"Placed residue @ {j} ; {count} residues remain ");

                    if (count == 0)
                    {
                        return;
                    }
                }
            }
        }

        public int DeleteUpToNResiduesRightwards(Alignment alignment, int i, int startPos, int k)
        {
            int counter = 0;
            // Console.WriteLine($"starting at {startPos} for alignment of width = {alignment.Width}, with goal of {k}");

            for(int j=startPos; j<alignment.Width; j++)
            {
                if (alignment.State[i, j] == false)
                {
                    alignment.State[i, j] = true;
                    counter++;

                    // Console.WriteLine($"deleted {counter}th residue.");

                    if (counter == k)
                    {
                        return counter;
                    }
                }
            }

            return counter;
        }

        
    }
}
