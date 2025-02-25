using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibModification.Helpers
{
    public class AlignmentStateHelper
    {
        public bool[,] ConvertMatrixFromCharToBool(in char[,] state)
        {
            int m = state.GetLength(0);
            int n = state.GetLength(1);

            bool[,] result = new bool[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = false;
                    if (Bioinformatics.IsGapChar(state[i, j]))
                    {
                        result[i, j] = true;
                    }
                }
            }

            return result;
        }

        public char[,] ConvertMatrixFromBoolToChar(List<BioSequence> sequences, in bool[,] state)
        {
            int m = state.GetLength(0);
            int n = state.GetLength(1);

            char[,] result = new char[m, n];

            for (int i = 0; i < m; i++)
            {
                string residues = sequences[i].Residues;
                int p = 0;
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = '-';
                    if (!state[i, j])
                    {
                        char x = residues[p++];
                        result[i, j] = x;
                    }
                }
            }

            return result;
        }
    }
}
