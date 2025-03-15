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
        private static HashSet<char> GapChars = new HashSet<char> { '-', '.' };

        public static bool IsGap(char c)
        {
            return GapCharacters.Contains(c);
        }

        public static bool IsGapChar(char c)
        {
            return GapCharacters.Contains(c);
        }
    }
}
