// ******************************************************************************
//  <copyright file="NoteRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  NoteRL.cs
//  
//     Purpose:  Create, update,delete and display note
//     @author  Pranali Patil
//     @version 1.0
//     @since   21-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooRepositoryLayer.ServiceRL
{
    // Including the requried assemblies in to the program
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.Context;
    using FundooRepositoryLayer.InterfaceRL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    ///  this class contains differnrt methods to intract with note tabel
    /// </summary>
    /// <seealso cref="FundooRepositoryLayer.InterfaceRL.INoteRL" />
    public class NoteRL : INoteRL
    {
        /// <summary>
        /// creating reference of authentication context class
        /// </summary>
        private AuthenticationContext authenticationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRL"/> class.
        /// </summary>
        /// <param name="authenticationContext">The authentication context.</param>
        public NoteRL(AuthenticationContext authenticationContext)
        {
            this.authenticationContext = authenticationContext;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="requestNote">The request note.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> CreateNote(NoteRequest requestNote, string userID)
        {
            try
            {
                // check whether user enter all the required data or not
                if (requestNote != null)
                {
                    // set the user entered values
                    var data = new NoteModel()
                    {
                        UserID = userID,
                        Title = requestNote.Title,
                        Description = requestNote.Description,
                        Reminder = requestNote.Reminder,
                        Collaborator = requestNote.Collaborator,
                        Color = requestNote.Color,
                        Image = requestNote.Image,
                        IsArchive = requestNote.IsArchive,
                        IsPin = requestNote.IsPin,
                        IsTrash = requestNote.IsTrash,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };

                    // add new note in tabel
                    authenticationContext.Note.Add(data);
                    await authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
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
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteNote(int noteID, string userID)
        {
            try
            {
               // get required note for user
                var user = authenticationContext.Note.Where(s => s.NoteID == noteID && s.UserID == userID).FirstOrDefault();

                // check whether user have required note or not
                if (user != null)
                {
                    // delete the note
                    authenticationContext.Note.Remove(user);
                    await authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
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
        /// <exception cref="Exception"></exception>
        public async Task<NoteModel> UpdateNote(NoteRequest noteRequest, int noteID, string userID)
        {
            try
            {
                // get required note for user
                var note = authenticationContext.Note.Where(s => s.NoteID == noteID && s.UserID==userID).FirstOrDefault();

                // check whether user have required note or not
                if (note != null)
                {
                    // set the current date and time 
                    note.ModifiedDate = DateTime.Now;

                    // check whether user entered value for note title or not
                    if (noteRequest.Title != null && noteRequest.Title != "")
                    {
                        note.Title = noteRequest.Title;
                    }

                    // check whether user entered value for note Description or not
                    if (noteRequest.Description != null && noteRequest.Description != "")
                    {
                        note.Description = noteRequest.Description;
                    }

                    // check whether user entered value for collaborator or not
                    if (noteRequest.Collaborator != null && noteRequest.Collaborator != "")
                    {
                        note.Collaborator = noteRequest.Collaborator;
                    }

                    // check whether user entered value for color or not
                    if (noteRequest.Color != null && noteRequest.Color != "")
                    {
                        note.Color = noteRequest.Color;
                    }

                    // check whether user entered value for image or not
                    if (noteRequest.Image != null && noteRequest.Image != "")
                    {
                        note.Image = noteRequest.Image;
                    }

                    // check whether user entered value for Archive or not
                    if (! noteRequest.IsArchive.Equals(note.IsArchive))
                    {
                        note.IsArchive = noteRequest.IsArchive;
                    }

                    // check whether user entered value for Pin or not
                    if (! noteRequest.IsPin.Equals(note.IsPin))
                    {
                        note.IsPin = noteRequest.IsPin;
                    }

                    // check whether user entered value for Trash or not
                    if (! noteRequest.IsTrash.Equals(note.IsTrash))
                    {
                        note.IsTrash = noteRequest.IsTrash;
                    }

                    // check whether user entered value for Reminder or not
                    if (noteRequest.Reminder >= DateTime.Now)
                    {
                        note.Reminder = noteRequest.Reminder;
                    }
                    
                    // update the changes
                    authenticationContext.Note.Update(note);
                    await authenticationContext.SaveChangesAsync();
                    return note;
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
        /// Displays the notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the list of note
        /// </returns>
        /// <exception cref="Exception"></exception>
        public IList<NoteResponse> DisplayNotes(string userID)
        {
            try
            {
                // get all notes of user
                var data = authenticationContext.Note.Where(s => s.UserID == userID);
                var List = new List<NoteResponse>();

                // check whether user have notes or not
                if (data != null)
                {
                    // iterates the loop for each note
                    foreach (var Note in data)
                    {
                        // get the required values of note
                        var Notes = new NoteResponse()
                        {
                          NoteID  =Note.NoteID,
                          Title=Note.Title,
                          Description=Note.Description,
                          Collaborator=Note.Collaborator,
                          Color=Note.Color,
                          Image=Note.Image,
                          IsArchive=Note.IsArchive,
                          IsPin=Note.IsPin,
                          IsTrash=Note.IsTrash,
                          Reminder=Note.Reminder
                        };

                        List.Add(Notes);
                    }

                    // returns the list
                    return List;
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
