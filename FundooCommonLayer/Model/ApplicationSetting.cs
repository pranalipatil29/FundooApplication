// ******************************************************************************
//  <copyright file="ApplicationSetting.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ApplicationSetting.cs
//  
//     Purpose: Get or set secret key and client url
//     @author  Pranali Patil
//     @version 1.0
//     @since   14-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model
{
    /// <summary>
    /// this class is used to get or set the secret key and client url
    /// </summary>
    public class ApplicationSetting
    {
        /// <summary>
        /// Gets or sets the JWT secret.
        /// </summary>
        /// <value>
        /// The JWT secret.
        /// </value>
        public string JWTSecret { get; set; }

        /// <summary>
        /// Gets or sets the client URL.
        /// </summary>
        /// <value>
        /// The client URL.
        /// </value>
        public string ClientURL { get; set; }


        public string CloudName { get; set; }

        public string APIkey { get; set; }

        public string APISecret { get; set; }

        public string FacebookAppId { get; set; }

        public string FacebookAppSecret { get; set; }
    }
}
