// ******************************************************************************
//  <copyright file="IAccountBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  IAccountBL.cs
//  
//     Purpose:  Creating interface for business layer
//     @author  Pranali Patil
//     @version 1.0
//     @since   12-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooBusinessLayer1.InterfaceBL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Response;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// creating interface for business layer
    /// </summary>
    public interface IAccountBL
    {
        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns> returns message indicating operation is done or not</returns>
        Task<bool> Register(RegistrationModel registrationModel);

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns> returns message indicating operation is done or not</returns>
        Task<AccountResponse> Login(LoginModel loginModel);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="forgetPasswordModel">The forget password model.</param>
        /// <returns> returns true or false depending upon operation result</returns>
        Task<bool> ForgetPassword(ForgetPasswordModel forgetPasswordModel);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <returns> returns true or false depending upon operation result</returns>
        Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel);

        /// <summary>
        /// Socials the login.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns> returns message indicating operation is done or not</returns>
        Task<bool> SocialLogin(RegistrationModel registrationModel);

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="loginResponse">The login response.</param>
        /// <returns> returns the token</returns>
        Task<string> GenerateToken(AccountResponse accountResponse);

        /// <summary>
        /// Changes the profile picture.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="formFile">The form file.</param>
        /// <returns> returns the operation result</returns>
        Task<AccountResponse> ChangeProfilePicture(string emailID, IFormFile formFile);
    }
}
