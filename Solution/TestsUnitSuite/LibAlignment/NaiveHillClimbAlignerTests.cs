﻿using LibAlignment.Aligners;
using LibAlignment;
using LibBioInfo;
using LibScoring.ObjectiveFunctions;
using LibScoring.ScoringMatrices;
using LibScoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibFileIO;

using TestsHarness;
using TestsHarness.Tools;
using LibAlignment.Aligners.SingleState;

namespace TestsUnitSuite.LibAlignment
{
    [TestClass]
    public class NaiveHillClimbAlignerTests
    {
        ExampleSequences ExampleSequences = Harness.ExampleSequences;
        SequenceConservation SequenceConservation = Harness.SequenceConservation;
        SequenceEquality SequenceEquality = Harness.SequenceEquality;
        AlignmentEquality AlignmentEquality = Harness.AlignmentEquality;
        AlignmentConservation AlignmentConservation = Harness.AlignmentConservation;

        public IterativeAligner GetAligner()
        {
            IScoringMatrix matrix = new BLOSUM62Matrix();
            IObjectiveFunction objective = new SumOfPairsObjectiveFunction(matrix);
            const int maxIterations = 50;
            return new NaiveHillClimbAligner(objective, maxIterations);
        }

        [TestMethod]
        [Timeout(5000)]
        [Ignore]

        public void AlignerModifiesAlignment()
        {
            List<BioSequence> inputs = new List<BioSequence>
            {
                ExampleSequences.GetSequence(ExampleSequence.ExampleA),
                ExampleSequences.GetSequence(ExampleSequence.ExampleB),
                ExampleSequences.GetSequence(ExampleSequence.ExampleC),
                ExampleSequences.GetSequence(ExampleSequence.ExampleD),
            };

            IterativeAligner climber = GetAligner();
            climber.Initialize(inputs);
            Alignment initial = climber.CurrentAlignment!.GetCopy();

            for (int i = 0; i < 10; i++)
            {
                climber.Iterate();
            }

            Alignment result = climber.CurrentAlignment!;

            bool alignmentsMatch = AlignmentEquality.AlignmentsMatch(initial, result);
            Assert.IsFalse(alignmentsMatch);

            AlignmentConservation.AssertAlignmentsAreConserved(initial, result);
        }
    }
}
