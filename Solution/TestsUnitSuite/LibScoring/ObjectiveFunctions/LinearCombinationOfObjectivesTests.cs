using LibBioInfo;
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

namespace TestsUnitSuite.LibScoring.ObjectiveFunctions
{
    [TestClass]
    public class LinearCombinationOfObjectivesTests
    {

        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;

        [DataTestMethod]
        [DataRow(1.0, 1.0, 1.0)]
        [DataRow(1.0, 1.0, -1.0)]
        [DataRow(0.3, 0.2, 0.1)]
        [DataRow(3.0, 2.0, 1.0)]
        public void CombinesObjectivesUsingWeights(double weightA, double weightB, double weightC)
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objA = new SumOfPairsObjectiveFunction(matrix);
            IObjectiveFunction objB = new TotallyConservedColumnsObjectiveFunction();
            IObjectiveFunction objC = new AffineGapPenaltyObjectiveFunction();

            List<IObjectiveFunction> objectives = new List<IObjectiveFunction>() { objA, objB, objC };
            List<double> weights = new List<double> { weightA, weightB, weightC };

            Alignment alignment = ExampleAlignments.GetExampleA();

            double expected = 0;
            for (int i=0; i<3; i++)
            {
                IObjectiveFunction objective = objectives[i];
                double weight = weights[i];
                double score = objective.ScoreAlignment(alignment);
                double contribution = weight * score;
                expected += contribution;
            }

            LinearCombinationOfObjectiveFunctions combo = new LinearCombinationOfObjectiveFunctions(objectives, weights);
            double actual = combo.ScoreAlignment(alignment);

            Assert.AreEqual(expected, actual, 0.001);
        }



    }
}
