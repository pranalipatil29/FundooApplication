﻿// ******************************************************************************
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
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.InterfaceRL;

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
                    return await this.noteRL.DeleteNote(noteID, userID);
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
        /// Determines whether the specified note identifier is archive.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="archive">if set to <c>true</c> [archive].</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns true indicating note is Archived or false to indicate note is UnArchived
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter correct note id
        /// or
        /// </exception>
        public async Task<bool> IsArchive(int noteID, bool archive, string userID)
        {
            try
            {
                // ckeck whether user entered correct note id or not
                if(noteID > 0)
                {
                    // if user entered correct note id then pass that id, archive value and user id to repository layer method
                    return await this.noteRL.IsArchive(noteID, archive, userID);
                }
                else
                {
                    // if user entered wrong id then throw exception
                    throw new Exception("Please enter correct note id");
                }
            }
            catch(Exception exception)
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
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is pin.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="isPin">if set to <c>true</c> [is pin].</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns true indicating note is Pinned or false to indicate note is UnPinned
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter correct Note ID
        /// or
        /// </exception>
        public async Task<bool> IsPin(int noteID, bool isPin, string userID)
        {
            try
            {
                // ckeck whether user entered correct note id or not
                if (noteID > 0)
                {
                    // if user entered correct note id then pass that id, Pin value and user id to repository layer method
                    return await this.noteRL.IsPin(noteID, isPin, userID);
                }
                else
                {
                    // if user entered wrong id then throw exception
                    throw new Exception("Please enter correct Note ID");
                }
            }
            catch(Exception exception)
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
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> IsTrash(int noteID, bool isTrash, string userID)
        {
            try
            {
                if(noteID > 0)
                {
                    return await this.noteRL.IsTrash(noteID, isTrash, userID);
                }
                else
                {
                    throw new Exception("Please enter correct Note Id");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
