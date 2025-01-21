using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO.AlignmentReaders
{
    public class FastaReader : IAlignmentReader
    {
        public string Directory = "";

        public Alignment ReadAlignmentFrom(string filename)
        {
            List<BioSequence> sequences = ReadSequencesFrom(filename);
            return new Alignment(sequences, true);
        }

        public List<BioSequence> ReadSequencesFrom(string filename)
        {
            List<string> contents = File.ReadAllLines(filename).ToList();
            return UnpackSequences(contents);
        }

        public List<BioSequence> UnpackSequences(List<string> contents)
        {
            List<int> identifierIndexes = CollectIdentifierLocations(contents);
            identifierIndexes.Add(contents.Count); // including last line as an end point

            List<BioSequence> result = new List<BioSequence>();

            for (int s = 0; s < identifierIndexes.Count - 1; s++)
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
            List<int> result = new List<int>();

            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i].StartsWith(">"))
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public BioSequence ParseAsSequence(List<string> contents)
        {
            string identifier = contents[0].Substring(1);

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < contents.Count; i++)
            {
                sb.Append(contents[i]);
            }

            string payload = sb.ToString();

            return new BioSequence(identifier, payload);
        }
    }
}
