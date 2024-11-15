using LibBioInfo;
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
