// ******************************************************************************
//  <copyright file="ApplicationModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ApplicationModel.cs
//  
//     Purpose:  Creating extra columns for user table
//     @author  Pranali Patil
//     @version 1.0
//     @since   14-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model
{
    // Including the requried assemblies in to the program
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// this class is used to define Application model
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser" />
    public class ApplicationModel : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is facebook.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is facebook; otherwise, <c>false</c>.
        /// </value>
        public bool IsFacebook { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is google.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is google; otherwise, <c>false</c>.
        /// </value>
        public bool IsGoogle { get; set; }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        [Column(TypeName = "int")]
        public int UserType { get; set; }

        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        [ForeignKey("ServiceModel")]
        public string ServiceType { get; set; }
    }
}
