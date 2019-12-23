using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using FundooBusinessLayer.InterfaceBL;
using FundooCommonLayer.Model;
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
        public async Task<IActionResult> CreateNote(NoteModel noteModel)
        {
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                // storing new account info in database
                var result = await noteBL.CreateNote(noteModel, userId);
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

                // storing new account info in database
                var result = await noteBL.DeleteNote(noteID);
                bool success = false;
                var message = "";

                // checking whether result is successfull or nor
                if (result)
                {
                    // if yes then return the result 
                    success = true;
                    message = "Succeffully Deleted Note";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "Note Id does not exist";
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