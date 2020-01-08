// ******************************************************************************
//  <copyright file="NoteLabelModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  NoteLabelModel.cs
//  
//     Purpose:  Creating columns for note and label table
//     @author  Pranali Patil
//     @version 1.0
//     @since   20-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model
{
    using System;
    // Including the requried assemblies in to the program
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// this class is used to create table for label and note
    /// </summary>
    public class NoteLabelModel
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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [ForeignKey("RegistrationModel")]
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        [ForeignKey("NoteModel")]
        public int NoteID { get; set; }

        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        /// <value>
        /// The label identifier.
        /// </value>
        [ForeignKey("LableModel")]
        public int LabelID { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string Label { get; set; }

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
