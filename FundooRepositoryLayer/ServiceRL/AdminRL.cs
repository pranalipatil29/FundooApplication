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
    using FundooRepositoryLayer.ImageUpload;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// this class is used to implement the repository layer Admin interface
    /// </summary>
    /// <seealso cref="FundooRepositoryLayer.InterfaceRL.IAdminRL" />
    public class AdminRL : IAdminRL
    {
        /// <summary>
        /// The reference of user manager
        /// </summary>
        private readonly UserManager<ApplicationModel> userManager;

        /// <summary>
        /// The reference of sign-in manager
        /// </summary>
        private readonly SignInManager<ApplicationModel> signinManager;

        /// <summary>
        /// creating reference of application settings
        /// </summary>
        private readonly ApplicationSetting applicationSettings;

        /// <summary>
        /// creating reference of authentication context
        /// </summary>
        private AuthenticationContext authenticationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminRL"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="authenticationContext">The authentication context.</param>
        public AdminRL(UserManager<ApplicationModel> userManager, SignInManager<ApplicationModel> signInManager, IOptions<ApplicationSetting> appSettings, AuthenticationContext authenticationContext)
        {
            this.userManager = userManager;
            this.signinManager = signInManager;
            this.applicationSettings = appSettings.Value;
            this.authenticationContext = authenticationContext;
        }

        /// <summary>
        /// Registers the specified registration model.
        /// </summary>
        /// <param name="registrationModel">The registration model.</param>
        /// <returns>
        /// returns the true or false based on operation result
        /// </returns>
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
                    ProfilePicture = null
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
                // if user record is present in database then throw exception
                throw new Exception("User record is Already Registered");
            }
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>
        /// returns the user info if user gets logged in
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<AccountResponse> Login(LoginModel loginModel)
        {
            try
            {
                // find the user info from user table
                var user = await this.userManager.FindByEmailAsync(loginModel.EmailId);

                // check the password entered by user is correct or not
                var userPassword = await this.userManager.CheckPasswordAsync(user, loginModel.Password);

                // check wheather user is present in database and password is correct or not
                if (user != null && userPassword && user.UserType == 1)
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
    
                // return the token value
                return token;
            }
            else
            {
                return null;
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
                // create the dictionary object to hold the count of basic and Advance service type users
                Dictionary<string, int> dictionary = new Dictionary<string, int>();

                // get the users which have Basic type service from user table
                var basic = this.authenticationContext.UserDataTable.Where(s => s.ServiceType == "Basic" && s.UserType == 0);

                // get the users which have Advance type service from user Table
                var advance = this.authenticationContext.UserDataTable.Where(s => s.ServiceType == "Advance" && s.UserType == 0);

                // add the count of users into dictionary
                dictionary.Add("Basic Service", basic.Count());
                dictionary.Add("Advance service", advance.Count());

                // return the dictionary object
                return dictionary;
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
                // get the users info from user table
                var users = this.authenticationContext.UserDataTable.Where(s => s.UserType == 0);
                var list = new List<AccountResponse>();

                // check wheather user table contains any user record or not
                if (users != null)
                {
                    // iterate the loop for all users
                    foreach (var data in users)
                    {
                         // get the user info
                        var user = new AccountResponse()
                        {
                            FirstName = data.FirstName,
                            LastName = data.LastName,
                            UserName = data.UserName,
                            EmailID = data.Email,
                            Profilepicture = data.ProfilePicture,
                            ServiceType = data.ServiceType
                        };

                        // add the user info into list
                        list.Add(user);
                    }

                    // return the list which holds info of users
                    return list;
                }
                else
                {
                    // if user table doesn't contain any user record then return null
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Searches the user.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns> returns the list of users info </returns>
        /// <exception cref="Exception"> exception message</exception>
        public IList<AccountResponse> SearchUser(string key)
        {
            try
            {
                // find the user which name or email ID contains admin entered key
                var users = this.authenticationContext.UserDataTable.Where(s => (s.FirstName.Contains(key) || s.LastName.Contains(key) || s.Email.Contains(key)) && s.UserType == 0);
                var list = new List<AccountResponse>();

                // check wheather any record is found or not
                if (users != null)
                {
                    // iterates the loop for all users
                    foreach (var data in users)
                    {
                        // get the user info
                        var user = new AccountResponse()
                        {
                            FirstName = data.FirstName,
                            LastName = data.LastName,
                            UserName = data.UserName,
                            EmailID = data.Email,
                            Profilepicture = data.ProfilePicture,
                            ServiceType = data.ServiceType
                        };

                        // add the user info into list
                        list.Add(user);
                    }

                    // return the list of user info
                    return list;
                }
                else
                {
                    // if no record found for admin entered key then return null
                    return null;
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
        /// <param name="file">The form file.</param>
        /// <returns> returns the admin info or null value</returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<AccountResponse> ChangeProfilePicture(string emailID, IFormFile file)
        {
            try
            {
                // check whether admin data already exist in the database or not
                var user = await this.userManager.FindByEmailAsync(emailID);

                // if admin is exist or not
                if (user != null)
                {
                    // send the API key,API secret key and cloud name to Upload Image class constructor
                    UploadImage imageUpload = new UploadImage(this.applicationSettings.APIkey, this.applicationSettings.APISecret, this.applicationSettings.CloudName);

                    // get the image url
                    var url = imageUpload.Upload(file);

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
                        Profilepicture = user.ProfilePicture,
                        ServiceType = user.ServiceType
                    };

                    // returning admin data
                    return data;
                }
                else
                {
                    // if admin data not found in database return null
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
