using asp.netwebApiTask_8_24_2024_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.netwebApiTask_8_24_2024_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private MyDbContext _db;
        
        public ProductsController(MyDbContext db) { _db = db; }
        [HttpGet]
        public IActionResult GetAllProducts() { var product = _db.Products.ToList();return Ok(product); }
        [HttpGet("{id:int}")]
        public IActionResult GetProductById(int id)
        {
            if (id < 0 && id > 10) {
                return BadRequest();
            }
            var Product = _db.Products.FirstOrDefault(x => x.ProductId == id);
            if (Product == null) { return NotFound(); }
            return Ok(Product);
        }
        [HttpGet("name/{name}")]
        public IActionResult GetProductByName(string name) {
        if (name == null) { return NotFound(); }
            var GetProductByName = _db.Products.Where(x => x.ProductName == name).ToList();
            if (GetProductByName == null) { NotFound(); }
            return Ok(GetProductByName);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id ) {
            if (id <= 0) { return BadRequest(); }
            var product = _db.Products.FirstOrDefault( x => x.ProductId == id);
            if (product == null) { return NotFound(); };
            _db.Products.Remove(product);
            return Ok();
        }
    }
}
