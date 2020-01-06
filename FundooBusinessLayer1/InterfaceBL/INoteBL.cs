// ******************************************************************************
//  <copyright file="INoteBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  INoteBL.cs
//  
//     Purpose:  Creating note interface for business layer
//     @author  Pranali Patil
//     @version 1.0
//     @since   21-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooBusinessLayer.InterfaceBL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Response;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// creating note interface for business layer
    /// </summary>
    public interface INoteBL
    {
        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="requestNote">The request note.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns message indicating operation is succcessful or not</returns>
        Task<bool> CreateNote(NoteRequest requestNote, string userID);

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the list of note</returns>
        IList<NoteResponse> DisplayNotes(string userID);

        Task<NoteResponse> GetNote(int noteID, string userID);

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the info of label</returns>
        Task<NoteModel> UpdateNote(NoteRequest noteRequest, int noteID, string userID);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns message indicating operation is done or not</returns>
        Task<bool> DeleteNote(int noteID, string userID);

        /// <summary>
        /// Determines whether the specified note identifier is archive.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="archive">if set to <c>true</c> [archive].</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns true indicating note is Archived or false to indicate note is UnArchived </returns>
        Task<NoteResponse> IsArchive(int noteID, string userID);

        /// <summary>
        /// Gets the archived notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns list of archived notes</returns>
        IList<NoteResponse> GetArchivedNotes(string userID);

        /// <summary>
        /// Determines whether the specified note identifier is pin.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>  returns true indicating note is Pinned or false to indicate note is UnPinned</returns>
        Task<NoteResponse> IsPin(int noteID, string userID);

        /// <summary>
        /// Gets the pinned notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns list of pinned notes</returns>
        IList<NoteResponse> GetPinnedNotes(string userID);

        /// <summary>
        /// Moves to trash.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the operation result</returns>
        Task<bool> MoveToTrash(int noteID, string userID);

        /// <summary>
        /// Gets the notes from trash.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the note info from trash</returns>
        IList<NoteResponse> GetNotesFromTrash(string userID);

        /// <summary>
        /// Restores from trash.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the operation result</returns>
        Task<NoteResponse> RestoreNote(int noteId, string userID);

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the operation result</returns>
        Task<NoteResponse> ChangeColor(int noteID, string color, string userID);

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the operation result</returns>
        Task<NoteResponse> SetReminder(int noteID, DateTime dateTime, string userID);

        /// <summary>
        /// Removes the reminder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the operation result</returns>
        Task<NoteResponse> RemoveReminder(int noteId, string userID);

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="formFile">The form file.</param>
        /// <returns> returns the operation result</returns>
        Task<NoteResponse> ImageUpload(int noteID, string userID, IFormFile file);

        /// <summary>
        /// Searches the specified kay.
        /// </summary>
        /// <param name="kay">The kay.</param>
        /// <returns>returns the list of notes or null value</returns>
        IList<NoteResponse> Search(string kay, string userID);

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> returns true or false depending upon operation result</returns>
        Task<bool> BulkTrash(string userId);

        Dictionary<string, string> GetContacts(string key, string userID);
    }
}
