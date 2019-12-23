using FundooCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer1.InterfaceBL
{
    public interface IAccountBL
    {
       
        Task<bool> Register(RegistrationModel registrationModel);

        
        Task<string> Login(LoginModel loginModel);

      
        Task<bool> ForgetPassword(ForgetPasswordModel forgetPasswordModel);

      
        Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel);

       
        Task<bool> SocialLogin(RegistrationModel registrationModel);
    }
}
