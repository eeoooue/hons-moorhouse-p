﻿using LibBioInfo;
using LibSimilarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsHarness;
using TestsHarness.Tools;

namespace TestsUnitSuite.LibSimilarity
{
    [TestClass]
    public class SimilarityGuideTests
    {
        ExampleAlignments ExampleAlignments = Harness.ExampleAlignments;

        [TestMethod]
        public void CanGetSetOfSequences()
        {
            List<BioSequence> sequences = GetExampleSequences();
            SimilarityGuide.SetSequences(sequences);
            SimilarityGraph graph = SimilarityGuide.Graph;
            graph.RecordSimilarity(sequences[0], sequences[1], 200);

            List<BioSequence> set = SimilarityGuide.GetSetOfSimilarSequences(sequences);
            Assert.IsTrue(set.Count > 0);
        }

        public List<BioSequence> GetExampleSequences()
        {
            Alignment alignment = ExampleAlignments.GetExampleA();
            List<BioSequence> sequences = alignment.Sequences;

            return sequences;
        }
    }
}
