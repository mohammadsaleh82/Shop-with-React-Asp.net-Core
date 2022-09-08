using Core.Services.Interfaces;
using DataLayer.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{ 
    public class ProductController : BaseController
    {
        private IProduct _productservice;

        public ProductController(IProduct productservice)
        {
            _productservice = productservice;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() =>
            await _productservice.GetProducts();

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id) =>
            await _productservice.GetProductById(id);
    }
}
