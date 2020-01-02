// ******************************************************************************
//  <copyright file="AccountController.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AccountController.cs
//  
//     Purpose:  Creating a controller to manage api calls
//     @author  Pranali Patil
//     @version 1.0
//     @since   13-12-2019
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
    using FundooBusinessLayer1.InterfaceBL;
    using FundooCommonLayer.Model;
    using FundooRepositoryLayer.ServiceRL;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// this class contains different methods to handle API calls for account
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        ///  creating reference of Account business layer class
        /// </summary>
        private readonly IAccountBL accountBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountBL"> injecting reference of account business layer.</param>
        public AccountController(IAccountBL accountBL)
        {
            this.accountBL = accountBL;
        }

        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns> returns message indicating operation is successful or not</returns>
        [HttpPost]
        [Route("Register")]
        ////Post: /api/Account/Register
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            // storing new account info in database
            var result = await this.accountBL.Register(registrationModel);
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
        /// <returns> returns the values indicating whether operation is successful or not</returns>
        [HttpPost]
        [Route("Login")]
        ////Post: /api/Account/Login
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            bool success = false;
            var message = string.Empty;

            // checking login information
            var data = await this.accountBL.Login(loginModel);

            // check whether user get login or not
            if (data != null)
            {
                success = true;
                message = "Login Successfull";

                // generate the token
                var token = await this.accountBL.GenerateToken(data);
                return this.Ok(new { success, message, token, data });
            }
            else
            {
                success = false;
                message = "Login Failed";
                return this.BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="forgetPasswordModel">The forget password model.</param>
        /// <returns> returns the values indicating whether operation is successful or not</returns>
        [HttpPost]
        [Route("ForgetPassword")]
        ////Post: /api/Account/ForgetPassword
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            bool success = false;
            var message = string.Empty;

            // geting the token
            var token = await this.accountBL.ForgetPassword(forgetPasswordModel);

            // chek whether token is generated or not
            if (token)
            {
                success = true;
                message = "Token is sent to your email id..Please Check your mail";
                return this.Ok(new { success, message });
            }
            else
            {
                success = false;
                message = "Invalid emailID";
                return this.BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <returns> returns the values indicating whether operation is successful or not</returns>
        [HttpPut]
        [Route("ResetPassword")]
        ////Post: /api/Account/ResetPassword
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            // geting the token for new password
            var token = await this.accountBL.ResetPassword(resetPasswordModel);
            string message = string.Empty;
            bool success = false;

            // check whether the token is generated or not
            if (token)
            {
                success = true;
                message = "Password changed successfully ";
                return this.Ok(new { success, message });
            }
            else
            {
                success = false;
                message = "Password Reset Failed";
                return this.BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Socials the login.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns> returns the values indicating whether operation is successful or not</returns>
        [HttpPut]
        [Route("SocialLogin")]
        ////Post: /api/Account/SocialLogin
        public async Task<IActionResult> SocialLogin(RegistrationModel registrationModel)
        {
            // geting the token for new password
            var result = await this.accountBL.SocialLogin(registrationModel);
            string message = string.Empty;
            bool success = false;

            // check whether the token is generated or not
            if (result)
            {
                success = true;
                message = "Social Login Successfully done..! ";
                return this.Ok(new { success, message });
            }
            else
            {
                success = false;
                message = "Login Failed";
                return this.BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Profiles the picture upload.
        /// </summary>
        /// <param name="formFile">The form file.</param>
        /// <returns> returns the operation result</returns>
        [HttpPut]
        [Route("ProfilePicture")]
        public async Task<IActionResult> ProfilePictureUpload(IFormFile formFile)
        {
            bool success = false;
            var message = string.Empty;

            try
            {
                // get the email ID of user
                var emailID = HttpContext.User.Claims.First(s => s.Type == "EmailID").Value;

                // pass the email ID and image to business layer method of account
                var data = await this.accountBL.ChangeProfilePicture( emailID, formFile);

                // check whether data is null or not
                if (data != null)
                {
                    success = true;
                    message = "Profile Picture is Uploaded ";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "user doesn't exist";
                    return this.Ok(new { success, message });
                }
            }
            catch (Exception exception)
            {
                success = false;
                message = exception.Message;
                return this.BadRequest(new { success, message });
            }
        }
    }
}