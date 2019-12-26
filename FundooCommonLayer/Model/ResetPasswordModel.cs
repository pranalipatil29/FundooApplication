// ******************************************************************************
//  <copyright file="ResetPasswordModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ResetPasswordModel.cs
//  
//     Purpose:  Creating reset password model
//     @author  Pranali Patil
//     @version 1.0
//     @since   18-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;


    /// <summary>
    /// this class is used for get or set properties for reset password model
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }
    }
}
