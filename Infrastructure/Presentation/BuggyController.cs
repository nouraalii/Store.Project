using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")] //GET : /api/Buggy/notfound
        public IActionResult GetNotFoundRequest() 
        {
            //Code
            return NotFound(); //404
        }

        [HttpGet("servererror")] //GET : /api/Buggy/servererror
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception();  
            return Ok();
        }

        [HttpGet(template: "badrequest")] //GET : /api/Buggy/badrequest
        public IActionResult GetBadRequest() 
        {
            //code
            return BadRequest(); //400
        }

        [HttpGet(template: "badrequest/{id}")] //GET : /api/Buggy/badrequest/1
        public IActionResult GetBadRequest(int id) //Validation Error
        {
            //code
            return BadRequest(); //400
        }

        [HttpGet(template: "unauthorized")] //GET : /api/Buggy/anauthorized
        public IActionResult GetUnauthorizedRequest()
        {
            //code
            return Unauthorized(); //401
        }
    }
}
