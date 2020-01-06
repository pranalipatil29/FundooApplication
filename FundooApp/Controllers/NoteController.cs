// ******************************************************************************
//  <copyright file="NoteController.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  NoteController.cs
//  
//     Purpose:  Creating a controller to manage api calls
//     @author  Pranali Patil
//     @version 1.0
//     @since   23-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooApp.Controllers
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooBusinessLayer.InterfaceBL;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Request.Note;
    using FundooCommonLayer.Model.Response;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// this class contains different methods to handle API calls for note
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        /// <summary>
        /// creating the reference of business layer note class
        /// </summary>
        private readonly INoteBL noteBL;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteController"/> class.
        /// </summary>
        /// <param name="noteBL"> reference of interface of business layer note class.</param>
        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <returns> returns the message indicating operation is done or not</returns>
        [HttpPost]
       // [Route("CreateNote")]
        ////Post: /api/Note/CreateNote
        public async Task<IActionResult> CreateNote(NoteRequest noteRequest)
        {
            try
            {
               // string userId = "e6ac5ba3-a6d4-400a-bf42-10d7e410ab7a";
               var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                var result = await this.noteBL.CreateNote(noteRequest, userId);
                bool success = false;
                var message = string.Empty;

                // check whether result is true or false
                if (result)
                {
                    // if true then return the result 
                    success = true;
                    message = "Succeffully Created Note";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "Failed";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns> returns the message indicating operation is done or not</returns>
        [HttpDelete]
        [Route("{noteID}")]
        ////Post: /api/Note/DeleteNote
        public async Task<IActionResult> DeleteNote(int noteID)
        {
            try
            {
               // string userId = "e6ac5ba3-a6d4-400a-bf42-10d7e410ab7a";
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var result = await this.noteBL.DeleteNote(noteID, userId);

                bool success = false;
                var message = string.Empty;

                // check whether result is true or false
                if (result)
                {
                    success = true;
                    message = "Succeffully Deleted Note";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "Failed";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <returns> returns the updated info of note</returns>
        [HttpPut]
        [Route("{noteID}")]
        ////Put: /api/Note/UpdateNote
        public async Task<IActionResult> UpdateNote(NoteRequest noteRequest, int noteID)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                var result = await this.noteBL.UpdateNote(noteRequest, noteID, userID);

                bool success = false;
                var message = string.Empty;
                if (result != null)
                {
                    success = true;
                    message = "Updated Successfully";
                    return this.Ok(new { success, message, result });
                }
                else
                {
                    success = false;
                    message = "Failed";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <returns> returns the list of notes</returns>
        [HttpGet]
       // [Route("DisplayNotes")]
        ////Post: /api/Note/DisplayNotes
        public async Task<IActionResult> DisplayNotes()
        {
            try
            {
               // string userId = "e6ac5ba3-a6d4-400a-bf42-10d7e410ab7a";
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                IList<NoteResponse> data = this.noteBL.DisplayNotes(userId);

                bool success = false;
                var message = string.Empty;

                if (data.Count != 0)
                {
                    success = true;
                    return this.Ok(new { success, data });
                }
                else
                {
                    success = false;
                    message = "Notes doesn't exist";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns> returns the info of specific note</returns>
        [HttpGet]
         [Route("{noteID}")]
        ////Post: /api/Note/DisplayNotes
        public async Task<IActionResult> GetNote(int noteID)
        {
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.GetNote(noteID,userId);
                bool success = false;
                var message = string.Empty;

                if (data != null)
                {
                    success = true;
                    message = "Note Found ";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Failed";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        /// <summary>
        /// Archives the specified note identifier.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="archive">if set to <c>true</c> [archive].</param>
        /// <returns> returns the result of operation</returns>
        [HttpPut]
        [Route("{noteID}/Archive")]
        public async Task<IActionResult> Archive(int noteID)
        {
            bool success = false;
            var message = string.Empty;

            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.IsArchive(noteID, userID);

                if (data != null)
                {
                    success = true;
                    message = "Note Archived";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = true;
                    message = "Note UnArchived";
                    return this.Ok(new { success, message });
                }
            }
            catch (Exception exception)
            {
                success = false;
                return this.BadRequest(new { success, exception.Message });
            }
        }

        /// <summary>
        /// Gets the archived notes.
        /// </summary>
        /// <returns> returns the result of operation</returns>
        [HttpGet]
        [Route("Archive")]
        public async Task<IActionResult> GetArchivedNotes()
        {
            bool success = false;
            var message = string.Empty;

            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var notes = this.noteBL.GetArchivedNotes(userID);

                if (notes.Count > 0)
                {
                    success = true;
                    message = "Archived Notes ";
                    return this.Ok(new { success, message, notes });
                }
                else
                {
                    success = false;
                    message = "Archived notes not found";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                success = false;
                return this.BadRequest(new { success, exception.Message });
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is pin.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="isPin">if set to <c>true</c> [is pin].</param>
        /// <returns> returns the result of operation</returns>
        [HttpPut]
        [Route("{noteID}/Pin")]
        public async Task<IActionResult> IsPin(int noteID)
        {
            bool success = false;
            var message = string.Empty;

            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.IsPin(noteID, userID);

                if (data != null)
                {
                    success = true;
                    message = "Note Pinned";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = true;
                    message = "Note UnPinned";
                    return this.Ok(new { success, message });
                }
            }
            catch (Exception exception)
            {
                success = false;
                return this.BadRequest(new { success, exception.Message });
            }
        }

        /// <summary>
        /// Gets the pinned notes.
        /// </summary>
        /// <returns> returns the result of operation</returns>
        [HttpGet]
        [Route("Pinned")]
        public async Task<IActionResult> GetPinnedNotes()
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var result = this.noteBL.GetPinnedNotes(userID);
                bool success = false;
                var message = string.Empty;

                if (result.Count > 0)
                {
                    success = true;
                    message = "Pinned Notes ";
                    return this.Ok(new { success, message, result });
                }
                else
                {
                    success = false;
                    message = "No pinned notes found";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Moves to trash.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns> returns the operation result</returns>
        [HttpPut]
        [Route("{noteID}/Trash")]
        public async Task<IActionResult> MoveToTrash(int noteID)
        {
            bool success = false;
            var message = string.Empty;
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var result = await this.noteBL.MoveToTrash(noteID, userId);

                if (result)
                {
                    success = true;
                    message = "Note Deleted";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "Note not found";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                success = false;
                return this.BadRequest(new { success, exception.Message });
            }
        }

        /// <summary>
        /// Gets the notes from trash.
        /// </summary>
        /// <returns> returns the notes info from trash</returns>
        [HttpGet]
        [Route("Trash")]
        public async Task<IActionResult> GetNotesFromTrash()
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var notes = this.noteBL.GetNotesFromTrash(userID);
                var message = string.Empty;
                bool success = false;

                if (notes.Count > 0)
                {
                    success = true;
                    message = "Notes In Trash";
                    return this.Ok(new { success, message, notes });
                }
                else
                {
                    success = false;
                    message = "Trash is Empty";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        /// <summary>
        /// Restores from trash.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns> returns the operation result</returns>
        [HttpPut]
        [Route("{noteID}/Restore")]
        public async Task<IActionResult> RestoreNote(int noteID)
        {
            bool success = false;
            var message = string.Empty;
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.RestoreNote(noteID, userId);

                if (data != null)
                {
                    success = true;
                    message = "Successfully Restored Note";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Note not found in trash";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                success = false;
                message = "User Authentication failed";
                return this.BadRequest(new { success, message, exception.Message });
            }
        }

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="changeColorRequest">The change color request.</param>
        /// <returns> returns the operation result</returns>
        [HttpPut]
        [Route("{noteID}/Color")]
        public async Task<IActionResult> ChangeColor(int noteID, ChangeColorRequest changeColorRequest)
        {
            bool success = false;
            var message = string.Empty;

            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.ChangeColor(noteID, changeColorRequest.Color, userId);

                if(data != null)
                {
                    success = true;
                    message = "Successfully changed note color";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Note doesn't exist";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception exception)
            {
                success = false;
                message = exception.Message;
                return this.BadRequest(new {success, message });
            }
        }

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="dateTime">The date time.</param>
        /// <returns> returns the operation result</returns>
        [HttpPut]
        [Route("{noteID}/Reminder")]
        public async Task<IActionResult> SetReminder(int noteID, ReminderRequest reminder)
        {
            bool success = false;
            var message = string.Empty;

            try
            {
                var userID = HttpContext.User.Claims.First(s => s.Type == "UserID").Value;

                var data = await this.noteBL.SetReminder(noteID, reminder.Reminder, userID);

                if(data != null)
                {
                    success = true;
                    message = "Reminder Added ";
                    return this.Ok(new { success, message ,data});
                }
                else
                {
                    success = false;
                    message = "Note doesn't exist";
                    return this.Ok(new { success, message });
                }
            }
            catch(Exception exception)
            {
                success = false;
                message = exception.Message;
                return this.BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Removes the reminder.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns> returns the operation result</returns>
        [HttpDelete]
        [Route("{noteID}/Reminder")]
        public async Task<IActionResult> RemoveReminder(int noteID)
        {
            bool success = false;
            var message = string.Empty;

            try
            {
                var userID = HttpContext.User.Claims.First(s => s.Type == "UserID").Value;
                var data = await this.noteBL.RemoveReminder(noteID, userID);

                if (data != null)
                {
                    success = true;
                    message = "Reminder Removed ";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Note doesn't exist";
                    return this.Ok(new { success, message });
                }
            }
            catch(Exception exception)
            {
                success = false;
                message = exception.Message;
                return this.BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="formFile">The form file.</param>
        /// <returns> returns the operation result</returns>
        [HttpPut]
        [Route("{noteID}/Image")]
        public async Task<IActionResult> ImageUpload(int noteID, IFormFile file)
        {
            bool success = false;
            var message = string.Empty;

            try
            {
                // get the user id
                var userID = HttpContext.User.Claims.First(s => s.Type == "UserID").Value;
                var data = await this.noteBL.ImageUpload(noteID, userID, file);

                // check whether data is null or not
                if(data != null)
                {
                    // if data is not null that means image is uploaded successfully
                    success = true;
                    message = "Image Uploaded ";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    // if data contains null value that means process of image uploading is failed
                    success = false;
                    message = "Failed to upload image";
                    return this.Ok(new { success, message });
                }
            }
            catch(Exception exception)
            {
                success = false;
                message = exception.Message;
                return this.BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Searches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>returns the list of notes or bad request result</returns>
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string key)
        {
            var message = string.Empty;
            bool success = false;

            try
            {
                // get the user id
                var userID = HttpContext.User.Claims.First(s => s.Type == "UserID").Value;

                IList<NoteResponse> result = this.noteBL.Search(key, userID);

                // check whether any note contains user entered key or not
                if (result.Count > 0)
                {
                    success = true;
                    return this.Ok(new { success, result });
                }
                else
                {
                    success = false;
                    message = "Note not found";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                success = false;
                message = exception.Message;
                return this.BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <returns> returns the operation result</returns>
        [HttpPut]
        [Route("BulkTrash")]
        public async Task<IActionResult> BulkTrash()
        {
            try
            {
                // get the user ID 
                var userId = this.HttpContext.User.Claims.First(s => s.Type == "UserID").Value;

                // Perform the operation and get the operation result
                var result = await this.noteBL.BulkTrash(userId);

                // check wheather the result variable contains true value or not
                if(result)
                {
                    // if result variable contains true value means opeartion is done successfully
                    return this.Ok(new { success = true, message = "Successfully deleted notes from trash" });
                }
                else
                {
                    // return message indicating opeartion is failed
                    return this.BadRequest(new { success = false, message = "Failed" });
                }
            }
            catch(Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }

        [HttpGet]
        [Route("Collaborators")]
        public async Task<IActionResult> GetContacts(string key)
        {
            try
            {
                var userID = this.HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                Dictionary<string,string> contactList = this.noteBL.GetContacts(key, userID);

                if (contactList.Count > 0)
                {
                    return this.Ok(new { success = true, contactList });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Contact not Found" });
                }
            }
            catch(Exception exception)
            {
                return this.BadRequest(new { success = false, message = exception.Message });
            }
        }
    }
}