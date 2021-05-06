using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Application.Auth.Entities;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/order")]
    public class OrderController : BaseController
    {
        private IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return SetResponse(orderService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return SetResponse(orderService.GetById(id));
        }

        [HttpGet("get-all-my-orders")]
        public IActionResult GetAllByPartnerId()
        {
            return SetResponse(orderService.GetAllByPartnerId());
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult Create(Order order)
        {
            return SetResponse(orderService.Add(order));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(string id, Order order)
        {
            return SetResponse(orderService.Update(id, order));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return SetResponse(orderService.Delete(id));
        }

        [HttpGet("{id}/partner")]
        public IActionResult GetPartner(string id)
        {
            return SetResponse(orderService.GetPartner(id));
        }
    }
}