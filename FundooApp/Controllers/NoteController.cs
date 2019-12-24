using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using FundooBusinessLayer.InterfaceBL;
using FundooCommonLayer.Model;
using FundooCommonLayer.Model.Request;
using FundooRepositoryLayer.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL; 
        

        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }

        [HttpPost]
        [Route("CreateNote")]
        // Post: /api/Note/CreateNote
        public async Task<IActionResult> CreateNote(RequestNote requestNote)
        {
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                var result = await noteBL.CreateNote(requestNote, userId);
                bool success = false;
                var message = "";

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
            catch(Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        [HttpPost]
        [Route("DeleteNote")]
        // Post: /api/Note/DeleteNote
        public async Task<IActionResult> DeleteNote(int noteID)
        {
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var result = await noteBL.DeleteNote(noteID,userId);
                bool success = false;
                var message = "";
                if (result)
                {
                    success = true;
                    message = "Succeffully Deleted Note";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "NoteId does not exist";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        [HttpPut]
        [Route("UpdateNote")]
        // Put: /api/Note/UpdateNote
        public async Task<IActionResult> UpdateNote(RequestNote noteRequest,int noteID)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                var result = await noteBL.UpdateNote(noteRequest, noteID, userID);

                bool success = false;
                var message = "";
                if(result != null)
                {
                    success = true;
                    message = "Updated Successfully";
                    return this.Ok(new { success, message,result});
                }
                else
                {
                    success = false;
                    message = "Failed";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception exception)
            {
                return this.BadRequest(exception.Message);
            }
        }

        [HttpGet]
        [Route("DisplayNotes")]
        // Post: /api/Note/DisplayNotes
        public async Task<IActionResult> DisplayNotes()
        {
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                IList<NoteModel> data =   noteBL.DisplayNotes(userId);

                bool success = false;
                var message = "";

                if (data.Count != 0)
                {
                    success = true;
                    message = "Notes: ";
                    return this.Ok(new { success, message, data});
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
    }
}