// ******************************************************************************
//  <copyright file="AdminBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AdminBL.cs
//  
//     Purpose:  Implementing different methods for admin
//     @author  Pranali Patil
//     @version 1.0
//     @since   3-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooBusinessLayer.ServicesBL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FundooBusinessLayer.InterfaceBL;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// this class implements all the methods in business layer Admin interface 
    /// </summary>
    /// <seealso cref="FundooBusinessLayer.InterfaceBL.IAdminBL" />
    public class AdminBL : IAdminBL
    {
        /// <summary>
        /// creating reference of repository layer Admin interface
        /// </summary>
        private readonly IAdminRL adminRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminBL"/> class.
        /// </summary>
        /// <param name="adminRL">The reference of repository layer admin interface.</param>
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>
        /// returns the user info if user gets logged in
        /// </returns>
        /// <exception cref="Exception">
        /// EmailID and Password is required
        /// or
        /// </exception>
        public async Task<AccountResponse> Login(LoginModel loginModel)
        {
            try
            {
                // check user entered all the required values or not
                if (loginModel != null)
                {
                    var result = await this.adminRL.Login(loginModel);
                    return result;
                }
                else
                {
                    throw new Exception("EmailID and Password is required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>
        /// returns the true or false based on operation result
        /// </returns>
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
                    return await this.adminRL.Register(registrationModel);
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
        /// Generates the token.
        /// </summary>
        /// <param name="accountResponse">The account response.</param>
        /// <returns>
        /// returns the token
        /// </returns>
        /// <exception cref="Exception">
        /// Invalid token
        /// or
        /// </exception>
        public async Task<string> GenerateToken(AccountResponse accountResponse)
        {
            try
            {
                // check wheather account response is null or not
                if (accountResponse != null)
                {
                    var result = await this.adminRL.GenerateToken(accountResponse);
                    return result;
                }
                else
                {
                    throw new Exception("Invalid token");
                }
            }
            catch (Exception exceptiion)
            {
                throw new Exception(exceptiion.Message);
            }
        }

        /// <summary>
        /// Gets the user statistics.
        /// </summary>
        /// <returns>
        /// returns the Count of users which uses Basic and advance services
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public Dictionary<string, int> GetUserStatistics()
        {
            try
            {
                return this.adminRL.GetUserStatistics();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Users the information.
        /// </summary>
        /// <returns>
        /// returns the list of users info
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public IList<AccountResponse> UsersInfo()
        {
            try
            {
                return this.adminRL.UsersInfo();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Searches the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns> returns the list of users</returns>
        /// <exception cref="Exception">
        /// Please enter UserName
        /// or
        /// </exception>
        public IList<AccountResponse> SearchUser(string name)
        {
            try
            {
                // check wheather user entered key is null or not
                if (name != null)
                {
                    return this.adminRL.SearchUser(name);
                }
                else
                {
                    throw new Exception("Please enter UserName");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Changes the profile picture.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="formFile">The form file.</param>
        /// <returns>
        /// returns the operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Please select correct image
        /// or
        /// </exception>
        public async Task<AccountResponse> ChangeProfilePicture(string emailID, IFormFile formFile)
        {
            try
            {
                // ckeck whether user passed correct image info or not
                if (formFile != null)
                {
                    // pass user email id and image to repository layer method
                    return await this.adminRL.ChangeProfilePicture(emailID, formFile);
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
