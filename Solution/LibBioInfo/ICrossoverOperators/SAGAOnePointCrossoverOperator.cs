using LibBioInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo.ICrossoverOperators
{
    public class SAGAOnePointCrossoverOperator : ICrossoverOperator
    {
        // an attempt to reproduce the One-Point Crossover operation described in SAGA (Notredame & Higgins, 1996)

        private AlignmentStateHelper StateHelper = new AlignmentStateHelper();

        public List<Alignment> CreateAlignmentChildren(Alignment a, Alignment b)
        {
            int heads = Randomizer.Random.Next(0, 2);

            if (heads == 1)
            {
                int i = Randomizer.Random.Next(2, a.Width);
                Console.WriteLine($"Performing A-B crossover @ position = {i}");
                return CrossoverAtPosition(a, b, i);
            }
            else
            {
                int i = Randomizer.Random.Next(2, b.Width);
                Console.WriteLine($"Performing B-A crossover @ position = {i}");
                return CrossoverAtPosition(b, a, i);
            }
        }

        public List<Alignment> CrossoverAtPosition(Alignment a, Alignment b, int position)
        {
            bool canPerform = true;

            if (position <= 0)
            {
                canPerform = false;
            }

            if (position >= a.Width || position >= b.Width)
            {
                canPerform = false;
            }

            if (!canPerform)
            {
                return new List<Alignment> { a, b, };
            }

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

            bool[,] childState = StateHelper.CombineAlignmentStates(aLeftState, bRightState);

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

            bool[,] childState = StateHelper.CombineAlignmentStates(bLeftState, aRightState);

            Alignment child = a.GetCopy();
            child.State = childState;

            return child;
        }

        public List<int> GetComplementPositionsForLeftState(bool[,] leftState, Alignment source)
        {
            List<int> result = new List<int>();

            for(int i=0; i<source.Height; i++)
            {
                int n = StateHelper.GetNumberOfResiduesInRow(leftState, i);
                int position = StateHelper.GetPositionOfNthResidue(source.State, i, n);
                result.Add(position);
            }

            return result;
        }

        public List<int> GetComplementPositionsForRightState(bool[,] rightState, Alignment source)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < source.Height; i++)
            {
                int residuesInRightState = StateHelper.GetNumberOfResiduesInRow(rightState, i);
                int totalResidues = StateHelper.GetNumberOfResiduesInRow(source.State, i);
                int residuesToInclude = totalResidues - residuesInRightState;
                int position = StateHelper.GetPositionOfNthResidue(source.State, i, residuesToInclude);
                result.Add(position+1);
            }

            return result;
        }

        public bool[,] CollectLeftsUntilPositions(bool[,] source, List<int> positions)
        {
            int m = positions.Count;
            int n = positions.Max();

            bool[,] result = new bool[m, n];
            for(int i=0; i<m; i++)
            {
                WriteLeftsUntilPosition(source, result, i, positions[i]);
            }

            return result;
        }

        public bool[,] CollectRightsUntilPositions(bool[,] source, List<int> positions)
        {
            int maxLeftPosition = positions.Min();
            int m = positions.Count;
            int n = source.GetLength(1) - (1 + maxLeftPosition);

            bool[,] result = new bool[m, n];
            for(int i=0; i<m; i++)
            {
                WriteRightsUntilPosition(source, result, i, positions[i]);
            }

            return result;
        }

        public void WriteLeftsUntilPosition(bool[,] source, bool[,] destination, int i, int n)
        {

            int sourceLength = source.GetLength(1);

            for (int j = 0; j < destination.GetLength(1); j++)
            {
                destination[i, j] = true;
            }

            for (int j = 0; j < n; j++)
            {
                if (j < sourceLength)
                {
                    destination[i, j] = source[i, j];
                }


            }

            
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

        public List<int> GetListOfPositions(int position, int count)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < count; i++)
            {
                result.Add(position);
            }

            return result;
        }
    }
}
