﻿// ******************************************************************************
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
    using FundooCommonLayer.Model.Response.Note;
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
        /// The application setting
        /// </summary>
        private readonly ApplicationSetting applicationSetting;

        /// <summary>
        /// creating reference of authentication context class
        /// </summary>
        private AuthenticationContext authenticationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRL"/> class.
        /// </summary>
        /// <param name="authenticationContext">The authentication context.</param>
        /// <param name="applSetting">The application setting.</param>
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
                    var data = new NoteRequest()
                    {
                        Title = requestNote.Title,
                        Description = requestNote.Description,
                        Reminder = requestNote.Reminder,
                        Collaborator = requestNote.Collaborator,
                        Color = requestNote.Color,
                        Image = requestNote.Image,
                        IsArchive = requestNote.IsArchive,
                        IsPin = requestNote.IsPin,
                        IsTrash = false,
                    };

                    // check wheather user added any collaborator or not
                    if (data.Collaborator != null)
                    {
                        // if user added collaborator then first save the note info into Note table
                        var noteInfo = new NoteModel()
                        {
                            UserID = userID,
                            Title = data.Title,
                            Description = data.Description,
                            Collaborators = 1,
                            Color = data.Color,
                            Reminder = data.Reminder,
                            Image = data.Image,
                            IsArchive = data.IsArchive,
                            IsPin = data.IsPin,
                            IsTrash = data.IsTrash,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        };

                        // add new note in tabel
                        this.authenticationContext.Note.Add(noteInfo);
                        await this.authenticationContext.SaveChangesAsync();

                        // get the collaborator info from User table
                        var user = this.authenticationContext.UserDataTable.Where(s => s.Email == data.Collaborator).FirstOrDefault();

                        // check wheather user entered collaborator is present in User table or not
                        if (user != null)
                        {
                            // if collaborator is present in User table then add the info into collaborator table
                            var collaborator = new CollaboratorModel()
                            {
                                NoteID = noteInfo.NoteID,
                                UserID = noteInfo.UserID,
                                CollaboratorID = user.Id,
                                EmailID = data.Collaborator,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            };

                            // adding the collaborator and in collaborator table
                            this.authenticationContext.Collaborators.Add(collaborator);

                            // save the changes into database
                            await this.authenticationContext.SaveChangesAsync();
                        }
                        else
                        {
                            // if user entered emailId for collaborator not found then throw exception
                            throw new Exception(" Email ID not found for Collaborator");
                        }
                    }
                    else
                    {
                        // if user doesn't add any collaborator then save the note with collaborator as 0 value in note table
                        var noteInfo = new NoteModel()
                        {
                            UserID = userID,
                            Title = data.Title,
                            Description = data.Description,
                            Collaborators = 0,
                            Color = data.Color,
                            Reminder = data.Reminder,
                            Image = data.Image,
                            IsArchive = data.IsArchive,
                            IsPin = data.IsPin,
                            IsTrash = data.IsTrash,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        };

                        // add new note in tabel
                        this.authenticationContext.Note.Add(noteInfo);

                        // save the changes in database
                        await this.authenticationContext.SaveChangesAsync();
                    }

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
                        var result = await this.ShareWith(note.NoteID, noteRequest.Collaborator, userID);
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
                        NoteResponse notes = this.GetNoteResponse(userID, note);

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

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the operation result
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<NoteResponse> GetNote(int noteID, string userID)
        {
            try
            {
                // get all notes of user
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID && s.IsArchive == false && s.IsTrash == false).FirstOrDefault();

                // check whether user have notes or not
                if (data != null)
                {
                    NoteResponse note = this.GetNoteResponse(userID, data);
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
                // get required note info from note table 
                var data = this.authenticationContext.Note.Where(s => s.NoteID == noteID && s.UserID == userID && s.IsTrash == false).FirstOrDefault();

                // check whether required note is found or not
                if (data != null)
                {
                    var list = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && s.NoteID == noteID);

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

                        NoteResponse note = this.GetNoteResponse(userID, data);

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

                        NoteResponse note = this.GetNoteResponse(userID, data);

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
                        // get note info
                        NoteResponse notes = this.GetNoteResponse(userID, note);

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
                    var collaboratorList = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && s.NoteID == noteID);

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

                        // get note info
                        NoteResponse note = this.GetNoteResponse(userID, data);

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

                        // get note info
                        NoteResponse note = this.GetNoteResponse(userID, data);

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
                        var collaboratorList = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && s.NoteID == note.NoteID);

                        // get note info
                        NoteResponse notes = this.GetNoteResponse(userID, note);

                        // add note into list
                        list.Add(notes);
                    }

                    // return the list of pinned notes
                    return list;
                }
                else
                {
                    // if user does not have any pinned note then return null
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
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns true or false indicating operation result</returns>
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
                        var collaboratorList = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && s.NoteID == note.NoteID);

                        // get info of each note
                        // get note info
                        NoteResponse notes = this.GetNoteResponse(userID, note);

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
                    var collaboratorList = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && s.NoteID == noteID);

                    // restore the note form trash
                    note.IsTrash = false;
                    note.ModifiedDate = DateTime.Now;

                    // update the changes in note table
                    this.authenticationContext.Update(note);

                    // saving the changes in note table
                    await this.authenticationContext.SaveChangesAsync();

                    // get note info
                    NoteResponse notes = this.GetNoteResponse(userID, note);

                    // returning the note info
                    return notes;
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
                    var collaboratorList = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && s.NoteID == noteID);

                    // set the new color to note
                    note.Color = color;
                    note.ModifiedDate = DateTime.Now;

                    // update the changes in note table
                    this.authenticationContext.Update(note);

                    // save the changes in note table
                    await this.authenticationContext.SaveChangesAsync();

                    // get note info
                    NoteResponse data = this.GetNoteResponse(userID, note);

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
                    var collaboratorList = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && s.NoteID == noteID);

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

                        // get note info
                        NoteResponse data = this.GetNoteResponse(userID, note);

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
                    var collaboratorList = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && s.NoteID == noteId);

                    // remove the reminder
                    note.Reminder = null;
                    note.ModifiedDate = DateTime.Now;

                    // update the changes in note table
                    this.authenticationContext.Note.Update(note);

                    // save the changes in note table
                    await this.authenticationContext.SaveChangesAsync();

                    // get note info
                    NoteResponse data = this.GetNoteResponse(userID, note);

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
        /// <param name="file"> The file </param>
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
                    var collaboratorList = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && s.NoteID == noteID);

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
                   
                    // get note info
                    NoteResponse data = this.GetNoteResponse(userID, note);

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
        /// Removes the image.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the note info or null value</returns>
        /// <exception cref="Exception">
        /// Note not Found
        /// or
        /// </exception>
        public async Task<NoteResponse> RemoveImage(int noteID, string userID)
        {
            try
            {
                // get the note data from note table
                var note = this.authenticationContext.Note.Where(s => s.NoteID == noteID && s.UserID == userID && s.IsTrash == false).FirstOrDefault();

                // check wheather note is found or not
                if (note != null)
                {
                    // store null value for image
                    note.Image = null;

                    // update the changes in note table
                    this.authenticationContext.Note.Update(note);

                    // save the changes in databse
                    await this.authenticationContext.SaveChangesAsync();

                    // get note info
                    NoteResponse data = this.GetNoteResponse(userID, note);

                    return data;
                }
                else
                {
                    throw new Exception("Note not Found");
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
        /// <returns> returns the list of notes or null value</returns>
        /// <exception cref="Exception"> exception message</exception>
        public IList<NoteResponse> Search(string key, string userId)
        {
            try
            {
                var element = key;

                // find the notes which contains user entered key element
                var data = this.authenticationContext.Note.Where(s => ((s.Title.Contains(element) || s.Description.Contains(element)) && s.IsTrash == false) && s.UserID == userId);
                var list = new List<NoteResponse>();

                // check whether any note contain user entered key
                if (data != null)
                {
                    // iterates the loop for every notes
                    foreach (var note in data)
                    {
                        // get note info
                        NoteResponse notes = this.GetNoteResponse(userId, note);

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
        /// <exception cref="Exception"> exception message</exception>
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
        /// <param name="key">The key tobe searched.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the list of contacts or null value
        /// </returns>
        /// <exception cref="Exception">
        /// Key required to search Person
        /// or
        /// </exception>
        public Dictionary<string, string> GetContacts(string key, string userID)
        {
            try
            {
                // check whether user entered any key or not
                if (key != null)
                {
                    // if user entered any key then find the contacts which contains user entered key value
                    var contacts = this.authenticationContext.UserDataTable.Where(s => s.Id != userID && s.Email.Contains(key));

                    // creating the dictionary class object to hold person name and email ID
                    Dictionary<string, string> contactList = new Dictionary<string, string>();

                    // check whether any contact is found or not
                    if (contacts != null)
                    {
                        // iterarates the loop for each contact
                        foreach (var person in contacts)
                        {
                            // add the person name and email ID in dictionary class object
                            contactList.Add(person.Id, person.Email);
                        }

                        // return the list
                        return contactList;
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

        /// <summary>
        /// Shares the with.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns true or false indicating operation result</returns>
        /// <exception cref="Exception">
        /// This email already exists
        /// or
        /// This email ID not found
        /// or
        /// </exception>
        public async Task<bool> ShareWith(int noteID, string id, string userID)
        {
            try
            {
                string userId = userID;

                // get the note info of user entered note id
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID && s.IsTrash == false).FirstOrDefault();

                if (note != null)
                {
                    // get the user info from user table through user entered email ID
                    var data = this.authenticationContext.UserDataTable.Where(s => s.Email == id || s.Id == id).FirstOrDefault();

                    // get the existed collaborator info form Collaborator table for user entered emailID
                    var existedCollaborator = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && s.NoteID == noteID && (s.CollaboratorID == id || s.EmailID == id)).FirstOrDefault();

                    // check wheather user is present in user table or not
                    if (data != null)
                    {
                        // check wheather note is already shared with user entered email ID or not
                        if (existedCollaborator == null)
                        {
                            var collaborator = new CollaboratorModel()
                            {
                                NoteID = noteID,
                                UserID = userId,
                                CollaboratorID = data.Id,
                                EmailID = data.Email,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            };

                            this.authenticationContext.Collaborators.Add(collaborator);

                            note.Collaborators = note.Collaborators + 1;
                            this.authenticationContext.Note.Update(note);

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
        /// Note not found
        /// or
        /// </exception>
        public async Task<bool> DeleteCollaborator(int noteID, string id, string userID)
        {
            try
            {
                var noteData = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID && s.IsTrash == false).FirstOrDefault();

                if (noteData != null)
                {
                    // get the note info which have user specified collaborator form collaborators table
                    var note = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && (s.CollaboratorID == id || s.EmailID == id) && s.NoteID == noteID).FirstOrDefault();

                    // check wheather note is found or not
                    if (note != null)
                    {
                        // if note is found in collaborators table then delete the collaborator for specified note
                        this.authenticationContext.Collaborators.Remove(note);

                        noteData.Collaborators = noteData.Collaborators - 1;
                        this.authenticationContext.Note.Update(noteData);

                        //// save the changes in database
                        await this.authenticationContext.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        // if note not found then return false
                        return false;
                    }
                }
                else
                {
                    throw new Exception("Note not found");
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
        /// <returns> returns note info</returns>
        /// <exception cref="Exception">
        /// Label already exist for note
        /// or
        /// Note not found
        /// or
        /// Label not found
        /// or
        /// </exception>
        public async Task<NoteResponse> AddLabel(int labelID, int noteID, string userID)
        {
            try
            {
                // get the labels of user from label table
                var label = this.authenticationContext.Label.Where(s => s.LabelID == labelID && s.UserID == userID).FirstOrDefault();

                // get the note data from note table
                var noteData = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID && s.IsTrash == false).FirstOrDefault();

                // check wheather user have label or not
                if (noteData != null)
                {            
                    // get data from Note label table for selected label and note
                    var noteLabelData = this.authenticationContext.NoteLabel.Where(s => s.UserID == userID && s.LabelID == labelID && s.NoteID == noteID).FirstOrDefault();

                    // check wheather user have note or not
                    if (label != null)
                    {
                        // check wheather user already have label for selected note or not
                        if (noteLabelData == null)
                        {
                            // add the label on note in note label table
                            var labelOnNote = new NoteLabelModel()
                            {
                                LabelID = label.LabelID,
                                Label = label.Label,
                                NoteID = noteID,
                                UserID = userID,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            };

                            // add new entry in Note Lable table
                            this.authenticationContext.NoteLabel.Add(labelOnNote);

                            // save the changes in database
                            await this.authenticationContext.SaveChangesAsync();
                        }
                        else
                        {
                            // if selected note already have user entered label then throw exception
                            throw new Exception("Label already exist for note");
                        }

                        // get the note info
                        NoteResponse note = this.GetNoteResponse(userID, noteData);

                        // return note info
                        return note;
                    }
                    else
                    {
                        // if user doen't have note then throw exception
                        throw new Exception("Label not found");
                    }
                }
                else
                {
                    // if user entered note ID  not found in Note table return null
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
        /// <returns>
        /// returns note info or null value
        /// </returns>
        /// <exception cref="Exception">
        /// Note doesn't have label
        /// or
        /// Label not found
        /// or
        /// </exception>
        public async Task<NoteResponse> RemoveLabel(int noteID, int labelID, string userID)
        {
            try
            {
                // get the labels of user from label table
                var label = this.authenticationContext.Label.Where(s => s.LabelID == labelID && s.UserID == userID).FirstOrDefault();

                // get the note data from note table
                var noteData = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID && s.IsTrash == false).FirstOrDefault();

                // check wheather user have label or not
                if (noteData != null)
                {                   
                    // get data from Note-label table for selected label and note
                    var noteLabelData = this.authenticationContext.NoteLabel.Where(s => s.UserID == userID && s.LabelID == labelID && s.NoteID == noteID).FirstOrDefault();

                    // check wheather user have note or not
                    if (label != null)
                    {
                        // check wheather user have label for selected note or not
                        if (noteLabelData != null)
                        {                         
                            // Remove entry in Note-Lable table
                            this.authenticationContext.NoteLabel.Remove(noteLabelData);

                            // save the changes in database
                            await this.authenticationContext.SaveChangesAsync();
                        }
                        else
                        {
                            // if selected note already have user entered label then throw exception
                            throw new Exception("Note doesn't have label");
                        }

                        // get the note info
                        NoteResponse note = this.GetNoteResponse(userID, noteData);

                        // return note info
                        return note;
                    }
                    else
                    {
                        // if user doen't have note then throw exception
                        throw new Exception("Label not found");
                    }
                }
                else
                {
                    // if user entered noteID not found in Note table then return null
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Gets the note response.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="note">The note.</param>
        /// <returns> returns the note info </returns>
        private NoteResponse GetNoteResponse(string userID, NoteModel note)
        {
            // creating a list for holding collaborators info for esch note
            var collaborators = new List<CollaboratorRsponse>();

            // get the collaborators for each note
            var collaboratorsList = this.authenticationContext.Collaborators.Where(s => s.UserID == userID && s.NoteID == note.NoteID);

            // get labels for Note
            var labelList = this.authenticationContext.NoteLabel.Where(s => s.UserID == userID && s.NoteID == note.NoteID);

            // creating list for label
            var labels = new List<LabelResponse>();

            // iterate the loop for each collaborator for note
            foreach (var coll in collaboratorsList)
            {
                // get the collaborator info
                var collaboratorData = new CollaboratorRsponse()
                {
                    CollaboratorID = coll.CollaboratorID,
                    EmailID = coll.EmailID,
                };

                // add the collaborator info into collaborator list
                collaborators.Add(collaboratorData);
            }

            // iteartes the loop for each label for note
            foreach (var data in labelList)
            {
                // get the label info
                var label = new LabelResponse()
                {
                    ID = data.LabelID,
                    Label = data.Label,
                };

                // add the label info into label list
                labels.Add(label);
            }

            // get the required values of note
            var notes = new NoteResponse()
            {
                NoteID = note.NoteID,
                Title = note.Title,
                Description = note.Description,
                Collaborator = collaboratorsList.Count(),
                Color = note.Color,
                Image = note.Image,
                IsArchive = note.IsArchive,
                IsPin = note.IsPin,
                IsTrash = note.IsTrash,
                Reminder = note.Reminder,
                Collaborators = collaborators,
                Labels = labels
            };

            // return the note info
            return notes;
        }
    }
}