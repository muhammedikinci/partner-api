using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Application.Auth.Entities;

namespace WebApi.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [ApiController]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("admin-check")]
        public IActionResult AdminCheck()
        {
            return Ok(true);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return SetResponse(userService.GetAll());
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return SetResponse(userService.GetById(id));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult Create(Domain.Models.User user)
        {
            return SetResponse(userService.Add(user));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("create-partner")]
        public IActionResult CreateParter(Domain.Models.User user)
        {
            return SetResponse(userService.AddPartner(user));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(string id, Domain.Models.User user)
        {
            return SetResponse(userService.Update(id, user));
        }
        
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return SetResponse(userService.Delete(id));
        }
    }
}