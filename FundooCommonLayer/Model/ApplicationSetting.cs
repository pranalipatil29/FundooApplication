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

        /// <summary>
        /// Gets or sets the name of the cloud.
        /// </summary>
        /// <value>
        /// The name of the cloud.
        /// </value>
        public string CloudName { get; set; }

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string APIkey { get; set; }

        /// <summary>
        /// Gets or sets the API secret.
        /// </summary>
        /// <value>
        /// The API secret.
        /// </value>
        public string APISecret { get; set; }

        /// <summary>
        /// Gets or sets the facebook application identifier.
        /// </summary>
        /// <value>
        /// The facebook application identifier.
        /// </value>
        public string FacebookAppId { get; set; }

        /// <summary>
        /// Gets or sets the facebook application secret.
        /// </summary>
        /// <value>
        /// The facebook application secret.
        /// </value>
        public string FacebookAppSecret { get; set; }
    }
}
