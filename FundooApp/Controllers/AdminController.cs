// ******************************************************************************
//  <copyright file="AdminController.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AdminController.cs
//  
//     Purpose:  Creating a controller to manage Admin API calls
//     @author  Pranali Patil
//     @version 1.0
//     @since   3-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooBusinessLayer.InterfaceBL;
    using FundooCommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var message = string.Empty;
            bool success = false;

            try
            {
                var data = await this.adminBL.Login(loginModel);

                if(data != null)
                {
                    success = true;
                    message = "Login Successfull";

                    var token = await this.adminBL.GenerateToken(data);
                    return this.Ok(new { success, message, data, token });
                }
                else
                {
                    success = false;
                    message = "Login Failed";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception exception)
            {
                success = false;
                message = exception.Message;
                return this.BadRequest(new { success, message });
            }
        }
    }
}