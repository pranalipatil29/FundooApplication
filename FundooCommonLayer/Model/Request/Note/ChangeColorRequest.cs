// ******************************************************************************
//  <copyright file="ChangeColorRequest.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ChangeColorRequest.cs
//  
//     Purpose:  Request for note color
//     @author  Pranali Patil
//     @version 1.0
//     @since  1-1-2020
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
    /// this class defines the property for note color
    /// </summary>
    public class ChangeColorRequest
    {
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [Required]
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
        public string Color { get; set; }
    }
}
