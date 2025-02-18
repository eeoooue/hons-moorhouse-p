using MAli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsRequirements
{
    [TestClass]
    public class Objective06
    {
        private MAliInterface MAli = new MAliInterface();

        /// <summary>
        /// Supports batch alignment of a series of sets of sequences from a directory.
        /// </summary>
        [TestMethod]
        public void Req6x01()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Interface displays progress on the current alignment task - in terms of time or iterations.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void Req6x02()
        {
            throw new NotImplementedException("Cannot automate tests for this requirement.");
        }
    }
}
