using System;
using System.Web.Mvc;
using ec.Reservation.Clients.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ecReservationClientsWebTest
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_Controller_Erfolgreich_Erreichbar()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index(-1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void About_Controller_Erfolgreich_Erreichbar()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.About() as ViewResult;
            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
        [TestMethod]
        public void Contact_Controller_Erfolgreich_Erreichbar()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Contact() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
