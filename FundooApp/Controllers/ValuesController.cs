// ******************************************************************************
//  <copyright file="ValuesController.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ValuesController.cs
//  
//     Purpose: Creating a controller to manage api calls
//     @author  Pranali Patil
//     @version 1.0
//     @since   23-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// handle the API class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// handle Get API call
        /// </summary>
        /// <returns> returns string value</returns>
        ////GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// handle Get API call and returns the string value
        /// </summary>
        /// <param name="id"> identity value</param>
        /// <returns> returns string value</returns>
        ////GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// handle post API call 
        /// </summary>
        /// <param name="value"> values to be post</param>
        ////POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// handle put API call
        /// </summary>
        /// <param name="id"> identity value</param>
        /// <param name="value"> string value</param>
        ////PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// handle API call for delete info
        /// </summary>
        /// <param name="id"> identity value</param>
        ////DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
