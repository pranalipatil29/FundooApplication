// ******************************************************************************
//  <copyright file="AccountRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AccountRL.cs
//  
//     Purpose:  Implementing login, registartion,reset password and forget password methods
//     @author  Pranali Patil
//     @version 1.0
//     @since   14-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooRepositoryLayer.ServiceRL
{
    // Including the requried assemblies in to the program
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Response;
    using FundooCommonLayer.MSMQ;
    using FundooRepositoryLayer.ImageUpload;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
  
    /// <summary>
    /// this class contains different methods to interact with database
    /// </summary>
    public class AccountRL : IAccountRL
    {
        /// <summary>
        /// creating reference of User manager
        /// </summary>
        private readonly UserManager<ApplicationModel> userManager;

        /// <summary>
        /// creating reference of sign in manager
        /// </summary>
        private readonly SignInManager<ApplicationModel> signinManager;

        /// <summary>
        /// creating reference of application settings class
        /// </summary>
        private readonly ApplicationSetting applicationSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRL"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="appSettings">The application settings.</param>
        public AccountRL(UserManager<ApplicationModel> userManager, SignInManager<ApplicationModel> signInManager, IOptions<ApplicationSetting> appSettings)
        {
            this.userManager = userManager;
            this.signinManager = signInManager;
            this.applicationSettings = appSettings.Value;
        }

        /// <summary>
        /// this method is used to store new account info into database
        /// </summary>
        /// <param name="registrationModel"> injecting registration model</param>
        /// <returns> returns message indication whether operation is successful or not</returns>
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
                    UserType = 0,
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
        /// <param name="loginModel"> injecting login model</param>
        /// <returns> returns message indication whether operation is successful or not</returns>
        public async Task<AccountResponse> LogIn(LoginModel loginModel)
        {
            try
            {
                // check whether the user entered email id is present in datbase
                var user = await this.userManager.FindByEmailAsync(loginModel.EmailId);

                // check whether the user entered password is matching
                var userPassword = await this.userManager.CheckPasswordAsync(user, loginModel.Password);

                // check whether user data is null or not or user password is correct 
                if (user != null && userPassword)
                {
                    // get the required user data 
                    var data = new AccountResponse()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailID = user.Email,
                        UserName = user.UserName,
                        Profilepicture= user.ProfilePicture,
                        ServiceType =user.ServiceType
                    };

                    // return the user data
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message) ;
            }           
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="loginResponse">The login response.</param>
        /// <returns> returns the token</returns>
        public async Task<string> GenerateToken(AccountResponse accountResponse)
        {
            var user = await this.userManager.FindByEmailAsync(accountResponse.EmailID);
          
            // check whether user email id and password is found or not 
            if (user != null)
            {
                // creating token for specific email id
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim("EmailID", user.Email.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.applicationSettings.JWTSecret)), SecurityAlgorithms.HmacSha256Signature)
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
        /// this method is used to perform operations for forget password API
        /// </summary>
        /// <param name="forgetPasswordModel"> injecting forget password model</param>
        /// <returns> returns message indication whether operation is successful or not</returns>
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
                      new Claim("EmailID", user.Email.ToString())
                   }),

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.applicationSettings.JWTSecret)), SecurityAlgorithms.HmacSha256Signature)
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
        /// this class is used to perform operations for reset password API
        /// </summary>
        /// <param name="resetPasswordModel"> injecting reset password model</param>
        /// <returns> returns message indication whether operation is successful or not</returns>
        public async Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            // get the token
            var token = new JwtSecurityToken(jwtEncodedString: resetPasswordModel.Token);

            // get the emailid form token
            var email = token.Claims.First(c => c.Type == "EmailID").Value;

            // check whether emailid is present in database or not
            var user = await this.userManager.FindByEmailAsync(email);

            // if email id is present in database then reset the password
            if (email != null)
            {
                // generate the new token for user emailid and new password 
                var resetToken = await this.userManager.GeneratePasswordResetTokenAsync(user);

                // reset the new password
                var result = await this.userManager.ResetPasswordAsync(user, resetToken, resetPasswordModel.Password);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<AccountResponse> ChangeProfilePicture(string emailID, IFormFile formFile)
        {
            try
            {
                // check whether user data already exist in the database or not
                var user = await this.userManager.FindByEmailAsync(emailID);

                // if user is exist or not
                if (user != null)
                {
                    // send the API key,API secret key and cloud name to Upload Image class constructor
                    UploadImage imageUpload = new UploadImage(this.applicationSettings.APIkey, this.applicationSettings.APISecret, this.applicationSettings.CloudName);

                    // get the image url
                    var url = imageUpload.Upload(formFile);

                    // set the image to note
                    user.ProfilePicture = url;
                    var result = await this.userManager.UpdateAsync(user);

                    // get the required user data 
                    var data = new AccountResponse()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailID = user.Email,
                        UserName = user.UserName,
                        Profilepicture= user.ProfilePicture,
                        ServiceType= user.ServiceType
                    };

                    // returning user data
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}