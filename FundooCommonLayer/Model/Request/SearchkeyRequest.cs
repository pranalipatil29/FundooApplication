// ******************************************************************************
//  <copyright file="SearchkeyRequest.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  SearchkeyRequest.cs
//  
//     Purpose:  Request for key tobe search
//     @author  Pranali Patil
//     @version 1.0
//     @since  6-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model.Request
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// this class contains property for key tobe search
    /// </summary>
    public class SearchkeyRequest
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [Required]
        public string Key { get; set; }
    }
}
