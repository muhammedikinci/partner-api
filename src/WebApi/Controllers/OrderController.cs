using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : BaseController
    {
        private IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

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

        [HttpPost]
        public IActionResult Create(Order order)
        {
            return SetResponse(orderService.Add(order));
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Order order)
        {
            return SetResponse(orderService.Update(id, order));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return SetResponse(orderService.Delete(id));
        }

        [HttpGet("{id}/products")]
        public IActionResult GetProducts(string id)
        {
            return SetResponse(orderService.GetProducts(id));
        }

        [HttpGet("{id}/partner")]
        public IActionResult GetPartner(string id)
        {
            return SetResponse(orderService.GetPartner(id));
        }
    }
}