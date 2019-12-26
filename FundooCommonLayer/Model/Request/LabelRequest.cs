// ******************************************************************************
//  <copyright file="LabelRequest.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  LabelRequest.cs
//  
//     Purpose:  Geting request for label info
//     @author  Pranali Patil
//     @version 1.0
//     @since   24-12-2019
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
    /// this class defines the properties for label 
    /// </summary>
    public class LabelRequest
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        [Required]
        public string Label { get; set; }
    }
}
