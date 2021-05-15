using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Application.Auth.Entities;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/product")]
    public class ProductController : BaseController
    {
        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return SetResponse(productService.GetAll());
        }

        [HttpGet("get-all-my-products")]
        public IActionResult GetAllByPartnerId()
        {
            return SetResponse(productService.GetAllByPartnerId());
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return SetResponse(productService.GetById(id));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult Create(Product product)
        {
            return SetResponse(productService.Add(product));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("update-or-create")]
        public IActionResult UpdateOrCreate(Product product)
        {
            return SetResponse(productService.UpdateOrCreate(product));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(string id, Product product)
        {
            return SetResponse(productService.Update(id, product));
        }

        [HttpPut("set-stock/{id}")]
        public IActionResult UpdateStock(string id, Domain.ValueObjects.StockUpdate stock)
        {
            return SetResponse(productService.UpdateStock(id, stock));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return SetResponse(productService.Delete(id));
        }
    }
}