
namespace XUnitTestCases
{
    using FundooApp.Controllers;
    using FundooBusinessLayer1.InterfaceBL;
    using FundooBusinessLayer1.ServicesBL;
    using FundooCommonLayer.Model;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    public class AccountTestCases
    {
        AccountController accountController;
        IAccountBL accountBL;
        IAccountRL accountRL;

        public AccountTestCases()
        {
            accountBL = new AccountBL(accountRL);
            accountController = new AccountController(accountBL);
        }


        [Fact]
        public void TestRegistration()
        {
            var repository =new Mock<IAccountRL>();
            RegistrationModel data = new RegistrationModel()
            {
                FirstName = "Pranali",
                LastName = "Patil",
                UserName = "pranali",
                EmailID = "pranali@2996@gmail.com",
                UserType = 0,
                ServiceType = "Adavance",
                Password="Pranali@29"
            };

            var createdResponse = accountController.Register(data);

            Assert.NotNull(createdResponse);
            
        }
    }
}
