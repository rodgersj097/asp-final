using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using F2018Pranks.Controllers;
using F2018Pranks.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace F2018Pranks.Tests.Controllers
{
    [TestClass]
    public class PranksControllerTest
    {
        PranksController controller;
        Mock<IMockPrank> mock;
        List<Prank> pranks;
        Prank prank; 
        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IMockPrank>();

            pranks = new List<Prank>
            {
                new Prank {
                    PrankId = 24,
                    Name = "Tape on the Receiver",
                    Description = "Hilarious",
                    Photo = "tape.png"
                },
                new Prank {
                    PrankId = 49,
                    Name = "Disconnect the Monitor",
                    Description = "Infuriating",
                    Photo = "disconnect.png"
                },
                new Prank {
                    PrankId = 72,
                    Name = "Gift Wrap the Desk",
                    Description = "Jim would be so proud",
                    Photo = "wrap.png"
                }
            };

            mock.Setup(m => m.Pranks).Returns(pranks.AsQueryable());
            controller = new PranksController(mock.Object);
        }
        // Prank Index 
        [TestMethod]
        public void IndexViewLoads()
        {
            //act 
            ViewResult result = controller.Index() as ViewResult;

            //assert
            Assert.AreEqual("Index", result.ViewName);

        }

        // Prank Index 
        [TestMethod]
        public void IndexValidLoadsPrank()
        {
            //act
            var result = (List<Prank>)((ViewResult)controller.Index()).Model;

            //asigne 
            CollectionAssert.AreEqual(pranks, result);

        }

        // Prank/Detials 
        [TestMethod]
        public void DetailsValidId()
        {
            Prank result = (Prank)((ViewResult)controller.Details(24)).Model;

            Assert.AreEqual(pranks[0], result);

        }

        // Prank/Detials 
        [TestMethod]
        public void DetialsInvalIdLoadsError()
        {
            ViewResult result = (ViewResult)controller.Details(2342); 

            Assert.AreEqual("Error", result.ViewName); 
        }

        // Prank/Detials
        [TestMethod]
        public void DetailsNoID()
        {
            ViewResult result = (ViewResult)controller.Details(null);

            Assert.AreEqual("Error", result.ViewName);
        }

        // Prank/Create
        [TestMethod]
        public void CreateViewLoads()
        {
            ViewResult result = (ViewResult)controller.Create();

            Assert.AreEqual("Create", result.ViewName);

        }

        // Prank/Create
        [TestMethod]
        public void CreateSavevalid()
        {
            Prank mockPrank = prank;
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(mockPrank);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        //Prank/create
        [TestMethod]
        public void CreateSaveInValid()
        {
            //arrange 
            Prank wrongPrank = prank;

            //act
            controller.ModelState.AddModelError("Error", "Error thing");
            ViewResult result = (ViewResult)controller.Create(wrongPrank);

            //assert
            Assert.IsNotNull(result.ViewBag.PrankId);
         

        }

        
    }
}
