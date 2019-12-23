using FundooCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.InterfaceRL
{
    public interface IAccountRL
    {
        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns> returns true or false depending upon operation result is successfull or not</returns>
        Task<bool> Register(RegistrationModel registrationModel);

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns> returns message indicating operation result is successfull or not</returns>
        Task<string> LogIn(LoginModel loginModel);

        /// <summary>
        /// Socials the login.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>  returns true or false depending upon operation result is successfull or not</returns>
        Task<bool> SocialLogin(RegistrationModel registrationModel);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="forgetPasswordModel">The forget password model.</param>
        /// <returns> returns message indicating operation result is successfull or not</returns>
        Task<bool> ForgetPassword(ForgetPasswordModel forgetPasswordModel);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <returns> returns true or false depending upon operation result</returns>
        Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel);
    }
}
