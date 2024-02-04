using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelephoneController : ControllerBase
    {
        private readonly IValidation _validationService;

        public TelephoneController(IValidation validationService)
        {
            _validationService = validationService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult> Upload([FromBody] InputDTO input) {
            try
            {
                bool res = await _validationService.Validate(input);
                if (res)
                    return Ok(new { Message = "Successful upload!" });
                else
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new { Error = "Invalid values for the attributes!" }); 
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Internal server error!" });
            }
        }
    }
}
