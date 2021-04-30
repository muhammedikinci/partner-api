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

        [HttpGet]
        public IActionResult GetAll()
        {
            return SetResponse(productService.GetAll());
        }

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

        [HttpPut("{id}")]
        public IActionResult Update(string id, Product product)
        {
            return SetResponse(productService.Update(id, product));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return SetResponse(productService.Delete(id));
        }

        [HttpGet("{id}/partner")]
        public IActionResult GetPartner(string id)
        {
            return SetResponse(productService.GetPartner(id));
        }
    }
}