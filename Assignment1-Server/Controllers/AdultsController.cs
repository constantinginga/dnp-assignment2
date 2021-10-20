using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment1_Server.Persistence;
using System.Threading.Tasks;
using System.Collections.Generic;
using Assignment1_Server.Models;
using System;
using System.Text.Json;

namespace Assignment1_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdultsController : ControllerBase
    {
        private readonly IAdultData adultData;

        public AdultsController(IAdultData adultData) => this.adultData = adultData;

        [HttpGet]
        public async Task<ActionResult<IList<Adult>>> GetAdults()
        {
            try
            {
                IList<Adult> adults = await adultData.GetAdults();
                return Ok(adults);
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Adult>> AddAdult([FromBody] Adult adult)
        {
            try
            {
                Adult added = await adultData.Add(adult);
                return Created($"/{added.Id}", added);
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> RemoveAdult([FromRoute] int id)
        {
            try
            {
                await adultData.RemoveAdult(adultData.Get(id));
                return Ok();
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdateAdult([FromBody] Adult adult)
        {
            try
            {
                await adultData.UpdateAdult(adult);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
