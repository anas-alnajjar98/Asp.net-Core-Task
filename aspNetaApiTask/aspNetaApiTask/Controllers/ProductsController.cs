using aspNetaApiTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspNetaApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private EcommerceCoreContext _context;
        public ProductsController(EcommerceCoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var Products = _context.Products.ToList();
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Products = _context.Products.FirstOrDefault(x=>x.ProductId==id);
            return Ok(Products);
        }
    }
}
