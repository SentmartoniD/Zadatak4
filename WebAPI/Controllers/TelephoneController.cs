﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelephoneController : ControllerBase
    {
        private readonly IValidation _validationService;
        private readonly IJSONHandler _JSONHandlerService;

        public TelephoneController(IValidation validationService, IJSONHandler JSONHandlerService)
        {
            _validationService = validationService;
            _JSONHandlerService = JSONHandlerService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult> Upload([FromBody] InputDTO input) {
            try
            {
                bool resValidate = await _validationService.Validate(input);
                if (resValidate)
                {
                    bool resSave = await _JSONHandlerService.Save(input);
                    if (resSave)
                        return Ok(new { Message = "Successful upload!" });
                    else
                        return BadRequest(new {Error = "Entity with the same values already exists!" });
                }
                else
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new { Error = "Invalid values for the attributes!"}); 
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = e.Message });
            }
        }

        [HttpGet("list-all")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                List<Input> list = await _JSONHandlerService.GetAll();
                return Ok(new { Results = list });
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = e.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                Input input = await _JSONHandlerService.GetById(id);
                if(input != null)
                    return Ok(new { Result = input });
                else
                    return BadRequest(new { Error = $"Entity with the id:{id} does not exists!" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = e.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            try
            {
                bool res = await _JSONHandlerService.DeleteById(id);
                if(res)
                    return Ok(new { Message = "Successfuly deleted!" });
                else
                    return BadRequest(new { Error = $"Entity with the id:{id} does not exists!" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = e.Message });
            }
        }

    }
}
