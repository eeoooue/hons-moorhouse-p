using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO
{
    public static class FileConventions
    {
        private static HashSet<char> GapCharacters = new HashSet<char> { '-', '.' };

        public static string ReplaceForeignGapCharactersInPayload(string payload)
        {
            StringBuilder sb = new StringBuilder();
            foreach(char x in payload)
            {
                if (GapCharacters.Contains(x))
                {
                    sb.Append(Bioinformatics.GapCharacter);
                }
                else
                {
                    sb.Append(x);
                }
            }

            return sb.ToString();
        }
    }
}
