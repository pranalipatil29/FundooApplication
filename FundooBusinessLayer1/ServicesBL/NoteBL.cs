// ******************************************************************************
//  <copyright file="NoteBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  NoteBL.cs
//  
//     Purpose:  Implementing business logic for Note
//     @author  Pranali Patil
//     @version 1.0
//     @since   21-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooBusinessLayer.ServicesBL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooBusinessLayer.InterfaceBL;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Request.Note;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// this class is used to check the business logic of note
    /// </summary>
    /// <seealso cref="FundooBusinessLayer.InterfaceBL.INoteBL" />
    public class NoteBL : INoteBL
    {
        /// <summary>
        /// creating reference of repository layer interface
        /// </summary>
        private readonly INoteRL noteRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteBL"/> class.
        /// </summary>
        /// <param name="noteRL">The reference of repository layer note class.</param>
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the message indicating whether operation is successful or not</returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<bool> CreateNote(NoteRequest noteRequest, string userID)
        {
            try
            {
                // check whether user entered null data or not
                if (noteRequest != null)
                {
                    // if user entered corrrect data then pass user entered data and user id to repository layer method to create note
                    return await this.noteRL.CreateNote(noteRequest, userID);
                }
                else
                {
                    // if user enter any null data then throw exception
                    throw new Exception("Data Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns message indicating operation is done or not
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter Note ID
        /// or
        /// </exception>
        public async Task<bool> DeleteNote(int noteID, string userID)
        {
            try
            {
                // check whther user enter currect note id or not
                if (noteID > 0)
                {
                    // if user enter correct note id then pass that note id and user id to repository layer method to delete note
                    var result = await this.noteRL.DeleteNote(noteID, userID);
                    return result;
                }
                else
                {
                    // if user enter wrong note id then throw exception
                    throw new Exception("Please enter correct Note ID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the info of label
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter Note ID
        /// or
        /// </exception>
        public async Task<NoteModel> UpdateNote(NoteRequest noteRequest, int noteID, string userID)
        {
            try
            {
                // check whther user enter currect note id or not
                if (noteID > 0)
                {
                    // if user enter correct note id then pass that note id, user entered data and user id to repository layer method to update note info
                    return await this.noteRL.UpdateNote(noteRequest, noteID, userID);
                }
                else
                {
                    // if user enter wrong note id then throw exception
                    throw new Exception("Please enter correct Note ID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the list of note
        /// </returns>
        /// <exception cref="Exception">
        /// User not found
        /// or
        /// </exception>
        public IList<NoteResponse> DisplayNotes(string userID)
        {
            try
            {
                // check whther user id is null or not
                if (userID != null)
                {
                    // if user id is not null then pass that id to repository layer method to display notes of that user
                    return this.noteRL.DisplayNotes(userID);
                }
                else
                {
                    // if user id contains null value then throw exception
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the Note info</returns>
        /// <exception cref="Exception">
        /// User not found
        /// or
        /// </exception>
        public async Task<NoteResponse> GetNote(int noteID, string userID)
        {
            try
            {
                // check whther user id is null or not
                if (userID != null)
                {
                    // if user id is not null then pass that id to repository layer method to display notes of that user
                    return await this.noteRL.GetNote(noteID, userID);
                }
                else
                {
                    // if user id contains null value then throw exception
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is archive.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns true indicating note is Archived or false to indicate note is UnArchived
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter correct note id
        /// or
        /// </exception>
        public async Task<NoteResponse> IsArchive(int noteID, string userID)
        {
            try
            {
                // ckeck whether user entered correct note id or not
                if (noteID > 0)
                {
                    // if user entered correct note id then pass that id, archive value and user id to repository layer method
                    return await this.noteRL.IsArchive(noteID, userID);
                }
                else
                {
                    // if user entered wrong id then throw exception
                    throw new Exception("Please enter correct note id");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Gets the archived notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns list of archived notes
        /// </returns>
        /// <exception cref="Exception">
        /// User not found
        /// or
        /// </exception>
        public IList<NoteResponse> GetArchivedNotes(string userID)
        {
            try
            {
                // check whther user id is null or not
                if (userID != null)
                {
                    // if user id does not contains null value then pass that user id to repository layer method
                    return this.noteRL.GetArchivedNotes(userID);
                }
                else
                {
                    // if user id contains null value then throw exception
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is pin.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns true indicating note is Pinned or false to indicate note is UnPinned
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter correct Note ID
        /// or
        /// </exception>
        public async Task<NoteResponse> IsPin(int noteID, string userID)
        {
            try
            {
                // ckeck whether user entered correct note id or not
                if (noteID > 0)
                {
                    // if user entered correct note id then pass that id, Pin value and user id to repository layer method
                    return await this.noteRL.IsPin(noteID, userID);
                }
                else
                {
                    // if user entered wrong id then throw exception
                    throw new Exception("Please enter correct Note ID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Gets the pinned notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns list of pinned notes
        /// </returns>
        /// <exception cref="Exception">
        /// User not found
        /// or
        /// </exception>
        public IList<NoteResponse> GetPinnedNotes(string userID)
        {
            try
            {
                // check whther user id is null or not
                if (userID != null)
                {
                    // if user id does not contains null value then pass that user id to repository layer method
                    return this.noteRL.GetPinnedNotes(userID);
                }
                else
                {
                    // if user id contains null value then throw exception
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Moves to trash.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter correct Note Id
        /// or
        /// </exception>
        public async Task<bool> MoveToTrash(int noteID, string userID)
        {
            try
            {
                // ckeck whether user entered correct note id or not
                if (noteID > 0)
                {
                    // if user entered correct note id then pass that id, user id to repository layer method
                    return await this.noteRL.MoveToTrash(noteID, userID);
                }
                else
                {
                    throw new Exception("Please enter correct Note Id");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Gets the notes from trash.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the note info from trash
        /// </returns>
        /// <exception cref="Exception">
        /// User not found
        /// or
        /// </exception>
        public IList<NoteResponse> GetNotesFromTrash(string userID)
        {
            try
            {
                // check whther user id is null or not
                if (userID != null)
                {
                    // if user id does not contains null value then pass that user id to repository layer method
                    return this.noteRL.GetNotesFromTrash(userID);
                }
                else
                {
                    // if user id contains null value then throw exception
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Restores from trash.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter correct NoteID
        /// or
        /// </exception>
        public async Task<NoteResponse> RestoreNote(int noteId, string userID)
        {
            try
            {
                // ckeck whether user entered correct note id or not
                if (noteId > 0)
                {
                    // if user entered correct note id then pass that id and user id to repository layer method
                    return await this.noteRL.RestoreNote(noteId, userID);
                }
                else
                {
                    throw new Exception("Please enter correct NoteID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter correct NoteID
        /// or
        /// </exception>
        public async Task<NoteResponse> ChangeColor(int noteID, string color, string userID)
        {
            try
            {
                // ckeck whether user entered correct note id or not
                if (noteID > 0)
                {
                    // if user entered correct note id then pass that id, color and user id to repository layer method
                    return await this.noteRL.ChangeColor(noteID, color, userID);
                }
                else
                {
                    throw new Exception("Please enter correct NoteID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter correct NoteID
        /// or
        /// </exception>
        public async Task<NoteResponse> SetReminder(int noteID, DateTime dateTime, string userID)
        {
            try
            {
                // ckeck whether user entered correct note id or not
                if (noteID > 0)
                {
                    // if user entered correct note id then pass that id, date and time value and user id to repository layer method
                    return await this.noteRL.SetReminder(noteID, dateTime, userID);
                }
                else
                {
                    throw new Exception("Please enter correct NoteID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Removes the reminder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter correct NoteID
        /// or
        /// </exception>
        public async Task<NoteResponse> RemoveReminder(int noteId, string userID)
        {
            try
            {
                // ckeck whether user entered correct note id or not
                if (noteId > 0)
                {
                    // if user entered correct note id then pass that id and user id to repository layer method
                    return await this.noteRL.RemoveReminder(noteId, userID);
                }
                else
                {
                    throw new Exception("Please enter correct NoteID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="file">The form file.</param>
        /// <returns>
        /// returns the operation result
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter correct noteID
        /// or
        /// </exception>
        public async Task<NoteResponse> ImageUpload(int noteID, string userID, IFormFile file)
        {
            try
            {
                // ckeck whether user entered correct note id or not
                if (noteID > 0)
                {
                    // if user entered correct note id then pass that id, user id and image to repository layer method
                    return await this.noteRL.ImageUpload(noteID, userID, file);
                }
                else
                {
                    // if user entered wrong note id throw the exception
                    throw new Exception("Please enter correct noteID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Removes the image.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the note info or null value</returns>
        /// <exception cref="Exception">
        /// Please enter correct NoteID
        /// or
        /// </exception>
        public async Task<NoteResponse> RemoveImage(int noteID, string userID)
        {
            try
            {
                // ckeck whether user entered correct note id or not
                if (noteID > 0)
                {
                    return await this.noteRL.RemoveImage(noteID, userID);
                }
                else
                {
                    // if user entered wrong note id throw the exception
                    throw new Exception("Please enter correct NoteID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Searches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> returns the Note list</returns>
        /// <exception cref="Exception">
        /// Key required to search
        /// or
        /// </exception>
        public IList<NoteResponse> Search(string key, string userId)
        {
            try
            {
                // check whether user entered any value or not
                if (key != null)
                {
                    // get the result of search opertion and return it
                    return this.noteRL.Search(key, userId);
                }
                else
                {
                    // if user not entered any key throw the exception
                    throw new Exception("Key required to search");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">
        /// User not found
        /// or
        /// </exception>
        public async Task<bool> BulkTrash(string userId)
        {
            try
            {
                // check wheather user id is null or not 
                if (userId != null)
                {
                    // if user id found then perform the operation and return the result of operation
                    return await this.noteRL.BulkTrash(userId);
                }
                else
                {
                    // if user not found then throw the exception
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Gets the contacts.
        /// </summary>
        /// <param name="key">The key tobe searched.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the list of contacts or null value
        /// </returns>
        /// <exception cref="Exception">
        /// EmailID or UserId is required
        /// or
        /// </exception>
        public Dictionary<string, string> GetContacts(string key, string userID)
        {
            try
            {
                // check wheather the user id contains any null value or not
                if (key != null)
                {
                    // return the list of persons which contains the user entered key
                    return this.noteRL.GetContacts(key, userID);
                }
                else
                {
                    // if user id contains null value then throw the exception
                    throw new Exception("EmailID or UserId is required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Shares the with.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">
        /// NoteId and user id is required
        /// or
        /// </exception>
        public async Task<bool> ShareWith(int noteID, string id, string userID)
        {
            try
            {
                if (id != null && noteID > 0)
                {
                    return await this.noteRL.ShareWith(noteID, id, userID);
                }
                else
                {
                    throw new Exception("NoteId and user id is required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the collaborator.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns true or false depending upon operation result
        /// </returns>
        /// <exception cref="Exception">
        /// NoteId and user id is required
        /// or
        /// </exception>
        public async Task<bool> DeleteCollaborator(int noteID, string id, string userID)
        {
            try
            {
                // check wheather user entered any null value or not
                if (id != null && noteID > 0)
                {
                    return await this.noteRL.DeleteCollaborator(noteID, id, userID);
                }
                else
                {
                    // if user entered any null value then throw the exception
                    throw new Exception("NoteId and user id is required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> return the note info or null value</returns>
        /// <exception cref="Exception">
        /// Note ID or labelID is Invalid
        /// or
        /// </exception>
        public async Task<NoteResponse> AddLabel(int labelID, int noteID, string userID)
        {
            try
            {
                // check wheather user entered correct label id and note id
                if (noteID > 0 && labelID > 0)
                {
                    return await this.noteRL.AddLabel(labelID, noteID, userID);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the note info or null value</returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<NoteResponse> RemoveLabel(int noteID, int labelID, string userID)
        {
            try
            {
                // check wheather uer entered correct note ID and Label ID
                if (noteID > 0 && labelID > 0)
                {
                    return await this.noteRL.RemoveLabel(noteID, labelID, userID);
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}