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
    using FundooCommonLayer.Model.Request.Note;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.Context;
    using FundooRepositoryLayer.ImageUpload;
    using FundooRepositoryLayer.InterfaceRL;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

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

        private readonly ApplicationSetting applicationSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRL"/> class.
        /// </summary>
        /// <param name="authenticationContext">The authentication context.</param>
        public NoteRL(AuthenticationContext authenticationContext, IOptions<ApplicationSetting> applSetting)
        {
            this.applicationSetting = applSetting.Value;
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
                        IsTrash = false,
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
        public async Task<NoteResponse> IsArchive(int noteID, string userID)
        {
            try
            {
                // find required note from note table 
                var data = this.authenticationContext.Note.Where(s => s.NoteID == noteID && s.UserID == userID && s.IsTrash == false).FirstOrDefault();

                // check whether required note is found or not
                if (data != null)
                {
                    // if required note is found then check note is archived or not
                    if (!data.IsArchive)
                    {
                        // if note is not archived then make it archived 
                        data.IsArchive = true;
                        data.ModifiedDate = DateTime.Now;

                        // check whether the note is pinned or not
                        if (data.IsPin)
                        {
                            // make it unpinned
                            data.IsPin = false;
                        }

                        // update the changes in table
                        this.authenticationContext.Note.Update(data);

                        // save the changes
                        await this.authenticationContext.SaveChangesAsync();

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
                        // if note is archived then make it UnArchived 
                        data.IsArchive = false;
                        data.ModifiedDate = DateTime.Now;

                        // update the changes in table
                        this.authenticationContext.Note.Update(data);

                        // save the changes
                        await this.authenticationContext.SaveChangesAsync();

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
        public async Task<NoteResponse> IsPin(int noteID, string userID)
        {
            try
            {
                // find the required note from note table
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID && s.IsTrash == false).FirstOrDefault();

                // check whether user have required note or not
                if (data != null)
                {
                    // check whether user required note is pinned or not 
                    if (!data.IsPin)
                    {
                        // if user required note is not pinned then make it pinned
                        data.IsPin = true;
                        data.ModifiedDate = DateTime.Now;

                        // check whether user entered note is archived or not
                        if (data.IsArchive)
                        {
                            // if user entered note is archived then make it unArchived
                            data.IsArchive = false;
                        }

                        // update the changes into table
                        this.authenticationContext.Note.Update(data);

                        // save the changes
                        await this.authenticationContext.SaveChangesAsync();
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
                        // if user required note is pinned then make it UnPinned
                        data.IsPin = false;
                        data.ModifiedDate = DateTime.Now;

                        // update the changes into table
                        this.authenticationContext.Note.Update(data);

                        // save the changes
                        await this.authenticationContext.SaveChangesAsync();

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
                if (note != null && !note.IsTrash)
                {
                    note.IsTrash = true;
                    note.ModifiedDate = DateTime.Now;

                    if (note.IsPin)
                    {
                        note.IsPin = false;
                    }

                    // update the note info in note table
                    this.authenticationContext.Note.Update(note);

                    // saving the changes in table
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
        /// Gets the notes from trash.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the operation result
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public IList<NoteResponse> GetNotesFromTrash(string userID)
        {
            try
            {
                // find the required notes from note table
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID && s.IsTrash == true);
                var list = new List<NoteResponse>();

                // check whether required notes are exist or not
                if (data != null)
                {
                    // iterates the loop for every notes
                    foreach (var note in data)
                    {
                        // get info of each note
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

                        // adding note into list
                        list.Add(notes);
                    }

                    // returning the list of notes
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

        /// <summary>
        /// Restores the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the operation result</returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<NoteResponse> RestoreNote(int noteID, string userID)
        {
            try
            {
                // find the required note from note table
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();

                // check whether required note is exist or not
                if (note != null && note.IsTrash)
                {
                    // restore the note form trash
                    note.IsTrash = false;
                    note.ModifiedDate = DateTime.Now;

                    // update the changes in note table
                    this.authenticationContext.Update(note);

                    // saving the changes in note table
                    await this.authenticationContext.SaveChangesAsync();

                    // get the note info
                    var data = new NoteResponse()
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

                    // returning the note info
                    return data;
                }
                else
                {
                    // if not doesn't exist then return null
                    return null;
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
        /// <exception cref="Exception"> exception message</exception>
        public async Task<NoteResponse> ChangeColor(int noteID, string color, string userID)
        {
            try
            {
                // find the required note from note table
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();

                // check whether required note is exist or not
                if (note != null && note.IsTrash == false)
                {
                    // set the new color to note
                    note.Color = color;
                    note.ModifiedDate = DateTime.Now;

                    // update the changes in note table
                    this.authenticationContext.Update(note);

                    // save the changes in note table
                    await this.authenticationContext.SaveChangesAsync();

                    // get the note info
                    var data = new NoteResponse()
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

                    // returning the note info
                    return data;
                }
                else
                {
                    // if note doesn't exist then return null
                    return null;
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
        /// Date & time are required to set reminder
        /// or
        /// </exception>
        public async Task<NoteResponse> SetReminder(int noteID, DateTime dateTime, string userID)
        {
            try
            {
                // find the required note from note table
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();

                // check whether required note is exist or not
                if (note != null && note.IsTrash == false)
                {
                    // check whether user entered date and time are greater than current time
                    if (dateTime != null && dateTime > DateTime.Now)
                    {
                        // set the new reminder
                        note.Reminder = dateTime;
                        note.ModifiedDate = DateTime.Now;

                        // updaye the info in note table 
                        this.authenticationContext.Note.Update(note);

                        // save the changes in note table
                        await this.authenticationContext.SaveChangesAsync();

                        // get the note info
                        var data = new NoteResponse()
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

                        // returning the note info
                        return data;
                    }
                    else
                    {
                        // if user entered time is less than current time then throw exception
                        throw new Exception("Date & time are required to set reminder");
                    }
                }
                else
                {
                    // if note doesn't exist then return null
                    return null;
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
        /// <exception cref="Exception"> exception message</exception>
        public async Task<NoteResponse> RemoveReminder(int noteId, string userID)
        {
            try
            {
                // find the required note from note table
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteId).FirstOrDefault();

                // check whether required note is exist or not
                if (note != null && note.IsTrash == false)
                {
                    // remove the reminder
                    note.Reminder = null;
                    note.ModifiedDate = DateTime.Now;

                    // update the changes in note table
                    this.authenticationContext.Note.Update(note);

                    // save the changes in note table
                    await this.authenticationContext.SaveChangesAsync();

                    // get the note info
                    var data = new NoteResponse()
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

                    // returning the note info
                    return data;
                }
                else
                {
                    // if required note doesn't exist then return null
                    return null;
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
        /// <param name="file"></param>
        /// <returns>
        /// returns the operation result
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<NoteResponse> ImageUpload(int noteID, string userID, IFormFile file)
        {
            try
            {
                // find the required note from note table
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();

                // chcek whether required note is exist or not
                if (note != null && note.IsTrash == false)
                {
                    // send the API key,API secret key and cloud name to Upload Image class constructor
                    UploadImage imageUpload = new UploadImage(this.applicationSetting.APIkey, this.applicationSetting.APISecret, this.applicationSetting.CloudName);

                    // get the image url
                    var url = imageUpload.Upload(file);

                    // set the image to note
                    note.Image = url;
                    note.ModifiedDate = DateTime.Now;

                    // update the changes in note table
                    this.authenticationContext.Note.Update(note);

                    // save the changes in note table
                    await this.authenticationContext.SaveChangesAsync();

                    // get the note info
                    var data = new NoteResponse()
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

                    // returning the note info
                    return data;
                }
                else
                {
                    // if note doesn't exist then return null
                    return null;
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
        /// <returns> returns the list of notes or null value</returns>
        /// <exception cref="Exception"> exception message</exception>
        public IList<NoteResponse> Search(string key, string userId)
        {
            try
            {
                var element = key;

                // find the notes which contains user entered key element
                var data = this.authenticationContext.Note.Where(s => ((s.Title.Contains(element) || s.Description.Contains(element) || s.Collaborator.Contains(element)) && s.IsTrash == false) && s.UserID == userId);
                var list = new List<NoteResponse>();

                // check whether any note contain user entered key
                if (data != null)
                {
                    // iterates the loop for every notes
                    foreach (var note in data)
                    {
                        // get info of each note
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

                        // adding note into list
                        list.Add(notes);
                    }

                    // returning the list of notes
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

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> returns the true or false depending upon operation result</returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> BulkTrash(string userId)
        {
            try
            {
                // get the note data of user
                var data = this.authenticationContext.Note.Where(s => s.UserID == userId && s.IsTrash);

                // check wheather user have notes in trash or not
                if (data != null)
                {
                    // itearates the loop for all notes in trash
                    foreach (var note in data)
                    {
                        // delete the note from trash
                        this.authenticationContext.Note.Remove(note);
                    }

                    // save the changes in note table
                    await this.authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    // if user doesn't have any note in trash return false
                    return false;
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
        /// <param name="key">The key.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the list of contacts or null value</returns>
        /// <exception cref="Exception">
        /// Key required to search Person
        /// or
        /// </exception>
        public List<string> GetContacts(string key, string userID)
        {
            try
            {
                // check whether user entered any key or not
                if (key != null)
                {
                    // if user entered any key then find the contacts which contains user entered key value
                    var contacts = this.authenticationContext.UserDataTable.Where(s => s.Id != userID && s.Email.Contains(key));

                    // creating the dictionary class object to hold person name and email ID
                    List<string> ContactList = new List<string>();

                    // check whether any contact is found or not
                    if (contacts != null)
                    {
                        // iterarates the loop for each contact
                        foreach (var person in contacts)
                        {
                            // add the person name and email ID in dictionary class object
                            ContactList.Add(person.Email);
                        }

                        // return the list
                        return ContactList;
                    }
                    else
                    {
                        // if user entered key is not found for any record then return null
                        return null;
                    }
                }
                else
                {
                    // if uesr doesn't entered any key value then throw the exception
                    throw new Exception("Key required to search Person");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> ShareWith(CollaboratorRequest collaboratorRequest, string userID)
        {
            try
            {
                string userId = userID;

                // get the note info of user entered note id
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == collaboratorRequest.noteID && s.IsTrash == false).FirstOrDefault();

                if (note != null)
                {
                      // get the user info from user table through user entered email ID
                        var data = this.authenticationContext.UserDataTable.Where(s => s.Email == collaboratorRequest.emailID).FirstOrDefault();

                        // get the existed collaborator info form Collaborator table for user entered emailID
                        var existedCollaborator = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && (s.NoteID == collaboratorRequest.noteID && s.Collaborator == collaboratorRequest.emailID)).FirstOrDefault();

                        // check wheather emailID is present in user table or not
                        if (data != null)
                        {
                            // check wheather note is already shared with user entered email ID or not
                            if (existedCollaborator == null)
                            {
                                var collaborator = new CollaboratorModel()
                                {
                                    NoteID = collaboratorRequest.noteID,
                                    UserID = userId,
                                    Collaborator = collaboratorRequest.emailID,
                                    CreatedDate = DateTime.Now,
                                    ModifiedDate = DateTime.Now
                                };

                                this.authenticationContext.Collaborators.Add(collaborator);
                                await this.authenticationContext.SaveChangesAsync();
                                return true;
                            }
                            else
                            {
                                throw new Exception("This email already exists");
                            }
                        }
                        else
                        {
                            throw new Exception("This email ID not found ");
                        }
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