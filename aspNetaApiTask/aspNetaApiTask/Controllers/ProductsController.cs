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
            var products = _context.Products
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.CategoryId ,
                    p.Category
                })
                .ToList();

            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Products = _context.Products.FirstOrDefault(x=>x.ProductId==id);
            return Ok(Products);
        }
        [HttpGet("{id}/{price}")]
        public IActionResult Get(int id, string price)
        {

            var Products = _context.Products.Where(x => x.CategoryId == id && string.Compare(x.Price, price) > 0).Count();
            return Ok(Products);
        }
    }
}
