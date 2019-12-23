using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooBusinessLayer1.InterfaceBL;
using FundooCommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {/// <summary>
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

        [HttpPost]
        [Route("Register")]
        // Post: /api/Account/Register
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            // storing new account info in database
            var result = await accountBL.Register(registrationModel);
            bool success = false;
            var message = "";


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
        /// <returns> returns the values indicating whether operation is successfull or not</returns>
        [HttpPost]
        [Route("Login")]
        // Post: /api/Account/Login
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            bool success = false;
            var message = "";

            // checking login information
            var token = await accountBL.Login(loginModel);

            // check whether user get login or not
            if (token != null)
            {
                success = true;
                message = "Login Successfull";
                return Ok(new { success, message, token });
            }
            else
            {
                success = false;
                message = "Login Failed";
                return BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="forgetPasswordModel">The forget password model.</param>
        /// <returns> returns the values indicating whether operation is successfull or not</returns>
        [HttpPost]
        [Route("ForgetPassword")]
        // Post: /api/Account/ForgetPassword
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            bool success = false;
            var message = "";

            // geting the token
            var token = await accountBL.ForgetPassword(forgetPasswordModel);

            // chek whether token is generated or not
            if (token)
            {
                success = true;
                message = "Token is sent to your email id..Please Check your mail";
                return Ok(new { success, message });
            }
            else
            {
                success = false;
                message = "Invalid emailID";
                return BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <returns>  returns the values indicating whether operation is successfull or not</returns>
        [HttpPut]
        [Route("ResetPassword")]
        // Post: /api/Account/ResetPassword
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            // geting the token for new password
            var token = await accountBL.ResetPassword(resetPasswordModel);

            // check whether the token is generated or not
            if (token != null)
            {
                bool success = true;
                var message = "Password changed successfully ";
                return Ok(new { success, message });
            }
            else
            {
                bool fail = false;
                var errorMessage = "Password Reset Failed";
                return BadRequest(new { fail, errorMessage });
            }
        }

        /// <summary>
        /// Socials the login.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("SocialLogin")]
        // Post: /api/Account/SocialLogin
        public async Task<IActionResult> SocialLogin(RegistrationModel registrationModel)
        {
            // geting the token for new password
            var result = await accountBL.SocialLogin(registrationModel);

            // check whether the token is generated or not
            if (result)
            {
                bool success = true;
                var message = "Social Login Successfully done..! ";
                return Ok(new { success, message });
            }
            else
            {
                bool fail = false;
                var errorMessage = "Login Failed";
                return BadRequest(new { fail, errorMessage });
            }
        }
    }
}