// ******************************************************************************
//  <copyright file="AccountBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AccountBL.cs
//  
//     Purpose:  Implementing business logic for apllication
//     @author  Pranali Patil
//     @version 1.0
//     @since   12-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooBusinessLayer1.ServicesBL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Threading.Tasks;
    using FundooBusinessLayer1.InterfaceBL;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// this class is used to check the business logic of application
    /// </summary>
    /// <seealso cref="FundooBusinessLayer1.InterfaceBL.IAccountBL" />
    public class AccountBL : IAccountBL
    {
        /// <summary>
        /// creating reference of repository layer interface
        /// </summary>
        private readonly IAccountRL accountRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountBL"/> class.
        /// </summary>
        /// <param name="accountRl">The reference of repository layer account class .</param>
        public AccountBL(IAccountRL accountRl)
        {
            this.accountRL = accountRl;
        }

        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns> returns true is task completed otherwise returns false</returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<bool> Register(RegistrationModel registrationModel)
        {
            try
            {
                // check whether all properties entered by user have some value or not
                if (registrationModel != null)
                {
                    // return true if registaration is successfull
                    return await this.accountRL.Register(registrationModel);
                }
                else
                {
                    // otherwise throw exception
                    throw new Exception("Data Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">
        /// EmailId or Password Required
        /// or
        /// </exception>
        public async Task<AccountResponse> Login(LoginModel loginModel)
        {
            try
            {
                // check whether user enter all data for login or not
                if (loginModel != null)
                {             
                    // get the user info
                    var result = await this.accountRL.LogIn(loginModel);
                    return result;
                }
                else
                {
                    // otherwise throw exception
                    throw new Exception("EmailId and Password is Requirred");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="forgetPasswordModel">The forget password model.</param>
        /// <returns> returns the true or false indicating operation is done or not</returns>
        /// <exception cref="Exception">
        /// Invalid EmailID
        /// or
        /// </exception>
        public async Task<bool> ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                // check whether user enter all required data or not
                if (forgetPasswordModel != null)
                {
                    // return true if user entered emailid is correct
                    var result = await this.accountRL.ForgetPassword(forgetPasswordModel);
                    return result;
                }
                else
                {
                    throw new Exception("Invalid EmailID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <returns> returns true or false indicating operation is successful or not</returns>
        /// <exception cref="Exception">
        /// Invalid Email id or password
        /// or
        /// </exception>
        public async Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                // check whether user enter all required data or not
                if (resetPasswordModel != null)
                {
                    // return true if password reset operation is successfull
                    var result = await this.accountRL.ResetPassword(resetPasswordModel);
                    return result;
                }
                else
                {
                    throw new Exception("Invalid Emailid or password");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="loginResponse">The login response.</param>
        /// <returns> returns the token</returns>
        /// <exception cref="Exception">
        /// invalid token
        /// or
        /// </exception>
        public async Task<string> GenerateToken(AccountResponse accountResponse)
        {
            try
            {
                if (accountResponse != null)
                {
                    var result = await this.accountRL.GenerateToken(accountResponse);
                    return result;
                }
                else
                {
                    throw new Exception("invalid token");
                }
            }
            catch (Exception exceptiion)
            {
                throw new Exception(exceptiion.Message);
            }
        }

        public async Task<AccountResponse> ChangeProfilePicture(string emailID, IFormFile formFile)
        {
            try
            {
                // ckeck whether user passed correct image info or not
                if (formFile != null)
                {
                    // pass user email id and image to repository layer method
                    return await this.accountRL.ChangeProfilePicture(emailID, formFile);
                }
                else
                {
                    throw new Exception("Please select correct image");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
