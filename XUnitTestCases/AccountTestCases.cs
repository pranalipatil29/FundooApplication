// ******************************************************************************
//  <copyright file="AccountTestCases.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AccountTestCases.cs
//  
//     Purpose:  Creating Test cases for Account class
//     @author  Pranali Patil
//     @version 1.0
//     @since   27-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace XUnitTestCases
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;
    using FundooApp.Controllers;
    using FundooBusinessLayer1.InterfaceBL;
    using FundooBusinessLayer1.ServicesBL;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class AccountTestCases
    {
        AccountController accountController;
        private readonly IAccountBL accountBL;

        public AccountTestCases()
        {
            var repository = new Mock<IAccountRL>();
            this.accountBL = new AccountBL(repository.Object);
            accountController = new AccountController(this.accountBL);
        }

        /// <summary>
        /// Tests the registration for bad request.
        /// </summary>
        [Fact]
        public async Task TestRegistrationForBadRequest()
        {
            //var repository =new Mock<IAccountRL>();

            RegistrationModel data = new RegistrationModel()
            {
                FirstName = "Abc",
                LastName = "Abc",
                UserName = "abc",
                EmailID = "abcgmail.com",
                ServiceType = "Basic",
                Password = ""
            };

            var result = await accountController.Register(data);
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

            var result = await accountController.Login(data);
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

            var result = await accountController.Login(model);
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

            accountController.ModelState.AddModelError("EmailId", "Required");
            var result = await accountController.Login(model);
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
                EmailId ="pranalipatil2996@gmail.com"            
            };

            accountController.ModelState.AddModelError("Password", "Required");
            var result = await accountController.Login(model);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the forget password for bad request.
        /// </summary>
        [Fact]
        public async Task TestForgetPasswordForBadRequest()
        {
            ForgetPasswordModel model = new ForgetPasswordModel()
            {
                EmailID = "pranalipatilgmail.com"
            };

            var result = await accountController.ForgetPassword(model);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the forget password for success.
        /// </summary>
        [Fact]
        public async Task TestForgetPasswordForSuccess()
        {
            ForgetPasswordModel model = new ForgetPasswordModel()
            {
                EmailID = "pranalipatil2996@gmail.com"
            };

            var result = await accountController.ForgetPassword(model);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the forget password for null request.
        /// </summary>
        [Fact]
        public async Task TestForgetPasswordForNullRequest()
        {
            ForgetPasswordModel model = new ForgetPasswordModel()
            {
               
            };

            var result = await this.accountBL.ForgetPassword(model);
            Assert.False(result);
        }

        /// <summary>
        /// Tests the reset password for bad request.
        /// </summary>
        [Fact]
        public async Task TestResetPasswordForBadRequest()
        {
            ResetPasswordModel model = new ResetPasswordModel()
            {
                Password = "p"
            };

            var result = await accountController.ResetPassword(model);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests the reset password for success.
        /// </summary>
        [Fact]
        public async Task TestResetPasswordForSuccess()
        {
            ResetPasswordModel model = new ResetPasswordModel()
            {
                Password = "Pranali@123"
            };

            var result = await accountController.ResetPassword(model);
            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// Tests the reset password for null request.
        /// </summary>
        [Fact]
        public async Task TestResetPasswordForNullRequest()
        {
            ResetPasswordModel model = new ResetPasswordModel()
            {
               
            };

            var result = await this.accountBL.ResetPassword(model);
            Assert.False(result);
        }
    }
}
