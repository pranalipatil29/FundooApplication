using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooBusinessLayer.InterfaceBL;
using FundooCommonLayer.Model;
using FundooCommonLayer.Model.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private ILabelBL labelBL;
        
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }

        [HttpPost]
        [Route("CreateLabel")]
        // Post: /api/Note/CreateLabel
        public async Task<IActionResult> CreateLabel(RequestLabel requestLabel)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var result = await labelBL.CreateLabel(requestLabel, userID);

                bool success = false;
                var message = "";

                if(result)
                {
                    success = true;
                    message = "Label Added successfully";
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

                return BadRequest(new { exception.Message });
            }
        }

        [HttpPut]
        [Route("UpdateLabel")]
        // Post: /api/Note/CreateLabel
        public async Task<IActionResult> UpdateLabel(RequestLabel requestLabel,int labelID)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await labelBL.UpdateLabel(requestLabel, labelID,userID);

                bool success = false;
                var message = "";

                if (data != null)
                {
                    success = true;
                    message = "Successfully updated label";
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

                return BadRequest(new { exception.Message });
            }
        }

        [HttpPost]
        [Route("DeleteLabel")]
        // Post: /api/Note/DeleteLabel
        public async Task<IActionResult> DeleteLabel(int labelID)
        {
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var result = await labelBL.DeleteLabel(labelID, userId);

                bool success = false;
                var message = "";
                if (result)
                {
                    success = true;
                    message = "Succeffully Deleted Label";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "LabelID does not exist";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        [HttpGet]
        [Route("DisplayLabels")]
        // Post: /api/Note/DisplayNotes
        public async Task<IActionResult> DisplayLabels()
        {
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                IList<LabelModel> data = labelBL.DisplayLabels(userId);

                bool success = false;
                var message = "";

                if (data.Count != 0)
                {
                    success = true;
                    message = "Labels: ";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Labels not available";
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