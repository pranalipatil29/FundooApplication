// ******************************************************************************
//  <copyright file="keyRequest.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  keyRequest.cs
//  
//     Purpose:  Request for key tobe search
//     @author  Pranali Patil
//     @version 1.0
//     @since  6-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model.Request.Note
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// this class contains property for key tobe search
    /// </summary>
    public class keyRequest
    {
        /// <summary>
        /// The key
        /// </summary>
        [Required]
        public string key;
    }
}
