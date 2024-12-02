using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.ICrossoverOperators
{
    public class SAGAOnePointCrossoverOperator : ICrossoverOperator
    {
        public List<Alignment> CreateAlignmentChildren(Alignment a, Alignment b)
        {
            throw new NotImplementedException();
        }

        public List<Alignment> CrossoverAtPosition(Alignment a, Alignment b, int position)
        {
            throw new NotImplementedException();
        }

        public bool[,] GetVerticalSplitLeft(Alignment a, int position)
        {
            int m = a.Height;
            int n = position;

            bool[,] result = new bool[a.Height, position];

            List<int> positions = new List<int>();

            for(int i=0; i<m; i++)
            {
                positions.Add(position);
            }

            return CollectLeftsUntilPositions(a.State, positions);
        }

        public bool[,] GetVerticalSplitRight(Alignment a, int position)
        {
            throw new NotImplementedException();
        }


        public bool[,] CollectLeftsUntilPositions(bool[,] source, List<int> positions)
        {
            int maxLeftPosition = GetMaximumValue(positions);
            int m = positions.Count;
            int n = maxLeftPosition;

            bool[,] result = new bool[m, n];
            for(int i=0; i<m; i++)
            {
                WriteLeftsUntilPosition(source, result, i, positions[i]);
            }

            return result;
        }

        public void WriteLeftsUntilPosition(bool[,] source, bool[,] destination, int i, int n)
        {
            for(int j=0; j<n; j++)
            {
                destination[i, j] = source[i, j];
            }
        }



        public bool[,] CollectRightsUntilPositions(bool[,] source, List<int> positions)
        {
            throw new NotImplementedException();
        }

        public void WriteRightsUntilPosition(bool[,] source, bool[,] destination, int i, int n)
        {
            throw new NotImplementedException();
        }


        public int GetMaximumValue(List<int> values)
        {
            int result = int.MinValue;
            foreach(int value in values)
            {
                result = Math.Max(value, result);
            }

            return result;
        }

        public int GetMinimumValue(List<int> values)
        {
            int result = int.MaxValue;
            foreach (int value in values)
            {
                result = Math.Min(value, result);
            }

            return result;
        }

        public bool[,] GetRightHandSide(Alignment alignment, bool[,] leftSide)
        {
            throw new NotImplementedException();


            List<int> residuesToSkip = CountResiduesPerRow(leftSide);
            List<int> starterPositions = CollectRightHandStarterPositions(alignment.State, residuesToSkip);

            int leftMostPosition = alignment.Width - 1;
            foreach(int x in starterPositions)
            {
                leftMostPosition = Math.Min(x, leftMostPosition);
            }

            int m = alignment.Height;

            bool[,] result = new bool[m, alignment.Width - leftMostPosition];

            for(int i=0; i<m; i++)
            {
                int starterPosition = starterPositions[i];

                for(int j=starterPosition; j< alignment.Width; j++)
                {
                    int j2 = j - leftMostPosition;
                    result[i, j2] = alignment.State[i, j];
                }
            }


            return result;
        }


        public List<int> CollectRightHandStarterPositions(bool[,] state, List<int> residuesToSkip)
        {
            List<int> result = new List<int>();


            for(int i=0; i<state.GetLength(0); i++)
            {
                int target = residuesToSkip[i];
                int j = 0;

                while (target > 0)
                {
                    if (state[i, j] == false)
                    {
                        target -= 1;
                    }
                    j += 1;
                }

                result.Add(j);

            }

            return result;

        }


        public List<int> CountResiduesPerRow(bool[,] state)
        {
            List<int> result = new List<int>();

            for(int i=0; i<state.GetLength(0); i++)
            {
                int total = 0;

                for(int j=0; j<state.GetLength(1); j++)
                {
                    if (state[i, j] == false)
                    {
                        total++;
                    }
                }

                result.Add(total);
            }

            return result;
        }
    }
}
