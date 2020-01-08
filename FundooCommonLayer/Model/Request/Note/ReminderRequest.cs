// ******************************************************************************
//  <copyright file="ReminderRequest.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ReminderRequest.cs
//  
//     Purpose:  Request for reminder
//     @author  Pranali Patil
//     @version 1.0
//     @since  4-1-2019
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
    /// defining properties for reminder Request
    /// </summary>
    public class ReminderRequest
    {
        /// <summary>
        /// Gets or sets the reminder.
        /// </summary>
        /// <value>
        /// The reminder.
        /// </value>
        [Required]
        public DateTime Reminder { get; set; }
    }
}
