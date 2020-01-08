// ******************************************************************************
//  <copyright file="NoteResponse.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  NoteResponse.cs
//  
//     Purpose:  Defining properties for handling note response
//     @author  Pranali Patil
//     @version 1.0
//     @since   24-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model.Response
{
    using FundooCommonLayer.Model.Response.Note;
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// this class contains properties of note
    /// </summary>
    public class NoteResponse
    {
        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        public int NoteID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the reminder.
        /// </summary>
        /// <value>
        /// The reminder.
        /// </value>
        public DateTime? Reminder { get; set; }

        /// <summary>
        /// Gets or sets the collaborator.
        /// </summary>
        /// <value>
        /// The collaborator.
        /// </value>
        public int Collaborator { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is archive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is archive; otherwise, <c>false</c>.
        /// </value>
        public bool IsArchive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pin; otherwise, <c>false</c>.
        /// </value>
        public bool IsPin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is trash.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is trash; otherwise, <c>false</c>.
        /// </value>
        public bool IsTrash { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the collaborators.
        /// </summary>
        /// <value>
        /// The collaborators.
        /// </value>
        public List<CollaboratorRsponse> Collaborators { get; set; }

        public List<LabelResponse> Labels { get; set; }
    }
}
