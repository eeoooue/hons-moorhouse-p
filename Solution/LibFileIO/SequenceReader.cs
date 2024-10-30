using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO
{
    internal class SequenceReader
    {
        public string Directory = "";

        public List<BioSequence> ReadSequencesFrom(string filename)
        {
            List<string> contents = File.ReadAllLines(filename).ToList();

            List<int> identifierIndexes = CollectIdentifierLocations(contents);
            identifierIndexes.Add(contents.Count); // including last line as an end point

            List<BioSequence> result = new List<BioSequence>();

            for(int s=0; s<identifierIndexes.Count - 1; s++)
            {
                int i = identifierIndexes[s];
                int j = identifierIndexes[s + 1];
                List<string> sequenceLines = contents.GetRange(i, j - i);
                BioSequence sequence = ParseAsSequence(sequenceLines);
                result.Add(sequence);
            }

            return result;
        }

        public List<int> CollectIdentifierLocations(List<string> contents)
        {
           throw new NotImplementedException();
        }

        public BioSequence ParseAsSequence(List<string> contents)
        {
            throw new NotImplementedException();
        }
    }
}
