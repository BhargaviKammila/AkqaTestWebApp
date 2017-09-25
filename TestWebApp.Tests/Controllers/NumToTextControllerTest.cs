using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestWebApp;
using TestWebApp.Controllers;
using TestWebApp.Models;

namespace TestWebApp.Tests.Controllers
{
    [TestClass]
    public class NumToTextControllerTest
    {
        [TestMethod]
        public void ConvertNumToText()
        {
            NumToTextController controller = new NumToTextController();
            
            // Act
            DataOutput result = controller.ConvertNumToText(new DataInput
            {
                Number = "10"
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("TEN DOLLARS", result.Output.Trim());            
        }

        [TestMethod]
        public void ConvertNumToTextWithCents()
        {
            NumToTextController controller = new NumToTextController();

            // Act
            DataOutput result = controller.ConvertNumToText(new DataInput
            {
                Number = "102.45"
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ONE HUNDRED AND  TWO DOLLARS AND FOURTY-FIVE CENTS", result.Output.Trim());
        }
    }
}
