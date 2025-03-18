using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public static class Bioinformatics
    {
        public const char GapCharacter = '-';

        private static HashSet<char> DNAResidues = new HashSet<char> { 'A', 'C', 'G', 'T' };
        private static HashSet<char> RNAResidues = new HashSet<char> { 'A', 'C', 'G', 'U' };
        private static HashSet<char> ProteinResidues = "CSTAGPDEQNHRKMILVWYF".ToHashSet();

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
