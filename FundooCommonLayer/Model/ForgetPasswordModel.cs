// ******************************************************************************
//  <copyright file="ForgetPasswordModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ForgetPasswordModel.cs
//  
//     Purpose:  Creating forget password model
//     @author  Pranali Patil
//     @version 1.0
//     @since   17-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model
{
     /// <summary>
    /// this class is used to get or set the properties for forget password API
    /// </summary>
    public class ForgetPasswordModel
    {
        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>
        /// The email identifier.
        /// </value>
        public string EmailID { get; set; }
    }
}
