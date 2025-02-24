using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public class Bioinformatics
    {
        public HashSet<char> DNAResidues = new HashSet<char> { 'A', 'C', 'G', 'T' };
        public HashSet<char> RNAResidues = new HashSet<char> { 'A', 'C', 'G', 'U' };
        public HashSet<char> ProteinResidues = new HashSet<char>();
        public HashSet<char> GapCharacters = new HashSet<char> { '-', '.' };

        public static HashSet<char> GapChars = new HashSet<char> { '-', '.' };


        public Bioinformatics()
        {
            foreach (char c in "CSTAGPDEQNHRKMILVWYF")
            {
                ProteinResidues.Add(c);
            }
        }

        public static bool IsGap(char c)
        {
            return GapChars.Contains(c);
        }

        public bool IsGapChar(char c)
        {
            return GapCharacters.Contains(c);
        }

        public bool IsDNAChar(char residue)
        {
            return DNAResidues.Contains(residue);
        }

        public bool IsRNAChar(char residue)
        {
            return RNAResidues.Contains(residue);
        }

        public bool IsNucleicChar(char residue)
        {
            return IsDNAChar(residue) || IsRNAChar(residue);
        }

        public bool IsProteinChar(char residue)
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
