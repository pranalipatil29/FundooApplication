// ******************************************************************************
//  <copyright file="IAccountRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  IAccountRL.cs
//  
//     Purpose:  Creating interface for repository layer
//     @author  Pranali Patil
//     @version 1.0
//     @since   13-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooRepositoryLayer.InterfaceRL
{
    // Including the requried assemblies in to the program
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Response;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// creating account interface for repository layer
    /// </summary>
    public interface IAccountRL
    {
        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns> returns true or false depending upon operation result is successful or not</returns>
        Task<bool> Register(RegistrationModel registrationModel);

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns> returns message indicating operation result is successful or not</returns>
        Task<AccountResponse> LogIn(LoginModel loginModel);

        /// <summary>
        /// Socials the login.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>  returns true or false depending upon operation result is successful or not</returns>
        Task<bool> SocialLogin(RegistrationModel registrationModel);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="forgetPasswordModel">The forget password model.</param>
        /// <returns> returns message indicating operation result is successful or not</returns>
        Task<bool> ForgetPassword(ForgetPasswordModel forgetPasswordModel);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <returns> returns true or false depending upon operation result</returns>
        Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel);

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="loginResponse">The login response.</param>
        /// <returns> returns the token</returns>
        Task<string> GenerateToken(AccountResponse accountResponse);

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="formFile">The form file.</param>
        /// <returns> returns the operation result</returns>
        Task<AccountResponse> ChangeProfilePicture(string emailID, IFormFile formFile);
    }
}
