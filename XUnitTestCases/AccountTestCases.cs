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
        private readonly IAccountBL accountBL ;
        private readonly IAccountRL accountRL ;

        public AccountTestCases(IAccountRL accountRL)
        {
            this.accountRL = accountRL;
            accountBL = new AccountBL(this.accountRL);
            accountController = new AccountController(accountBL);
        }

        //[Fact]
        //public async Task TestRegistrationForBadRequest()
        //{
        //    //var repository =new Mock<IAccountRL>();

        //    RegistrationModel data = new RegistrationModel()
        //    {
        //        FirstName = "Pranali",
        //        LastName = "Patil",
        //        UserName = "pranali",
        //        EmailID = "pranali@2996@gmail.com",
        //        UserType = 0,
        //        ServiceType = "Adavance",
        //        Password="Pranali@29"
        //    };

        //    var result = await accountController.Register(data);
        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        [Fact]
        public async Task TestLoginForBadRequest()
        {
            var repository = new Mock<IAccountRL>();
            var business = new AccountBL(repository.Object);
            var controller = new AccountController(business);

            LoginModel data = new LoginModel()
            {
                EmailId = "pranalipatil2996@gmail.com",
                Password = ""
            };

            var result = await controller.Login(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestLoginForNotNull()
        {
            var repository = new Mock<IAccountRL>();
            var business = new AccountBL(repository.Object);
            var controller = new AccountController(business);

            LoginModel model = new LoginModel()
            {
               EmailId = "pranali2996@gmail.com",
               Password="Pranali@29"                
            };

            // var data = business.Login(model);
            var result = await controller.Login(model);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestLoginForEmailID()
        {
            var repository = new Mock<IAccountRL>();
            var business = new AccountBL(repository.Object);
            var controller = new AccountController(business);

            LoginModel model = new LoginModel()
            {
               Password = "Pranali@29"
            };

            controller.ModelState.AddModelError("EmailId", "Required");
            
            var result = await controller.Login(model);
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}
