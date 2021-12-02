using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary.Tests1
{
    /// <summary>
    ///This is a test class for ZigbeeDongleTest and is intended
    ///to contain all ZigbeeDongleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ZigbeeDongleTest
    {




        /// <summary>
        ///A test for Broadcast
        ///</summary>
        [TestMethod()]
        public void BroadcastTest()
        {
            ZigbeeDongle target = new ZigbeeDongle(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.Broadcast(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Disassociate
        ///</summary>
        [TestMethod()]
        public void DisassociateTest()
        {
            try
            {
                ZigbeeDongle target = new ZigbeeDongle(); // TODO: Initialize to an appropriate value
                Regex regex = new Regex(@"(\w+)");
                string command = "AT + DASSL";


                MatchCollection matches = regex.Matches(command);

                target.Disassociate();
                return;
            }
            catch (Exception)
            {

                Assert.Fail();
            }

        }

        /// <summary>
        ///A test for Join_PAN
        ///</summary>
        [TestMethod()]
        public void Join_PANTest()
        {
            try
            {
                ZigbeeDongle target = new ZigbeeDongle(); // TODO: Initialize to an appropriate value
                string PAN_ID = "AT+JPAN:435243"; // TODO: Initialize to an appropriate value
                target.Join_PAN(PAN_ID);
                return;
            }
            catch (Exception)
            {

                Assert.Fail();
            }

            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Open
        ///</summary>
        [TestMethod()]
        public void OpenTest()
        {
            ZigbeeDongle target = new ZigbeeDongle(); // TODO: Initialize to an appropriate value
            target.Open();

        }

        /// <summary>
        ///A test for SendATCommand
        ///</summary>
        [TestMethod()]

        //[DeploymentItem("ComfortHomeUerInterface.exe")]

        public void SendATCommandTest()
        {
            try
            {
                ZigbeeDongle target = new ZigbeeDongle(); // TODO: Initialize to an appropriate value
                //string command = "AT + "; // TODO: Initialize to an appropriate value

                //target.SendATCommand(command);

                //target.SendATCommand(command);

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        ///A test for Setup_PAN
        ///</summary>
        [TestMethod()]
        public void Setup_PANTest()
        {
            ZigbeeDongle target = new ZigbeeDongle(); // TODO: Initialize to an appropriate value
            target.Setup_PAN();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Unicast
        ///</summary>
        [TestMethod()]
        public void UnicastTest()
        {
            ZigbeeDongle target = new ZigbeeDongle(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            ConnectedModule module = null; // TODO: Initialize to an appropriate value
            target.Unicast(message, module);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for readData
        ///</summary>
        [TestMethod()]
        public void readDataTest()
        {
            ZigbeeDongle target = new ZigbeeDongle(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.readData();
            Assert.AreEqual(expected, actual);

        }
    }
}
