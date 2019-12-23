using FundooBusinessLayer.InterfaceBL;
using FundooCommonLayer.Model;
using FundooRepositoryLayer.InterfaceRL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.ServiceBL
{
   public class AccountBL:IAccountBL
    {
        private readonly IAccountRL accountRL;

        public AccountBL(IAccountRL accountRl)
        {
            this.accountRL = accountRl;
        }

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
