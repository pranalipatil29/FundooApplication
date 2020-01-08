// ******************************************************************************
//  <copyright file="LabelResponse.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  LabelResponse.cs
//  
//     Purpose:  Defining properties for handling label response
//     @author  Pranali Patil
//     @version 1.0
//     @since   24-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model.Response
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Defining properties for handling label response
    /// </summary>
    public class LabelResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }
    }
}