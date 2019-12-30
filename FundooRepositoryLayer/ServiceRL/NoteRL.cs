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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.Context;
    using FundooRepositoryLayer.InterfaceRL;

    /// <summary>
    ///  this class contains different methods to interact with note table
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
        /// <returns> returns true or false indicating operation is successful or not</returns>
        /// <exception cref="Exception"> exception message</exception>
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
                    this.authenticationContext.Note.Add(data);
                    await this.authenticationContext.SaveChangesAsync();
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
        /// <exception cref="Exception"> exception message</exception>
        public async Task<bool> DeleteNote(int noteID, string userID)
        {
            try
            {
                // get required note for user
                var note = this.authenticationContext.Note.Where(s => s.NoteID == noteID && s.UserID == userID).FirstOrDefault();

                // check whether user have required note or not
                if (note != null && note.IsTrash)
                {
                    // delete the note from note tabel
                    this.authenticationContext.Note.Remove(note);
                    await this.authenticationContext.SaveChangesAsync();
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
        /// <exception cref="Exception"> exception message</exception>
        public async Task<NoteModel> UpdateNote(NoteRequest noteRequest, int noteID, string userID)
        {
            try
            {
                // get required note for user
                var note = this.authenticationContext.Note.Where(s => s.NoteID == noteID && s.UserID == userID).FirstOrDefault();

                // check whether user have required note or not
                if (note != null)
                {
                    // set the current date and time 
                    note.ModifiedDate = DateTime.Now;

                    // check whether user entered value for note title or not
                    if (noteRequest.Title != null && noteRequest.Title != string.Empty)
                    {
                        note.Title = noteRequest.Title;
                    }

                    // check whether user entered value for note Description or not
                    if (noteRequest.Description != null && noteRequest.Description != string.Empty)
                    {
                        note.Description = noteRequest.Description;
                    }

                    // check whether user entered value for collaborator or not
                    if (noteRequest.Collaborator != null && noteRequest.Collaborator != string.Empty)
                    {
                        note.Collaborator = noteRequest.Collaborator;
                    }

                    // check whether user entered value for color or not
                    if (noteRequest.Color != null && noteRequest.Color != string.Empty)
                    {
                        note.Color = noteRequest.Color;
                    }

                    // check whether user entered value for image or not
                    if (noteRequest.Image != null && noteRequest.Image != string.Empty)
                    {
                        note.Image = noteRequest.Image;
                    }

                    // check whether user entered value for Archive or not
                    if (!noteRequest.IsArchive.Equals(note.IsArchive))
                    {
                        note.IsArchive = noteRequest.IsArchive;
                    }

                    // check whether user entered value for Pin or not
                    if (!noteRequest.IsPin.Equals(note.IsPin))
                    {
                        note.IsPin = noteRequest.IsPin;
                    }

                    // check whether user entered value for Trash or not
                    if (!noteRequest.IsTrash.Equals(note.IsTrash))
                    {
                        note.IsTrash = noteRequest.IsTrash;
                    }

                    // check whether user entered value for Reminder or not
                    if (noteRequest.Reminder >= DateTime.Now)
                    {
                        note.Reminder = noteRequest.Reminder;
                    }

                    // update the changes
                    this.authenticationContext.Note.Update(note);
                    await this.authenticationContext.SaveChangesAsync();
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
        /// <exception cref="Exception"> exception message</exception>
        public IList<NoteResponse> DisplayNotes(string userID)
        {
            try
            {
                // get all notes of user
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID && s.IsArchive == false && s.IsTrash == false);
                var list = new List<NoteResponse>();

                // check whether user have notes or not
                if (data != null)
                {
                    // iterates the loop for each note
                    foreach (var note in data)
                    {
                        // get the required values of note
                        var notes = new NoteResponse()
                        {
                            NoteID = note.NoteID,
                            Title = note.Title,
                            Description = note.Description,
                            Collaborator = note.Collaborator,
                            Color = note.Color,
                            Image = note.Image,
                            IsArchive = note.IsArchive,
                            IsPin = note.IsPin,
                            IsTrash = note.IsTrash,
                            Reminder = note.Reminder
                        };

                        list.Add(notes);
                    }

                    // returns the list
                    return list;
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

        public async Task<NoteResponse> GetNote(int noteID, string userID)
        {
            try
            {
                // get all notes of user
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID && s.IsArchive == false && s.IsTrash == false).FirstOrDefault();

                // check whether user have notes or not
                if (data != null)
                {
                    var note = new NoteResponse()
                    {
                       NoteID = data.NoteID,
                       Title = data.Title,
                       Description = data.Description,
                       Collaborator = data.Collaborator,
                       Color = data.Color,
                       Image = data.Image,
                       IsArchive = data.IsArchive,
                       IsPin = data.IsPin,
                       IsTrash = data.IsTrash,
                       Reminder = data.Reminder
                    };

                    // returns the note info
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
        /// Determines whether the specified note identifier is archive.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="archive">if set to <c>true</c> [archive].</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns true indicating note is Archived or false to indicate note is UnArchived
        /// </returns>
        /// <exception cref="Exception">
        /// Note doesn't exist
        /// or
        /// </exception>
        public async Task<bool> IsArchive(int noteID, bool archive, string userID)
        {
            try
            {
                // find required note from note table 
                var note = this.authenticationContext.Note.Where(s => s.NoteID == noteID && s.UserID == userID && s.IsTrash == false).FirstOrDefault();

                // check whether required note is found or not
                if (note != null)
                {
                    // if required note is found then check note is archived or not
                    if (!note.IsArchive && archive)
                    {
                        // if note is not archived then make it archived 
                        note.IsArchive = true;

                        // check whether the note is pinned or not
                        if (note.IsPin)
                        {
                            // make it unpinned
                            note.IsPin = false;
                        }

                        // update the changes in table
                        this.authenticationContext.Note.Update(note);

                        // save the changes
                        await this.authenticationContext.SaveChangesAsync();
                        return true;
                    }
                    else if (note.IsArchive && !archive)
                    {
                        // if note is archived then make it UnArchived 
                        note.IsArchive = false;

                        // update the changes in table
                        this.authenticationContext.Note.Update(note);

                        // save the changes
                        await this.authenticationContext.SaveChangesAsync();
                        return false;
                    }
                    else
                    {
                        // check whether note is already archived or not
                        if (note.IsArchive && archive)
                        {
                            throw new Exception("Note is already archived");
                        }
                        else
                        {
                            // if note is doesn't archived and user wants to unarchive it
                            throw new Exception("Note doesn't archived");
                        }
                    }
                }
                else
                {
                    // if required note is not found then throw exception
                    throw new Exception("Note doesn't exist");
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
        /// <exception cref="Exception"> exception message</exception>
        public IList<NoteResponse> GetArchivedNotes(string userID)
        {
            try
            {
                // get the notes which are archived
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID && s.IsArchive == true);
                var list = new List<NoteResponse>();

                // check whether user have any archived note or not
                if (data != null)
                {
                    // iterates the loop for each note
                    foreach (var note in data)
                    {
                        // get the required values of note
                        var notes = new NoteResponse()
                        {
                            NoteID = note.NoteID,
                            Title = note.Title,
                            Description = note.Description,
                            Collaborator = note.Collaborator,
                            Color = note.Color,
                            Image = note.Image,
                            IsArchive = note.IsArchive,
                            IsPin = note.IsPin,
                            IsTrash = note.IsTrash,
                            Reminder = note.Reminder
                        };

                        // add note into list
                        list.Add(notes);
                    }

                    // return the list of archived notes
                    return list;
                }
                else
                {
                    // if user does not have any archived note then return null
                    return null;
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
        /// <param name="isPin">if set to <c>true</c> [is pin].</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns true indicating note is Pinned or false to indicate note is UnPinned
        /// </returns>
        /// <exception cref="Exception">
        /// Note doesn't exist
        /// or
        /// </exception>
        public async Task<bool> IsPin(int noteID, bool isPin, string userID)
        {
            try
            {
                // find the required note from note table
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID && s.IsTrash == false).FirstOrDefault();

                // check whether user have required note or not
                if (note != null)
                {
                    // check whether user required note is pinned or not 
                    if (!note.IsPin && isPin)
                    {
                        // if user required note is not pinned then make it pinned
                        note.IsPin = true;

                        // check whether user entered note is archived or not
                        if (note.IsArchive)
                        {
                            // if user entered note is archived then make it unArchived
                            note.IsArchive = false;
                        }

                        // update the changes into table
                        this.authenticationContext.Note.Update(note);

                        // save the changes
                        await this.authenticationContext.SaveChangesAsync();
                        return true;
                    }
                    else if (note.IsPin && !isPin)
                    {
                        // if user required note is pinned then make it UnPinned
                        note.IsPin = false;

                        // update the changes into table
                        this.authenticationContext.Note.Update(note);

                        // save the changes
                        await this.authenticationContext.SaveChangesAsync();
                        return false;
                    }
                    else
                    {
                        if (note.IsPin && isPin)
                        {
                            throw new Exception("Note is already pinned");
                        }
                        else
                        {
                            throw new Exception("Note doesn't pinned");
                        }
                    }
                }
                else
                {
                    // if user required note doesn't found then throw exception
                    throw new Exception("Note doesn't exist");
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
        /// returns list of Pinned notes
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public IList<NoteResponse> GetPinnedNotes(string userID)
        {
            try
            {
                // finds the user notes which are pinned
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID && s.IsPin == true && s.IsTrash == false);
                var list = new List<NoteResponse>();

                // check whether use have any pinned note or not
                if (data != null)
                {
                    // iterates the loop for all notes
                    foreach (var note in data)
                    {
                        // get the required note info
                        var notes = new NoteResponse()
                        {
                            NoteID = note.NoteID,
                            Title = note.Title,
                            Description = note.Description,
                            Collaborator = note.Collaborator,
                            Color = note.Color,
                            Image = note.Image,
                            IsArchive = note.IsArchive,
                            IsPin = note.IsPin,
                            IsTrash = note.IsTrash,
                            Reminder = note.Reminder
                        };

                        // add note into list
                        list.Add(notes);
                    }

                    // return the list of pinned notes
                    return list;
                }
                else
                {
                    /// if user does not have any pinned note then return null
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is trash.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="isTrash">if set to <c>true</c> [is trash].</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Note is already in trash
        /// or
        /// Note doesn't exist in trash
        /// or
        /// Note not found
        /// or
        /// </exception>
        public async Task<bool> MoveToTrash(int noteID, string userID)
        {
            try
            {
                // find the required note from note table
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();

                // check whether user have required note or not
                if (note != null && ! note.IsTrash)
                {
                    note.IsTrash = true;

                    if (note.IsPin)
                    {
                        note.IsPin = false;
                    }

                    this.authenticationContext.Note.Update(note);
                    await this.authenticationContext.SaveChangesAsync();
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

        public IList<NoteResponse> GetNotesFromTrash(string userID)
        {
            try
            {
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID && s.IsTrash == true);
                var list = new List<NoteResponse>();

                if (data != null)
                {
                    foreach (var note in data)
                    {
                        var notes = new NoteResponse()
                        {
                            NoteID = note.NoteID,
                            Title = note.Title,
                            Description = note.Description,
                            Collaborator = note.Collaborator,
                            Color = note.Color,
                            Image = note.Image,
                            IsArchive = note.IsArchive,
                            IsPin = note.IsPin,
                            IsTrash = note.IsTrash,
                            Reminder = note.Reminder
                        };

                        list.Add(notes);
                    }

                    return list;
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

        public async Task<bool> RestoreFromTrash(int noteID, string userID)
        {
            try
            {
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();

                if (note != null && note.IsTrash)
                {

                    note.IsTrash = false;
                    this.authenticationContext.Update(note);
                    await this.authenticationContext.SaveChangesAsync();

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
    }
}
