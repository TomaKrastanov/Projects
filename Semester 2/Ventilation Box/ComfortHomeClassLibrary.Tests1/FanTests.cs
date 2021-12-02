using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary.Tests1
{
    /// <summary>
    ///This is a test class for FanTest and is intended
    ///to contain all FanTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FanTest
    {
        /// <summary>
        ///A test for Prioritize
        ///</summary>
        [TestMethod()]
        public void PrioritizeTest()
        {
            Fan target = new Fan(); // TODO: Initialize to an appropriate value
            target.co2Reading = Convert.ToString(1500);
            target.Prioritize();

        }

        /// <summary>
        ///A test for SetState
        ///</summary>
        [TestMethod()]
        public void SetStateTest()
        {
            try
            {
                Fan target = new Fan(); // TODO: Initialize to an appropriate value
                target.co2Reading = Convert.ToString(1500);
                string stateToSet = "MEDIUM"; // TODO: Initialize to an appropriate value
                target.SetState(stateToSet);
                return;
            }
            catch (Exception)
            {

                Assert.Fail();
            }

        }
    }
}
