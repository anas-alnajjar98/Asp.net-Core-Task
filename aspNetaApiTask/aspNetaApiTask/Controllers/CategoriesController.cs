using aspNetaApiTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspNetaApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private EcommerceCoreContext _context;
        public CategoriesController(EcommerceCoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get() {
        var categories = _context.Categories.ToList();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var categories = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
            return Ok(categories);
        }
    }
}
