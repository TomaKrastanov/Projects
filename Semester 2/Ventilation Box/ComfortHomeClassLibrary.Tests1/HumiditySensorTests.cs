using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary.Tests1
{
    /// <summary>
    ///This is a test class for HumiditySensorTest and is intended
    ///to contain all HumiditySensorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HumiditySensorTest
    {


        /// <summary>
        ///A test for CompareValue
        ///</summary>
        [TestMethod()]
        public void CompareValueTests()
        {
            try
            {
                //HumiditySensor humiditySensor;
                // Fan s0 = new Fan();
                // s0.humidReading = (string)null;
                // s0.state = Fan.States.OFF;
                // s0.co2Reading = (string)null;
                // s0.vocReading = (string)null;
                // s0.tempReading = (string)null;
                // humiditySensor = new HumiditySensor((string)null);
                // humiditySensor.fan = s0;
                // humiditySensor.SensorValue = (float)61;
                // humiditySensor.MostRecentValue = (float)0;
                // humiditySensor.SetThreshold(1);
                // this.CompareValueTests();
                // Assert.AreEqual<string>("HIGH", ((Sensor)humiditySensor).fan.humidReading);

                //string name = "HUMIDITY"; // TODO: Initialize to an appropriate value
                //HumiditySensor target = new HumiditySensor(name); // TODO: Initialize to an appropriate value
                //target.fan = (Fan)null;
                //target.SensorValue = (float)0;
                //target.MostRecentValue = (float)0;
                //target.SetThreshold(90);
                //target.SensorValue = (float)80;
                //target.CompareValue();


                string name = "HUMIDITY"; // TODO: Initialize to an appropriate value
                HumiditySensor target = new HumiditySensor(name); // TODO: Initialize to an appropriate value
                int threshold = 90;
                target.CompareValue();
                Assert.AreEqual(threshold, target);
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
                string name = "HUMIDITY"; // TODO: Initialize to an appropriate value
                HumiditySensor target = new HumiditySensor(name); // TODO: Initialize to an appropriate value
                int humTH = 90; // TODO: Initialize to an appropriate value
                target.SetThreshold(humTH);
                return;
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }
    }
}
