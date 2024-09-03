using asp.netwebApiTask_8_24_2024_.DTOs;
using asp.netwebApiTask_8_24_2024_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace asp.netwebApiTask_8_24_2024_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private MyDbContext _db;

        public ProductsController(MyDbContext db) { _db = db; }
        [HttpGet]
        public IActionResult GetAllProducts() { var product = _db.Products.ToList(); return Ok(product); }
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
        [Authorize]
        [HttpGet("GetProductsBasedOnCategoryId/{id}")]
        public IActionResult GetProductsBasedOnCategoryId(int id)
        {
            if (id <= 0) { return BadRequest(); }
            var Product = _db.Products.Where(x => x.CategoryId == id).ToList();
            if (Product == null) { return NotFound(); };
            return Ok(Product);

        }
        [HttpGet("GetAllProductbyprice")]
        public IActionResult GetAllProductbyprice() {
            var products=_db.Products.OrderByDescending(x => x.Price).ToList();
            return Ok(products);
        }
        [HttpPost]
        public IActionResult postProduct([FromForm] productRequestDTO product)
        {
            if (product.ProductImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, product.ProductImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProductImage.CopyToAsync(stream);
                }

            }

            var pro = new Product
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ProductImage = product.ProductImage.FileName
            };

            _db.Products.Add(pro);
            _db.SaveChanges();
            return Ok(pro);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id ) {
            if (id <= 0) { return BadRequest(); }
            var product = _db.Products.FirstOrDefault( x => x.ProductId == id);
            if (product == null) { return NotFound(); };
            _db.Products.Remove(product);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult putProduct(int id, [FromForm] productRequestDTO product)
        {
            if (product.ProductImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, product.ProductImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProductImage.CopyToAsync(stream);
                }

            }
            var x = _db.Products.FirstOrDefault(l => l.ProductId == id);


            x.ProductName = product.ProductName;
            x.Description = product.Description;
            x.Price = product.Price;
            x.CategoryId = product.CategoryId;
            x.ProductImage = product.ProductImage.FileName;

            

            _db.SaveChanges();
            return Ok(x);
        }
        [HttpGet("getLastFive")]
        public IActionResult getProduct() {

            var product = _db.Products.OrderBy(p=>p.ProductName).Reverse().Take(5).ToList();
            return Ok(product); }
        [HttpGet("lastlastof5")]
        public IActionResult getLastProduct() {
            var product=_db.Products.OrderBy(p=>p.ProductName).ToList().TakeLast(5);
        return Ok(product);
        }

    }
}
