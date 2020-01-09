// ******************************************************************************
//  <copyright file="LabelTestCases.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  LabelTestCases.cs
//  
//     Purpose:  Creating Test cases for Label
//     @author  Pranali Patil
//     @version 1.0
//     @since   9-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace XUnitTestCases
{
    using FundooApp.Controllers;
    using FundooBusinessLayer.InterfaceBL;
    using FundooBusinessLayer.ServicesBL;
    using FundooCommonLayer.Model.Request;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    ///  this class contains different test cases for Label Controller
    /// </summary>
   public class LabelTestCases
    {
        /// <summary>
        /// creating reference of label controller
        /// </summary>
        LabelController labelController;

        /// <summary>
        /// creating reference of business layer label interface
        /// </summary>
        ILabelBL labelBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelTestCases"/> class.
        /// </summary>
        public LabelTestCases()
        {
            var repository = new Mock<ILabelRL>();
            this.labelBL = new LabelBL(repository.Object);
            labelController = new LabelController(this.labelBL);
        }

        /// <summary>
        /// Tests the label creation for bad request.
        /// </summary>
        [Fact]
        public async Task TestLabelCreationForBadRequest()
        {
            var data = new LabelRequest()
            {
               
            };

            labelController.ModelState.AddModelError("Label", "Required");
            var result = await labelController.CreateLabel(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the label creation for success.
        /// </summary>
        [Fact]
        public async Task TestLabelCreationForSuccess()
        {
            var data = new LabelRequest()
            {
                Label ="jdhf"
            };

            var result = await labelController.CreateLabel(data);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the label creation for null request.
        /// </summary>
        [Fact]
        public async Task TestLabelCreationForNullRequest()
        {
            var data = new LabelRequest()
            {

            };

            var result = await labelBL.CreateLabel(data, "8e9a3160-f95e-4424-91ca-3164d34a29ac");
            Assert.Null(data.Label);
        }

        /// <summary>
        /// Tests the update label null request.
        /// </summary>
        [Fact]
        public async Task TestUpdateLabelNullRequest()
        {
            var data = new LabelRequest()
            {

            };

            var result = await labelController.UpdateLabel(data, 1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the update label for bad result.
        /// </summary>
        [Fact]
        public async Task TestUpdateLabelForBadResult()
        {
            var data = new LabelRequest()
            {
                Label = ""
            };

            var result = await labelController.UpdateLabel(data, 1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the update label for ok o result.
        /// </summary>
        [Fact]
        public async Task TestUpdateLabelForOkOResult()
        {
            var data = new LabelRequest()
            {
                Label = "ddgh"
            };

            var result = await labelController.UpdateLabel(data, 1);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the update label for invelid label identifier.
        /// </summary>
        [Fact]
        public async Task TestUpdateLabelForInvelidLabelID()
        {
            var data = new LabelRequest()
            {
                Label ="fhkas"
            };

            var result = await labelController.UpdateLabel(data, -1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the delete label for bad result.
        /// </summary>
        [Fact]
        public async Task TestDeleteLabelForBadResult()
        {
           var result = await labelController.DeleteLabel(-1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the label update for ok o result.
        /// </summary>
        [Fact]
        public async Task TestLabelUpdateForOkOResult()
        {
            var result = await labelController.DeleteLabel(1);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
