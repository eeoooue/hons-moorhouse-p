using LibAlignment;
using LibAlignment.Aligners.PopulationBased;
using LibBioInfo;
using LibFileIO;
using LibScoring;
using LibScoring.ObjectiveFunctions;
using LibScoring.ScoringMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness;
using TestsHarness.Tools;

namespace TestsUnitSuite.LibAlignment
{
    [TestClass]
    public class GeneticAlgorithmAlignerTests
    {
        ExampleSequences ExampleSequences = Harness.ExampleSequences;
        SequenceConservation SequenceConservation = Harness.SequenceConservation;
        SequenceEquality SequenceEquality = Harness.SequenceEquality;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        [TestMethod]
        public void HasPopulationOfAlignments()
        {
            List<BioSequence> inputs = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            GeneticAlgorithmAligner aligner = GetAligner();
            aligner.PopulationSize = 6;
            aligner.Initialize(inputs);

            Assert.IsTrue(aligner.Population.Count == 6);
        }

        [TestMethod]
        public void PopulationMembersDiffer()
        {
            List<BioSequence> inputs = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            GeneticAlgorithmAligner aligner = GetAligner();
            aligner.PopulationSize = 2;
            aligner.Initialize(inputs);

            Assert.IsTrue(aligner.Population.Count == 2);

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(aligner.Population[0], aligner.Population[1]);
            Assert.IsFalse(alignmentsMatch);

        }



        [TestMethod]
        [Timeout(5000)]

        public void AlignerModifiesAlignment()
        {
            List<BioSequence> inputs = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            GeneticAlgorithmAligner aligner = GetAligner();
            aligner.Initialize(inputs);
            Alignment initial = aligner.Population[0].GetCopy();
            for (int i = 0; i < 3; i++)
            {
                aligner.Iterate();
            }

            Alignment result = aligner.Population[0].GetCopy();

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(initial, result);
            Assert.IsFalse(alignmentsMatch);

            AlignmentConservation.AssertAlignmentsAreConserved(initial, result);
        }

        public GeneticAlgorithmAligner GetAligner()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsObjectiveFunction(matrix);
            GeneticAlgorithmAligner aligner = new GeneticAlgorithmAligner(objective, 10);
            return aligner;
        }

    }
}
