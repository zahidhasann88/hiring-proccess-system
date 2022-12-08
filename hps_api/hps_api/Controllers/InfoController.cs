using hps_api.DTOs;
using hps_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hps_api.Controllers
{

    public class MyClass1
    {
        public int? Id { get; set; }
    }

    public class MyClass2
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : Controller
    {
        private readonly PostgresContext _context;

        public InfoController(PostgresContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetInfos()
        {
            List<Info> infos = await _context.Infos.ToListAsync();

            if (infos.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, new ResponseDto
                {
                    Message = "Info list",
                    Success = true,
                    Payload = infos
                });
            }

            return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
            {
                Message = "not found infos",
                Success = false,
                Payload = null
            });

        }
        [HttpPost("GetInfoById")] // read - id (R)
        public async Task<ActionResult<ResponseDto>> GetInfos([FromBody] MyClass1 input)
        {
            if (input.Id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "id error",
                    Success = false,
                    Payload = null
                });
            }

     
            var info = await _context.Infos.Where(i => i.Id >= input.Id).FirstOrDefaultAsync();

            if (info == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "No Info",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "info",
                Success = true,
                Payload = info
            });
        }

        [HttpPost("CreateInfo")]
        public async Task<ActionResult<ResponseDto>> PostInfo([FromBody] Info input)
        {
            if (input.Name == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " name is null",
                    Success = false,
                    Payload = null
                });
            }

            Info info = await _context.Infos.Where(i => i.Id == input.Id).FirstOrDefaultAsync();
            if (info != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ResponseDto
                {
                    Message = "already exist",
                    Success = false,
                    Payload = null
                });
            }

            _context.Infos.Add(input);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "creating error",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "create done",
                Success = true,
                Payload = new { input.Id }
            });
        }

        [HttpPut("UpdateInfo")]
        public async Task<ActionResult<ResponseDto>> PutInfo([FromBody] Info input)
        {
            if (input.Id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " id is null",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Name == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Name is null",
                    Success = false,
                    Payload = null
                });
            }

            //old 
            Info info = await _context.Infos.Where(i => i.Id == input.Id).FirstOrDefaultAsync();
            if (info == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "this info not listed your db",
                    Success = false,
                    Payload = null
                });
            }

            //new
            info.Name = input.Name;
            info.Country = input.Country;
            info.City = input.City;
            info.Skills = input.Skills;
            info.DateOfBirth = input.DateOfBirth;
            info.Resume = input.Resume;
            _context.Infos.Update(info);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "updating Unsuccesfull",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "updating complete",
                Success = true,
                Payload = null
            });
        }
        // DELETE: api/Countries/5 // delete (d)
        [HttpDelete("DeleteInfo")]
        public async Task<ActionResult<ResponseDto>> DeleteInfo([FromBody] MyClass1 input)
        {
            if (input.Id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " id is null",
                    Success = false,
                    Payload = null
                });
            }

            Info info = await _context.Infos.Where(i => i.Id == input.Id).FirstOrDefaultAsync();
            if (info == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "No exist in your database",
                    Success = false,
                    Payload = null
                });
            }

            _context.Infos.Remove(info);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "deleting error",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "deleted",
                Success = true,
                Payload = new { input.Id } // optional, can be null too like update
            });
        }
        private bool InfoExists(int? id)
        {
            return _context.Infos.Any(e => e.Id == id);
        }
    }
}
