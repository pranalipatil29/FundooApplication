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
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Response;
    using Microsoft.AspNetCore.Http;

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

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the operation result</returns>
        Task<NoteResponse> GetNote(int noteID, string userID);

        /// <summary>
        /// Determines whether the specified note identifier is archive.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>  returns true indicating note is Archived or false to indicate note is UnArchived</returns>
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
        /// <returns> returns true indicating note is Pinned or false to indicate note is UnPinned</returns>
        Task<NoteResponse> IsPin(int noteID, string userID);

        /// <summary>
        /// Gets the pinned notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns list of Pinned notes</returns>
        IList<NoteResponse> GetPinnedNotes(string userID);

        /// <summary>
        /// Moves to trash.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the operation result</returns>
        Task<bool> MoveToTrash(int noteID, string userID);

        /// <summary>
        /// Gets the notes from trash.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the operation result</returns>
        IList<NoteResponse> GetNotesFromTrash(string userID);

        /// <summary>
        /// Restores from trash.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the operation result</returns>
        Task<NoteResponse> RestoreNote(int noteId, string userID);

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the operation result</returns>
        Task<NoteResponse> ChangeColor(int noteID, string color, string userID);

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the operation result</returns>
        Task<NoteResponse> SetReminder(int noteID, DateTime dateTime, string userID);

        /// <summary>
        /// Removes the reminder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the operation result</returns>
        Task<NoteResponse> RemoveReminder(int noteId, string userID);

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="file">The form file.</param>
        /// <returns>returns the operation result</returns>
        Task<NoteResponse> ImageUpload(int noteID, string userID, IFormFile file);

        /// <summary>
        /// Searches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the list of notes or null value</returns>
        IList<NoteResponse> Search(string key, string userID);

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        Task<bool> BulkTrash(string userID);

        /// <summary>
        /// Gets the contacts.
        /// </summary>
        /// <param name="key">The key tobe searched.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the list of contacts or null value</returns>
        Dictionary<string, string> GetContacts(string key, string userID);

        /// <summary>
        /// Shares the with.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns true or false depending upon operation result</returns>
        Task<bool> ShareWith(int noteID, string id, string userID);

        /// <summary>
        /// Deletes the collaborator.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns true or false depending upon operation result</returns>
        Task<bool> DeleteCollaborator(int noteID, string id, string userID);

        /// <summary>
        /// Removes the image.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the note info or null value</returns>
        Task<NoteResponse> RemoveImage(int noteID, string userID);

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the note info</returns>
        Task<NoteResponse> AddLabel(int labelID, int noteID, string userID);

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns note info or null value</returns>
        Task<NoteResponse> RemoveLabel(int noteID, int labelID, string userID);
    }
}
