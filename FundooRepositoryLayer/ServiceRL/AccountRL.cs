using FundooCommonLayer.Model;
using FundooCommonLayer.MSMQ;
using FundooRepositoryLayer.InterfaceRL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.ServiceRL
{
    public class AccountRL : IAccountRL
    {
        /// <summary>
        /// creating private property of User manager
        /// </summary>
        private readonly UserManager<ApplicationModel> userManager;

        /// <summary>
        /// creating private property of sign in manager
        /// </summary>
        private readonly SignInManager<ApplicationModel> signinManager;

        private readonly ApplicationSetting applicationSettings;

        /// <summary>
        /// injecting reference of user manager and sign in manager in this class constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public AccountRL(UserManager<ApplicationModel> userManager, SignInManager<ApplicationModel> signInManager, IOptions<ApplicationSetting> appSettings)
        {
            this.userManager = userManager;
            this.signinManager = signInManager;
            this.applicationSettings = appSettings.Value;
        }

        /// <summary>
        /// this method is used to store new account info into databse
        /// </summary>
        /// <param name="registrationModel"></param>
        /// <returns> returns message indication whether operation is successfull or not</returns>
        public async Task<bool> Register(RegistrationModel registrationModel)
        {
            // check whether user data already exist in the database or not
            var user = await this.userManager.FindByEmailAsync(registrationModel.EmailID);

            // if user record not exist then Register the user info
            if (user == null)
            {
                // geting values for application model properties
                var data = new ApplicationModel()
                {
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName,
                    UserName = registrationModel.UserName,
                    Email = registrationModel.EmailID,
                    UserType = registrationModel.UserType,
                    ServiceType = registrationModel.ServiceType
                };

                // assigning password and info of user into table 
                var result = await this.userManager.CreateAsync(data, registrationModel.Password);

                // check whether result is successed or not
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// this method is used to get the login
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns> returns message indication whether operation is successfull or not</returns>
        public async Task<string> LogIn(LoginModel loginModel)
        {
            // check whether the user entered email id is present in datbase
            var user = await this.userManager.FindByEmailAsync(loginModel.EmailId);

            // check whether the user entered password is matching
            var userPassword = await userManager.CheckPasswordAsync(user, loginModel.Password);

            // check whether user email id and password is found or not 
            if (user != null && userPassword)
            {
                // creating token for specific email id
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationSettings.JWTSecret)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
               
                return token;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// this method is used to perform operations for forget password api
        /// </summary>
        /// <param name="forgetPasswordModel"></param>
        /// <returns> returns message indication whether operation is successfull or not</returns>
        public async Task<bool> ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            // check whether user entered emailid is present in database or not
            var user = await this.userManager.FindByEmailAsync(forgetPasswordModel.EmailID);

            // creating MSMQ sender class object
            MSMQSender msmq = new MSMQSender();

            // check whether user contains its property values or not
            if (user != null)
            {
                // create a new token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                   {
                      new Claim("EmailID",user.Email.ToString())
                   }),

                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationSettings.JWTSecret)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                msmq.SendToQueue(forgetPasswordModel.EmailID, token);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// this class is used to perform operations for reset password api
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <returns> returns message indication whether operation is successfull or not</returns>
        public async Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            // get the token
            var token = new JwtSecurityToken(jwtEncodedString: resetPasswordModel.Token);

            // get the emailid form token
            var email = (token.Claims.First(c => c.Type == "EmailID").Value);

            // check whether emailid is present in database or not
            var user = await userManager.FindByEmailAsync(email);

            // if email id is present in database then reset the password
            if (email != null)
            {
                // generate the new token for user emailid and new password 
                var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

                // reset the new password
                var result = await userManager.ResetPasswordAsync(user, resetToken, resetPasswordModel.Password);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Socials the login.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns></returns>
        public async Task<bool> SocialLogin(RegistrationModel registrationModel)
        {
            // check whether user data already exist in the database or not
            var user = await this.userManager.FindByEmailAsync(registrationModel.EmailID);

            // if user record not exist then Register the user info
            if (user == null)
            {
                // geting values for application model properties
                var data = new ApplicationModel()
                {
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName,
                    UserName = registrationModel.UserName,
                    Email = registrationModel.EmailID,
                    UserType = registrationModel.UserType,
                    IsFacebook = registrationModel.IsFacebook,
                    ServiceType=registrationModel.ServiceType
                    
                };

                // assigning password and info of user into table 
                var result = await this.userManager.CreateAsync(data, registrationModel.Password);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}