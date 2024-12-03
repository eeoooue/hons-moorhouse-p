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
            int heads = Randomizer.Random.Next(0, 2);

            if (heads == 1)
            {
                int i = Randomizer.Random.Next(1, a.Width);
                return CrossoverAtPosition(a, b, i);
            }
            else
            {
                int i = Randomizer.Random.Next(1, b.Width);
                return CrossoverAtPosition(b, a, i);
            }
        }

        public List<Alignment> CrossoverAtPosition(Alignment a, Alignment b, int position)
        {
            Alignment child1 = GetABCrossover(a, b, position);
            Alignment child2 = GetBACrossover(a, b, position);
            
            return new List<Alignment> { child1, child2 };
        }

        public Alignment GetABCrossover(Alignment a, Alignment b, int position)
        {
            List<int> aLeftPositions = GetListOfPositions(position, a.Height);
            bool[,] aLeftState = CollectLeftsUntilPositions(a.State, aLeftPositions);
            List<int> bRightPositions = GetComplementPositionsForLeftState(aLeftState, b);
            bool[,] bRightState = CollectRightsUntilPositions(b.State, bRightPositions);

            bool[,] childState = CombineAlignmentStates(aLeftState, bRightState);

            Alignment child = a.GetCopy();
            child.State = childState;

            return child;
        }

        public Alignment GetBACrossover(Alignment a, Alignment b, int position)
        {
            List<int> aRightPositions = GetListOfPositions(position - 1, a.Height);
            bool[,] aRightState = CollectRightsUntilPositions(a.State, aRightPositions);
            List<int> bLeftPositions = GetComplementPositionsForRightState(aRightState, b);
            bool[,] bLeftState = CollectLeftsUntilPositions(b.State, bLeftPositions);

            bool[,] childState = CombineAlignmentStates(bLeftState, aRightState);

            Alignment child = a.GetCopy();
            child.State = childState;

            return child;
        }

        public bool[,] CombineAlignmentStates(bool[,] leftState, bool[,] rightState)
        {
            int m = leftState.GetLength(0);
            int leftN = leftState.GetLength(1);
            int rightN = rightState.GetLength(1);

            bool[,] result = new bool[m, leftN + rightN];

            for(int i=0; i<m; i++)
            {
                for(int j=0; j<leftN; j++)
                {
                    result[i, j] = leftState[i, j];
                }
                
                for (int j = 0; j < rightN; j++)
                {
                    result[i, j + leftN] = rightState[i, j];
                }
            }

            return result;
        }

        public List<int> GetComplementPositionsForLeftState(bool[,] leftState, Alignment source)
        {
            List<int> result = new List<int>();

            for(int i=0; i<source.Height; i++)
            {
                int n = GetNumberOfResiduesInRow(leftState, i);
                int position = GetPositionOfNthResidue(source.State, i, n);
                result.Add(position);
            }

            return result;
        }

        public List<int> GetComplementPositionsForRightState(bool[,] rightState, Alignment source)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < source.Height; i++)
            {
                int residuesInRightState = GetNumberOfResiduesInRow(rightState, i);
                int totalResidues = GetNumberOfResiduesInRow(source.State, i);
                int residuesToInclude = totalResidues - residuesInRightState;
                int position = GetPositionOfNthResidue(source.State, i, residuesToInclude);
                result.Add(position+1);
            }

            return result;
        }

        public int GetPositionOfNthResidue(bool[,] state, int i, int n)
        {
            int counter = 0;
            for(int j=0; j<state.GetLength(1); j++)
            {
                if (state[i, j] == false)
                {
                    counter++;
                }

                if (counter == n)
                {
                    return j;
                }
            }

            return state.GetLength(1);
        }

        public int GetNumberOfResiduesInRow(bool[,] state, int i)
        {
            int result = 0;
            for(int j = 0; j < state.GetLength(1); j++)
            {
                if (state[i,j] == false)
                {
                    result++;
                }
            }

            return result;
        }


        public List<int> GetListOfPositions(int position, int count)
        {
            List<int> result = new List<int>();
            for(int i=0; i<count; i++)
            {
                result.Add(position);
            }

            return result;
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
            int m = a.Height;
            int n = position;

            bool[,] result = new bool[a.Height, position];

            List<int> positions = new List<int>();

            for (int i = 0; i < m; i++)
            {
                positions.Add(position);
            }

            return CollectRightsUntilPositions(a.State, positions);
        }


        public bool[,] CollectLeftsUntilPositions(bool[,] source, List<int> positions)
        {
            int maxRightPosition = GetMaximumValue(positions);
            int m = positions.Count;
            int n = maxRightPosition;

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
                if (j < n)
                {
                    destination[i, j] = source[i, j];
                }
            }

            for(int j=n; j<destination.GetLength(1); j++)
            {
                destination[i, j] = true;
            }
        }



        public bool[,] CollectRightsUntilPositions(bool[,] source, List<int> positions)
        {
            int maxLeftPosition = GetMinimumValue(positions);
            int m = positions.Count;
            int n = source.GetLength(1) - (1 + maxLeftPosition);

            bool[,] result = new bool[m, n];
            for(int i=0; i<m; i++)
            {
                WriteRightsUntilPosition(source, result, i, positions[i]);
            }

            return result;
        }

        public void WriteRightsUntilPosition(bool[,] source, bool[,] destination, int i, int leftMarker)
        {
            for(int j=0; j<destination.GetLength(1); j++)
            {
                destination[i, j] = true;
            }

            int n = source.GetLength(1);
            int j2 = destination.GetLength(1);
            for(int j=n-1; j>leftMarker; j--)
            {
                j2 -= 1;
                destination[i, j2] = source[i, j];
            }
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
