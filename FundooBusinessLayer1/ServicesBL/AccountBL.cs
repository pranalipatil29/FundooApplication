using FundooBusinessLayer1.InterfaceBL;
using FundooCommonLayer.Model;
using FundooRepositoryLayer.InterfaceRL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer1.ServicesBL
{
    public class AccountBL : IAccountBL
    {
        /// <summary>
        /// creating reference of repository layer interface
        /// </summary>
        private readonly IAccountRL accountRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountBL"/> class.
        /// </summary>
        /// <param name="accountRl">The account rl.</param>
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
                    return await accountRL.Register(registrationModel);
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
        /// EmailId or Password Requirred
        /// or
        /// </exception>
        public async Task<string> Login(LoginModel loginModel)
        {
            try
            {
                // check whether user enter all data for login or not
                if (loginModel != null)
                {
                    // return true if login successfull
                    var result = await accountRL.LogIn(loginModel);
                    return result;
                }
                else
                {
                    // otherwise throw exception
                    throw new Exception("EmailId or Password Requirred");
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
        /// <returns></returns>
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
                    var result = await accountRL.ForgetPassword(forgetPasswordModel);
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
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Invalid Emailid or password
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
                    var result = await accountRL.ResetPassword(resetPasswordModel);
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
        /// Socials the login.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>
        /// returns message indicating operation is done or not
        /// </returns>
        /// <exception cref="Exception">
        /// Please provide correct data
        /// or
        /// </exception>
        public async Task<bool> SocialLogin(RegistrationModel registrationModel)
        {
            try
            {
                if (registrationModel != null)
                {
                    var result = await accountRL.SocialLogin(registrationModel);
                    return true;
                }
                else
                {
                    throw new Exception("Please provide correct data");
                }
            }
            catch (Exception exceptiion)
            {
                throw new Exception(exceptiion.Message);
            }
        }
    }
}
