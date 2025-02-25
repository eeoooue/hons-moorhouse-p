using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibBioInfo
{
    public class BioSequence
    {
        public string Identifier { get; private set; }
        public string Payload { get; private set; }
        public string Residues { get; private set; }

        public BioSequence(string identifier, string payload)
        {
            Identifier = identifier;
            Payload = payload.ToUpper();
            Residues = ExtractResidues();
        }

        public bool IsNucleic()
        {
            foreach(char c in Residues)
            {
                if (!Bioinformatics.IsNucleicChar(c))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsProtein()
        {
            foreach (char c in Residues)
            {
                if (!Bioinformatics.IsProteinChar(c))
                {
                    return false;
                }
            }

            return true;
        }

        private string ExtractResidues()
        {
            StringBuilder sb = new StringBuilder();
            foreach(char c in Payload)
            {
                if (Bioinformatics.IsGapChar(c))
                {
                    continue;
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public bool PayloadIsValid()
        {
            return IsNucleic() || IsProtein();
        }
    }
}
