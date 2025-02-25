using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public static class Bioinformatics
    {
        public static HashSet<char> DNAResidues = new HashSet<char> { 'A', 'C', 'G', 'T' };
        public static HashSet<char> RNAResidues = new HashSet<char> { 'A', 'C', 'G', 'U' };
        public static HashSet<char> ProteinResidues = GetProteinResidues();
        public static HashSet<char> GapCharacters = new HashSet<char> { '-', '.' };
        public static HashSet<char> GapChars = new HashSet<char> { '-', '.' };

        private static HashSet<char> GetProteinResidues()
        {
            HashSet<char> result = new HashSet<char>();
            foreach (char c in "CSTAGPDEQNHRKMILVWYF")
            {
                result.Add(c);
            }

            return result;
        }

        public static bool IsGap(char c)
        {
            return GapCharacters.Contains(c);
        }

        public static bool IsGapChar(char c)
        {
            return GapCharacters.Contains(c);
        }

        public static bool IsDNAChar(char residue)
        {
            return DNAResidues.Contains(residue);
        }

        public static bool IsRNAChar(char residue)
        {
            return RNAResidues.Contains(residue);
        }

        public static bool IsNucleicChar(char residue)
        {
            return IsDNAChar(residue) || IsRNAChar(residue);
        }

        public static bool IsProteinChar(char residue)
        {
            if (residue == 'X') // denotes an unknown residue
            {
                return true;
            }

            if (residue == 'B' || residue == 'Z')
            {
                return true;
            }

            return ProteinResidues.Contains(residue);
        }
    }
}
