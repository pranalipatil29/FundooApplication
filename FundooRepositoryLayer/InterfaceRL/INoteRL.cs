﻿// ******************************************************************************
//  <copyright file="INoteRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  INoteRL.cs
//  
//     Purpose:  Creating note interface for repository layer
//     @author  Pranali Patil
//     @version 1.0
//     @since   21-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooRepositoryLayer.InterfaceRL
{
    // Including the requried assemblies in to the program
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Response;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// creating note interface for repository layer
    /// </summary>
    public interface INoteRL
    {
        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns message indicating operation is done or not</returns>
        Task<bool> CreateNote(NoteRequest noteRequest, string userID);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns message indicating operation is done or not</returns>
        Task<bool> DeleteNote(int noteID, string userID);

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the info of label</returns>
        Task<NoteModel> UpdateNote(NoteRequest noteRequest, int noteID, string userID);

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the list of note</returns>
        IList<NoteResponse> DisplayNotes(string userID);
    }
}
