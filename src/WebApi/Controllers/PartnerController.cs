using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Application.Auth.Entities;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/partner")]
    public class PartnerController : BaseController
    {
        private IPartnerService partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            this.partnerService = partnerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return SetResponse(partnerService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return SetResponse(partnerService.GetById(id));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult Create(Partner partner)
        {
            return SetResponse(partnerService.Add(partner));
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Partner partner)
        {
            return SetResponse(partnerService.Update(id, partner));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return SetResponse(partnerService.Delete(id));
        }

        [HttpGet("{id}/products")]
        public IActionResult GetProducts(string id)
        {
            return SetResponse(partnerService.GetProducts(id));
        }

        [HttpGet("{id}/orders")]
        public IActionResult GetOrders(string id)
        {
            return SetResponse(partnerService.GetOrders(id));
        }
    }
}