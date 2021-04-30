using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartnerController : ControllerBase
    {
        private IPartnerService partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            this.partnerService = partnerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(partnerService.GetAll());
        }

        [HttpPost]
        public IActionResult Create(Partner partner)
        {
            return Ok(partnerService.Add(partner));
        }

        [HttpPut]
        public IActionResult Update(Partner partner)
        {
            return Ok(partnerService.Update(partner));
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            return Ok(partnerService.Delete(id));
        }
    }
}