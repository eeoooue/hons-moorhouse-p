using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    internal class Bioinformatics
    {
        public HashSet<char> NucleicResidues = new HashSet<char>();
        public HashSet<char> ProteinResidues = new HashSet<char>();
        public HashSet<char> GapCharacters = new HashSet<char> { '-', '.' };

        public Bioinformatics()
        {
            foreach (char c in "ACGTU")
            {
                NucleicResidues.Add(c);
            }

            foreach (char c in "CSTAGPDEQNHRKMILVWYF")
            {
                ProteinResidues.Add(c);
            }
        }

        public bool IsGapChar(char c)
        {
            return GapCharacters.Contains(c);
        }

        public bool IsNucleicChar(char residue)
        {
            return NucleicResidues.Contains(residue);
        }

        public bool IsProteinChar(char residue)
        {
            return ProteinResidues.Contains(residue);
        }
    }
}
