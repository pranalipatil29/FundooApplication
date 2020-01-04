// ******************************************************************************
//  <copyright file="AdminRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AdminRL.cs
//  
//     Purpose:  Implementing different methods for admin
//     @author  Pranali Patil
//     @version 1.0
//     @since   3-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooRepositoryLayer.ServiceRL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.Context;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
 
    public class AdminRL :IAdminRL
    {
        private readonly UserManager<ApplicationModel> userManager;

        private readonly SignInManager<ApplicationModel> signinManager;

        private readonly ApplicationSetting applicationSettings;

        private AuthenticationContext authenticationContext;


        public AdminRL(UserManager<ApplicationModel> userManager, SignInManager<ApplicationModel> signInManager, IOptions<ApplicationSetting> appSettings, AuthenticationContext authenticationContext)
        {
            this.userManager = userManager;
            this.signinManager = signInManager;
            this.applicationSettings = appSettings.Value;
            this.authenticationContext = authenticationContext;
        }

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
                    UserType = 1,
                    ServiceType = "Advance",
                    ProfilePicture=null
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

        public async Task<AccountResponse> Login(LoginModel loginModel)
        {
            try
            {
                var user = await this.userManager.FindByEmailAsync(loginModel.EmailId);

                var userPassword =await this.userManager.CheckPasswordAsync(user, loginModel.Password);

                if(user != null && userPassword && user.UserType == 1)
                {
                    // get the required user data 
                    var data = new AccountResponse()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailID = user.Email,
                        UserName = user.UserName,
                        Profilepicture = user.ProfilePicture
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
                throw new Exception(exception.Message);
            }
        }

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

        public Dictionary<string, int> GetUserStatistics()
        {
            try
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>();

                var basic = this.authenticationContext.UserDataTable.Where(s => s.ServiceType == "Basic" && s.UserType == 0);

                var advance = this.authenticationContext.UserDataTable.Where(s => s.ServiceType == "Advance" && s.UserType == 0);

                dictionary.Add("Basic", basic.Count());
                dictionary.Add("Advance", advance.Count());

                return dictionary;

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

    }
}
