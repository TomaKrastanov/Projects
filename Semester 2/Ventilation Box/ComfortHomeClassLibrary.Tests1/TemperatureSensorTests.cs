using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary.Tests1
{
    /// <summary>
    ///This is a test class for TemperatureSensorTest and is intended
    ///to contain all TemperatureSensorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TemperatureSensorTest
    {





        /// <summary>
        ///A test for CompareValue
        ///</summary>
        [TestMethod()]
        public void CompareValueTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            TemperatureSensor target = new TemperatureSensor(name); // TODO: Initialize to an appropriate value
            target.CompareValue();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SetThreshold
        ///</summary>
        [TestMethod()]
        public void SetThresholdTest()
        {
            try
            {
                string name = "TEMPERATURE"; // TODO: Initialize to an appropriate value
                TemperatureSensor target = new TemperatureSensor(name); // TODO: Initialize to an appropriate value
                int tempTH = 25; // TODO: Initialize to an appropriate value
                target.SetThreshold(tempTH);
                return;
            }
            catch (Exception)
            {

                Assert.Fail();
            }

        }
    }
}
