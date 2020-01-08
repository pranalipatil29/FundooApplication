// ******************************************************************************
//  <copyright file="CollaboratorModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  CollaboratorModel.cs
//  
//     Purpose:  Creating columns for collaborator table
//     @author  Pranali Patil
//     @version 1.0
//     @since  6-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// Defining columns for Collaborator table
    /// </summary>
    public class CollaboratorModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        [ForeignKey("NoteModel")]
        public int NoteID { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [ForeignKey("RegistrationModel")]
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the collaborator identifier.
        /// </summary>
        /// <value>
        /// The collaborator identifier.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string CollaboratorID { get; set; }

        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>
        /// The email identifier.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string EmailID { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        [Column(TypeName = "DateTime")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        [Column(TypeName = "DateTime")]
        public DateTime ModifiedDate { get; set; }
    }
}
