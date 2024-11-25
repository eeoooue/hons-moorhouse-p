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
using TestsUnitSuite.HarnessTools;

namespace TestsUnitSuite.LibScoring.ObjectiveFunctions
{
    [TestClass]
    public class SumOfPairsObjectiveTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;
        FileHelper FileHelper = new FileHelper();

        #region Legacy and new scoring is equivalent

        [DataTestMethod]
        [DataRow("BB11001", 0.05)]
        [DataRow("BB11002", 0.05)]
        [DataRow("BB11003", 0.05)]
        [DataRow("BB11001", 0.01)]
        [DataRow("BB11002", 0.01)]
        [DataRow("BB11003", 0.01)]
        [Timeout(500)]
        public void NewScoringAgreesWithLegacy(string filename, double tolerance)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            IScoringMatrix matrix = new IdentityMatrix();
            SumOfPairsObjectiveFunction function = new SumOfPairsObjectiveFunction(matrix);

            for(int j=0; j<alignment.Width; j++)
            {
                double score1 = function.ScoreColumn(alignment, j);
                double score2 = function.LegacyScoreColumn(alignment, j);
                Assert.AreEqual(score1, score2, tolerance);
            }
            
        }

        #endregion

        #region Is Time Efficient

        [DataTestMethod]
        [DataRow("BB11001", 8)]
        [DataRow("BB11001", 16)]
        [DataRow("BB11001", 32)]
        [DataRow("BB11001", 64)]
        [DataRow("BB11001", 128)]
        [Timeout(500)]
        public void CanScoreBBSAlignmentsEfficiently(string filename, int times)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);

            for (int i = 0; i < times; i++)
            {
                IScoringMatrix matrix = new IdentityMatrix();
                IObjectiveFunction function = new SumOfPairsObjectiveFunction(matrix);
                double score = function.ScoreAlignment(alignment);
            }
        }

        [DataTestMethod]
        [DataRow("1ggxA_1h4uA", 8)]
        [DataRow("1ggxA_1h4uA", 16)]
        [DataRow("1ggxA_1h4uA", 32)]
        [DataRow("1ggxA_1h4uA", 64)]
        [Timeout(5000)]
        public void CanScorePREFABAlignmentsEfficiently(string filename, int times)
        {
            List<Alignment> result = new List<Alignment>();

            List<BioSequence> sequences = FileHelper.ReadSequencesFrom(filename);
            Alignment alignment = new Alignment(sequences);
            IScoringMatrix matrix = new IdentityMatrix();
            IObjectiveFunction function = new SumOfPairsObjectiveFunction(matrix);

            for (int i = 0; i < times; i++)
            {
                double score = function.ScoreAlignment(alignment);
            }
        }

        #endregion

        [TestMethod]
        public void CanInstantiateObjectiveWithIdentityMatrix()
        {
            IScoringMatrix matrix = new IdentityMatrix();
            IObjectiveFunction function = new SumOfPairsObjectiveFunction(matrix);
        }

        [TestMethod]
        public void CanInstantiateObjectiveWithBLOSUM26Matrix()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction function = new SumOfPairsObjectiveFunction(matrix);
        }

        [TestMethod]
        public void CanScoreAlignment()
        {
            Alignment alignment = ExampleAlignments.GetAlignment(ExampleAlignment.ExampleA);
            IScoringMatrix matrix = new IdentityMatrix();
            IObjectiveFunction function = new SumOfPairsObjectiveFunction(matrix);
            double score = function.ScoreAlignment(alignment);
        }

        [TestMethod]
        public void CanScoreAlignmentColumn()
        {
            Alignment alignment = ExampleAlignments.GetAlignment(ExampleAlignment.ExampleA);
            IScoringMatrix matrix = new IdentityMatrix();
            SumOfPairsObjectiveFunction function = new SumOfPairsObjectiveFunction(matrix);
            double score = function.ScoreColumn(alignment, 0);
        }
    }
}
