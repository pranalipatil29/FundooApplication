// ******************************************************************************
//  <copyright file="AdminTestCases.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AdminTestCases.cs
//  
//     Purpose:  Creating Test cases for Account class
//     @author  Pranali Patil
//     @version 1.0
//     @since   8-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace XUnitTestCases
{
    // Including the requried assemblies in to the program
    using FundooApp.Controllers;
    using FundooBusinessLayer.InterfaceBL;
    using FundooBusinessLayer.ServicesBL;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// this class contains different test cases for Admin Controller
    /// </summary>
    public class AdminTestCases
    {
        /// <summary>
        /// The admin controller
        /// </summary>
        private readonly AdminController adminController;

        /// <summary>
        /// The admin bl
        /// </summary>
        private readonly IAdminBL adminBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminTestCases"/> class.
        /// </summary>
        public AdminTestCases()
        {
            var repository = new Mock<IAdminRL>();
            this.adminBL = new AdminBL(repository.Object);
            this.adminController = new AdminController(adminBL);
        }

        /// <summary>
        /// Tests the registration for bad request.
        /// </summary>
        [Fact]
        public async Task TestRegistrationForBadRequest()
        {
            // setting the values for registration model with password field as empty
            RegistrationModel data = new RegistrationModel()
            {
                FirstName = "Abc",
                LastName = "Abc",
                UserName = "abc",
                EmailID = "abcgmail.com",
                ServiceType = "Basic",
                Password = ""
            };

            var result = await adminController.Register(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the login for bad request.
        /// </summary>
        [Fact]
        public async Task TestLoginForBadRequest()
        {
            LoginModel data = new LoginModel()
            {
                EmailId = "pranalipatil2996@gmail.com",
                Password = ""
            };

            var result = await adminController.Login(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the login for success.
        /// </summary>
        [Fact]
        public async Task TestLoginForSuccess()
        {
            var model = new LoginModel()
            {
                EmailId = "pranali2996@gmail.com",
                Password = "Pranali@29"
            };

            var result = await adminController.Login(model);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the login for email identifier.
        /// </summary>
        [Fact]
        public async Task TestLoginForEmailID()
        {
            LoginModel model = new LoginModel()
            {
                Password = "Pranali@29"
            };

            adminController.ModelState.AddModelError("EmailId", "Required");
            var result = await adminController.Login(model);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the login for password.
        /// </summary>
        [Fact]
        public async Task TestLoginForPassword()
        {
            LoginModel model = new LoginModel()
            {
                EmailId = "pranalipatil2996@gmail.com"
            };

            adminController.ModelState.AddModelError("Password", "Required");
            var result = await adminController.Login(model);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the search user for bad object result.
        /// </summary>
        [Fact]
        public async Task TestSearchUserForBadObjectResult()
        {
            SearchkeyRequest key = new SearchkeyRequest()
            {
                Key =""
            };

            var result = await this.adminController.SearchUser(key);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the search user for ok object result.
        /// </summary>
        [Fact]
        public async Task TestSearchUserForOkObjectResult()
        {
            SearchkeyRequest key = new SearchkeyRequest()
            {
                Key = "pooja"
            };

            var result = await this.adminController.SearchUser(key);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
