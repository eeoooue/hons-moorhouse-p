using LibScoring;
using LibScoring.ObjectiveFunctions;
using LibScoring.ScoringMatrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness.Tools;
using TestsHarness;
using LibBioInfo;
using LibBioInfo.IAlignmentModifiers;
using LibAlignment.Helpers;

namespace TestsUnitSuite.LibAlignment.Helper
{
    [TestClass]
    public class AlignmentSelectionHelperTests
    {
        ExampleSequences ExampleSequences = Harness.ExampleSequences;
        SequenceConservation SequenceConservation = Harness.SequenceConservation;
        SequenceEquality SequenceEquality = Harness.SequenceEquality;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        AlignmentSelectionHelper Helper = new AlignmentSelectionHelper();

        [TestMethod]
        [Ignore]
        public void CanSortAlignmentsInDescendingOrder()
        {
            BLOSUM62Matrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsWithAffineGapPenaltiesObjectiveFunction(matrix, 4, 1);

            IAlignmentModifier randomizer = new AlignmentRandomizer();

            List<ScoredAlignment> examples = new List<ScoredAlignment>();
            for(int i=0; i<10; i++)
            {
                Alignment alignment = Harness.ExampleAlignments.GetExampleA();
                randomizer.ModifyAlignment(alignment);
                double score = objective.ScoreAlignment(alignment);
                ScoredAlignment scored = new ScoredAlignment(alignment, score);
                examples.Add(scored);
            }

            Helper.SortScoredAlignments(examples);

            double previous = double.MaxValue;

            foreach(ScoredAlignment alignment in examples)
            {
                Assert.IsTrue(previous >= alignment.Score);
                previous = alignment.Score;
            }
        }
    }
}
