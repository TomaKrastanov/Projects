using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary.Tests1
{
    /// <summary>
    ///This is a test class for CO2SensorTest and is intended
    ///to contain all CO2SensorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CO2SensorTest

    {



        /// <summary>
        ///A test for CompareValue
        ///</summary>
        [TestMethod()]
        public void CompareValueTest()
        {
            try
            {
                CO2Sensor cO2 = new CO2Sensor(name: "CO2");
                cO2.SetFan(new Fan());
                cO2.SetCurrent(1262);
                cO2.SetThreshold(1500);
                cO2.CompareValue();
                Assert.IsTrue(true);
                return;
            }
            catch (Exception)
            {

                Assert.Fail();
            }
           
        }

        /// <summary>
        ///A test for SetThreshold
        ///</summary>
        [TestMethod()]
        public void SetThresholdTest()
        {
            try
            {
                string name = "CO2"; // TODO: Initialize to an appropriate value
                CO2Sensor target = new CO2Sensor(name); // TODO: Initialize to an appropriate value
                int co2TH = 1500; // TODO: Initialize to an appropriate value
                target.SetThreshold(co2TH);
                return;
            }
            catch (Exception)
            {

                Assert.Fail();
            }





        }
    }
}
