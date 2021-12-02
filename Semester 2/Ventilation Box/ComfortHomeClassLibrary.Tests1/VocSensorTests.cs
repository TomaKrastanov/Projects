using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary.Tests1
{
    /// <summary>
    ///This is a test class for VocSensorTest and is intended
    ///to contain all VocSensorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VocSensorTest
    {






        /// <summary>
        ///A test for CompareValue
        ///</summary>
        [TestMethod()]
        public void CompareValueTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            VocSensor target = new VocSensor(name); // TODO: Initialize to an appropriate value
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
                string name = "VOC"; // TODO: Initialize to an appropriate value
                VocSensor target = new VocSensor(name); // TODO: Initialize to an appropriate value
                int vocTh = 600; // TODO: Initialize to an appropriate value
                target.SetThreshold(vocTh);
                return;
            }
            catch (Exception)
            {

                Assert.Fail();
            }

        }
    }
}
