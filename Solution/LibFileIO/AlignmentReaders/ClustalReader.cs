using LibBioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFileIO.AlignmentReaders
{
    public class ClustalReader : IAlignmentReader
    {
        public string Directory = "";

        public bool FileAppearsReadable(string filename)
        {
            List<string> contents = File.ReadAllLines(filename).ToList();
            string line = contents[0].ToUpper().Trim();
            return line.StartsWith("CLUSTAL");
        }

        public Alignment ReadAlignmentFrom(string filename)
        {
            List<BioSequence> sequences = ReadSequencesFrom(filename);
            return new Alignment(sequences, true);
        }

        public List<BioSequence> ReadSequencesFrom(string filename)
        {
            List<string> contents = File.ReadAllLines(filename).ToList();
            List<BioSequence> result = UnpackAlignment(contents);

            return result;
        }

        public List<BioSequence> UnpackAlignment(List<string> contents)
        {
            List<string> sequenceContents = RemoveClustalHeader(contents);
            List<string> identifiers = CollectUniqueIdentifiers(sequenceContents);

            Dictionary<string, StringBuilder> builders = InitializeStringBuilders(identifiers);
            foreach (string line in sequenceContents)
            {
                TryExtractPayloadFromLine(builders, line);
            }

            return ConstructSequences(builders, identifiers);
        }

        public List<string> RemoveClustalHeader(List<string> contents)
        {
            return contents.GetRange(1, contents.Count - 1);
        }

        public List<string> CollectUniqueIdentifiers(List<string> sequenceContents)
        {
            HashSet<string> identifiers = new HashSet<string>();

            foreach (string line in sequenceContents)
            {
                string identifier = ExtractIdentifier(line);
                if (identifier.Length > 0)
                {
                    identifiers.Add(identifier);
                }
            }

            return identifiers.ToList();
        }

        public string ExtractIdentifier(string line)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in line)
            {
                if (c == ' ')
                {
                    break;
                }
                sb.Append(c);
            }
            return sb.ToString();
        }

        public Dictionary<string, StringBuilder> InitializeStringBuilders(List<string> identifiers)
        {
            Dictionary<string, StringBuilder> result = new Dictionary<string, StringBuilder>();
            foreach (string identifier in identifiers)
            {
                result[identifier] = new StringBuilder();
            }

            return result;
        }

        public void TryExtractPayloadFromLine(Dictionary<string, StringBuilder> builders, string line)
        {
            string identifier = ExtractIdentifier(line);

            if (builders.ContainsKey(identifier))
            {
                string payload = TrimOffIdentifier(identifier, line);
                StringBuilder sb = builders[identifier];
                sb.Append(payload);
            }
        }

        public string TrimOffIdentifier(string identifier, string line)
        {
            return line.Substring(identifier.Length).Trim();
        }

        public List<BioSequence> ConstructSequences(Dictionary<string, StringBuilder> builders, List<string> identifiers)
        {
            List<BioSequence> result = new List<BioSequence>();

            foreach (string identifier in identifiers)
            {
                StringBuilder sb = builders[identifier];
                string payload = sb.ToString();
                payload = FileConventions.ReplaceForeignGapCharactersInPayload(payload);

                BioSequence sequence = new BioSequence(identifier, payload);
                result.Add(sequence);
            }

            return result;
        }

    }
}
