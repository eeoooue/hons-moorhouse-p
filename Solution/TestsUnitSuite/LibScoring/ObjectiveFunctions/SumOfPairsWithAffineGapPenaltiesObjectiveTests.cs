using LibBioInfo.IAlignmentModifiers;
using LibBioInfo;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using LibScoring.ObjectiveFunctions;
using LibScoring.ScoringMatrices;
using TestsHarness;

namespace TestsUnitSuite.LibScoring.ObjectiveFunctions
{
    [TestClass]
    public class SumOfPairsWithAffineGapPenaltiesObjectiveTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;

        public SumOfPairsWithAffineGapPenaltiesObjectiveFunction GetObjectiveFunction()
        {
            BLOSUM62Matrix matrix = new BLOSUM62Matrix();
            return new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(matrix);
        }


        [TestMethod]
        public void LeftJustifiedAlignmentHasNoPenalty()
        {
            SumOfPairsWithAffineGapPenaltiesObjectiveFunction objective = GetObjectiveFunction();

            Alignment alignment = ExampleAlignments.GetExampleA();

            double sumOfPairsScoreWithAffineGapPenalties = objective.ScoreAlignment(alignment);
            double sumOfPairsScore = objective.SumOfPairsOF.ScoreAlignment(alignment);
            double affineGapPenalty = objective.AffineGapPenaltyOF.ScoreAlignment(alignment);

            Assert.AreEqual(sumOfPairsScoreWithAffineGapPenalties, sumOfPairsScore, 0.001);
            Assert.AreEqual(sumOfPairsScoreWithAffineGapPenalties + affineGapPenalty, sumOfPairsScore, 0.001);
            Assert.AreEqual(0, affineGapPenalty, 0.001);
        }


        [TestMethod]
        public void RandomizedAlignmentHasPenalty()
        {
            SumOfPairsWithAffineGapPenaltiesObjectiveFunction objective = GetObjectiveFunction();

            Alignment alignment = ExampleAlignments.GetExampleA();
            AlignmentRandomizer randomizer = new AlignmentRandomizer();
            randomizer.ModifyAlignment(alignment);

            double sumOfPairsScoreWithAffineGapPenalties = objective.ScoreAlignment(alignment);
            double sumOfPairsScore = objective.SumOfPairsOF.ScoreAlignment(alignment);
            double affineGapPenalty = objective.AffineGapPenaltyOF.ScoreAlignment(alignment);

            Assert.IsTrue(sumOfPairsScore > sumOfPairsScoreWithAffineGapPenalties);
            Assert.AreEqual(sumOfPairsScoreWithAffineGapPenalties + affineGapPenalty, sumOfPairsScore, 0.001);
            Assert.IsTrue(affineGapPenalty > 0);
        }
    }
}
