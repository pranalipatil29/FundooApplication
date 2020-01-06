﻿// ******************************************************************************
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
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooBusinessLayer.InterfaceBL;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Response;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// this class contains different API's for Admin
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// creating reference of business layer admin class
        /// </summary>
        private readonly IAdminBL adminBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="adminBL">The admin bl.</param>
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            // storing new account info in database
            var result = await this.adminBL.Register(registrationModel);
            bool success = false;
            var message = string.Empty;

            // checking whether result is successfull or nor
            if (result)
            {
                // if yes then return the result 
                success = true;
                message = "Registration Succeffully Completed";
                return this.Ok(new { success, message });
            }
            else
            {
                success = false;
                message = "Registration Failed";
                return this.BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns> returns the operation result</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var message = string.Empty;
            bool success = false;

            try
            {
                // get the admin data from business layer method
                var data = await this.adminBL.Login(loginModel);

                // check wheather data variable holds admin info or not
                if(data != null)
                {
                    success = true;
                    message = "Login Successfull";

                    // generate the token for admin
                    var token = await this.adminBL.GenerateToken(data);

                    // return the Ok response with admin data
                    return this.Ok(new { success, message, token, data });
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

        /// <summary>
        /// Users the statistics.
        /// </summary>
        /// <returns>returns the list of users or bad request result</returns>
        [HttpGet]
        [Route("UserStatistics")]
        public async Task<IActionResult> UserStatistics()
        {
            var message = string.Empty;
            bool success = false;

            try
            {
                Dictionary<string, int> users = this.adminBL.GetUserStatistics();

                if(users.Count > 0)
                {
                    success = true;
                    message = "User statistics Info";
                    return this.Ok(new { success, message , users });
                }
                else
                {
                    success = false;
                    message = "users not found";
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

        /// <summary>
        /// Gets the users information.
        /// </summary>
        /// <returns>returns the count of users which uses basic or advance services or bad request result</returns>
        [HttpGet]
        [Route("UsersList")]
        public async Task<IActionResult> GetUsersInfo()
        {
            var message = string.Empty;
            bool success = false;

            try
            {
                IList<AccountResponse> users = this.adminBL.UsersInfo();

                if (users.Count > 0)
                {
                    success = true;
                    message = "Users Info";
                    return this.Ok(new { success, message, users });
                }
                else
                {
                    success = false;
                    message = "users not found";
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

        /// <summary>
        /// Searches the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>returns the list of users or bad request result</returns>
        [HttpPost]
        [Route("SearchUser")]
        public async Task<IActionResult> SearchUser(SearchkeyRequest searchkeyRequest)
        {
            var message = string.Empty;
            bool success = false;

            try
            {
                // get the list of users which have admin entered name
                IList<AccountResponse> result = this.adminBL.SearchUser(searchkeyRequest.key);

                // check wheather any user having admin entered name or not
                if(result.Count > 0)
                {
                    success = true;
                    return this.Ok(new { success, result });
                }
                else
                {
                    success = false;
                    message = "user not found";
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