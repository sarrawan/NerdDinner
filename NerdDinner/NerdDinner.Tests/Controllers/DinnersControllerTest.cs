using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdDinner.Controllers;



namespace NerdDinner.Tests.Controllers
{

    [TestClass]
    public class DinnersControllerTest
    {
        [TestMethod]
        public void DetailsAction_Should_Return_View_For_ExistingDinner()
        {
            var controller = new DinnersController();

            var result = controller.Details(1) as ViewResult;

            Assert.IsNotNull(result, "Expected View");

        }

        [TestMethod]
        public void DetailsAction_Should_Return_NotFoundView_For_BogusDinner()
        {
            var controller = new DinnersController();

            var result = controller.Details(999) as ViewResult;

            Assert.AreEqual("NotFound", result.ViewName);
        }
    }
}
