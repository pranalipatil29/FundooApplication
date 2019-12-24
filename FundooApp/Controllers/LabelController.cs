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
                var result = await labelBL.UpdateLabel(requestLabel, labelID);

                bool success = false;
                var message = "";

                if (result != null)
                {
                    success = true;
                    message = "Successfully updated label";
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

                return BadRequest(new { exception.Message });
            }
        }
    }
}