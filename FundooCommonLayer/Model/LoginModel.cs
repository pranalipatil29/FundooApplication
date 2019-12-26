// ******************************************************************************
//  <copyright file="LoginModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  LoginModel.cs
//  
//     Purpose:  Creating Login model
//     @author  Pranali Patil
//     @version 1.0
//     @since   16-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// this class is used to define properties for login model
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>
        /// The email identifier.
        /// </value>
        [Required]
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }
    }
}
