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
    using FundooCommonLayer.Model.Response;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// this class contains different methods to handle API calls for note
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
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
               var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                var result = await this.noteBL.CreateNote(noteRequest, userId);
                bool success = false;
                var message = string.Empty;

                // checking whether result is successfull or nor
                if (result)
                {
                    // if yes then return the result 
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
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var result = await this.noteBL.DeleteNote(noteID, userId);
                bool success = false;
                var message = string.Empty;
                if (result)
                {
                    success = true;
                    message = "Succeffully Deleted Note";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "Note doesn't exist in trash";
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
      //  [Route("UpdateNote")]
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
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                IList<NoteResponse> data = this.noteBL.DisplayNotes(userId);

                bool success = false;
                var message = string.Empty;

                if (data.Count != 0)
                {
                    success = true;
                    message = "Notes: ";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Notes not available";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

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
                    message = "Note doesn't exist";
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
        public async Task<IActionResult> Archive(int noteID, bool archive)
        {
            bool success = false;
            var message = string.Empty;

            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var result = await this.noteBL.IsArchive(noteID, archive, userID);

                if (result)
                {
                    success = true;
                    message = "Note Archived";
                    return this.Ok(new { success, message });
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
                    message = "Archived Notes :";
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
        public async Task<IActionResult> IsPin(int noteID, bool isPin)
        {
            bool success = false;
            var message = string.Empty;

            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var result = await this.noteBL.IsPin(noteID, isPin, userID);

                if (result)
                {
                    success = true;
                    message = "Note Pinned";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "UnPinned Note";
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
                    message = "Pinned Notes: ";
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

        [HttpPost]
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
                    message = "Notes In Trash :- ";
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

        [HttpPut]
        [Route("{noteID}/Restore")]
        public async Task<IActionResult> RestoreFromTrash(int noteID)
        {
            bool success = false;
            var message = string.Empty;
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                var result = await this.noteBL.RestoreFromTrash(noteID, userId);

                if (result)
                {
                    success = true;
                    message = "Successfully Restored Note";
                    return this.Ok(new { success, message });
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
    }
}