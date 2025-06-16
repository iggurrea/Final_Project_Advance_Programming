using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;

namespace TicketTests
{
    [TestClass]
    public class TicketTests
    {
        [TestMethod]
        public void HardwareTicket_GetSummary_ReturnsCorrectFormat()
        {
            var hardware = new HardwareTicket
            {
                Equipment = "Printer",
                Malfunction = "Paper jam"
            };

            string result = hardware.GetSummary();

            Assert.AreEqual("[HW] Printer - Paper jam", result);
        }
    }
}
