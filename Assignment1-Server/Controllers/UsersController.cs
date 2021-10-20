using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment1_Server.Models;
using Assignment1_Server.Persistence;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Assignment1_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService) => this.userService = userService;

        [HttpGet]
        public async Task<ActionResult<IList<User>>> GetUsers()
        {
            try
            {
                IList<User> users = await userService.GetUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            try
            {
                userService.ValidateUser(user.Username, user.Password);
                User added = await userService.Add(user);
                return Created($"/{added.Username}", added);
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
