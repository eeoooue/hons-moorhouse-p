using LibAlignment;
using LibAlignment.Aligners.PopulationBased;
using LibAlignment.Aligners.SingleState;
using LibBioInfo.Metrics;
using LibBioInfo.ScoringMatrices;
using LibScoring;
using LibScoring.FitnessFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsRequirements
{
    [TestClass]
    public class Objective02
    {
        /// <summary>
        /// Employs a metaheuristic algorithm (such as Genetic Algorithm) to guide the alignment process.
        /// </summary>
        [TestMethod]
        [DoNotParallelize]
        public void Req2x01()
        {
            IFitnessFunction objective = new SumOfPairsFitnessFunction(new BLOSUM62Matrix());
            GeneticAlgorithmAligner geneticAlgorithm = new GeneticAlgorithmAligner(objective, 100);
            Assert.IsTrue(geneticAlgorithm is IterativeAligner);
        }

        /// <summary>
        /// Demonstrates MSA using a single-state metaheuristic algorithm - such as Simulated Annealing.
        /// </summary>
        [TestMethod]
        [DoNotParallelize]
        public void Req2x02()
        {
            IFitnessFunction objective = new SumOfPairsFitnessFunction(new BLOSUM62Matrix());
            IteratedLocalSearchAligner iteratedLocalSearch = new IteratedLocalSearchAligner(objective, 100);
            Assert.IsTrue(iteratedLocalSearch is IterativeAligner);
        }
    }
}
