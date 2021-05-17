using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Application.Auth.Entities;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/product-request")]
    public class ProductRequestController : BaseController
    {
        private IProductRequestService productRequestService;

        public ProductRequestController(IProductRequestService productRequestService)
        {
            this.productRequestService = productRequestService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return SetResponse(productRequestService.GetAll());
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return SetResponse(productRequestService.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(Domain.Models.ProductRequest productRequest)
        {
            return SetResponse(productRequestService.Add(productRequest));
        }

        [HttpPost("get-all-my-requests")]
        public IActionResult GetAllMyRequests()
        {
            return SetResponse(productRequestService.GetAllMyRequests());
        }

        [HttpPost("update-my-request")]
        public IActionResult UpdateMyRequest(Domain.Models.ProductRequest productRequest)
        {
            return SetResponse(productRequestService.UpdateMyRequest(productRequest));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(string id, Domain.Models.ProductRequest productRequest)
        {
            return SetResponse(productRequestService.Update(id, productRequest));
        }
        
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return SetResponse(productRequestService.Delete(id));
        }
    }
}