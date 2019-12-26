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
    using FundooBusinessLayer.InterfaceBL;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.InterfaceRL;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this class is used to check the business logic of note
    /// </summary>
    /// <seealso cref="FundooBusinessLayer.InterfaceBL.INoteBL" />
    public class NoteBL:INoteBL
    {
        /// <summary>
        /// creating reference of repository layer interface
        /// </summary>
        public readonly INoteRL noteRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteBL"/> class.
        /// </summary>
        /// <param name="noteRL">The note rl.</param>
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<bool> CreateNote(NoteRequest noteRequest, string userID)
        {
            try
            {
                if (noteRequest != null)
                {
                    return await noteRL.CreateNote(noteRequest, userID);
                }
                else
                {
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
        /// Pleaase enter Note ID
        /// or
        /// </exception>
        public async Task<bool> DeleteNote(int noteID, string userID)
        {
            try
            {
                if (noteID != 0)
                {
                    return await noteRL.DeleteNote(noteID,userID);
                }
                else
                {
                    throw new Exception("Pleaase enter Note ID");
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
        /// Pleaase enter Note ID
        /// or
        /// </exception>
        public async Task<NoteModel> UpdateNote(NoteRequest noteRequest, int noteID, string userID)
        {
            try
            {
                if(noteID != 0)
                {
                    return await noteRL.UpdateNote(noteRequest, noteID, userID);
                }
                else
                {
                    throw new Exception("Pleaase enter Note ID");
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
                if(userID != null)
                {
                    return noteRL.DisplayNotes(userID);
                }
                else
                {
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
