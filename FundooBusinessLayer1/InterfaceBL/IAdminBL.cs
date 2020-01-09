// ******************************************************************************
//  <copyright file="IAdminBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  IAdminBL.cs
//  
//     Purpose:  Creating interface for Admin
//     @author  Pranali Patil
//     @version 1.0
//     @since   3-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooBusinessLayer.InterfaceBL
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
    /// declaring the methods for admin controller
    /// </summary>
    public interface IAdminBL
    {
        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns> returns the true or false based on operation result</returns>
        Task<bool> Register(RegistrationModel registrationModel);

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns> returns the user info if user gets logged in</returns>
        Task<AccountResponse> Login(LoginModel loginModel);

        /// <summary>
        /// Gets the user statistics.
        /// </summary>
        /// <returns> returns the Count of users which uses Basic and advance services</returns>
        Dictionary<string, int> GetUserStatistics();

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="accountResponse">The account response.</param>
        /// <returns> returns the token</returns>
        Task<string> GenerateToken(AccountResponse accountResponse);

        /// <summary>
        /// Users the information.
        /// </summary>
        /// <returns> returns the list of users info</returns>
        IList<AccountResponse> UsersInfo();

        /// <summary>
        /// Searches the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>returns the list of users</returns>
        IList<AccountResponse> SearchUser(string name);

        /// <summary>
        /// Changes the profile picture.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="file">The form file.</param>
        /// <returns> returns the operation result</returns>
        Task<AccountResponse> ChangeProfilePicture(string emailID, IFormFile file);
    }
}
