using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary.Tests1
{

    /// <summary>
    ///This is a test class for ConnectedModuleTest and is intended
    ///to contain all ConnectedModuleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConnectedModuleTest
    {

        /// <summary>
        ///A test for AddSensor
        ///</summary>
        [TestMethod()]
        public void AddSensorTest()
        {

            string ID = "12343"; // TODO: Initialize to an appropriate value
            ConnectedModule target = new ConnectedModule(ID); // TODO: Initialize to an appropriate value
            string TYPE = "CO2"; // TODO: Initialize to an appropriate value
            string ID1 = "12343"; // TODO: Initialize to an appropriate value
            target.AddSensor(TYPE, ID1);



        }

        /// <summary>
        ///A test for assignFan
        ///</summary>
        [TestMethod()]
        public void assignFanTest()
        {
            try
            {
                string ID = "13245"; // TODO: Initialize to an appropriate value
                ConnectedModule target = new ConnectedModule(ID); // TODO: Initialize to an appropriate value
                Fan fan = new Fan(); // TODO: Initialize to an appropriate value
                target.assignFan(fan);
                return;
            }
            catch (Exception)
            {

                Assert.Fail();
            }

        }
    }
}
