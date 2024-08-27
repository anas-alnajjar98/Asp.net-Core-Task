using asp.netwebApiTask_8_24_2024_.DTOs;
using asp.netwebApiTask_8_24_2024_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.netwebApiTask_8_24_2024_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CategoriesController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(_context.Categories.ToList());
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public IActionResult GetCategoryById(int id)
        {
            if (id < 5)
                return BadRequest("ID must be greater than or equal to 5");

            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetCategoryByName(string name)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryName == name);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpPost]
        public IActionResult CreateNewCategory([FromForm] newCategory category) {
            if (category.CategoryImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, category.CategoryImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    category.CategoryImage.CopyToAsync(stream);
                }

            }
            var NewCat = new Category();
            NewCat.CategoryName=category.CategoryName;
            NewCat.CategoryImage=category.CategoryImage.FileName;
            _context.Categories.Add(NewCat);
            _context.SaveChanges();
            return Ok(category);
      
        

        
        }
        [HttpPut("{id}")]
        public IActionResult updateCategory(int id, [FromForm] newCategory category)
        {

            if (category.CategoryImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, category.CategoryImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    category.CategoryImage.CopyToAsync(stream);
                }

            }
            var c = _context.Categories.FirstOrDefault(l => l.CategoryId == id);

            c.CategoryName = category.CategoryName ?? c.CategoryName;

            c.CategoryImage = category.CategoryImage.FileName ?? c.CategoryImage;



            _context.Categories.Update(c);
            _context.SaveChanges();

            return Ok();

        }

    }

}
