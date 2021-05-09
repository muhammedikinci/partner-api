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

        [HttpGet]
        public IActionResult GetAll()
        {
            return SetResponse(userService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return SetResponse(userService.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(Domain.Models.User user)
        {
            return SetResponse(userService.Add(user));
        }

        [HttpPost("create-partner")]
        public IActionResult CreateParter(Domain.Models.User user)
        {
            return SetResponse(userService.AddPartner(user));
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Domain.Models.User user)
        {
            return SetResponse(userService.Update(id, user));
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return SetResponse(userService.Delete(id));
        }
    }
}