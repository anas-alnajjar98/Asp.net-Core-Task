using asp.netwebApiTask_8_24_2024_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.netwebApiTask_8_24_2024_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private MyDbContext _db;
        public OrdersController(MyDbContext db) { _db = db; }
        [HttpGet]
        public IActionResult getAllOrders() { return Ok(_db.Orders.ToList()); }
        [HttpGet("{id:int}")]
        public IActionResult GetOrderById(int id) {
            if (id <= 0) { return BadRequest(); }
            var orders = _db.Orders.Find(id);
            if (orders == null) { return NotFound(); }
            return Ok(orders);
        }
        [HttpGet("name/{name}")]
        public IActionResult GetOrderByName(string name)
        {
            if (string.IsNullOrEmpty(name)) { return BadRequest(); }
            var orders = _db.Orders.FirstOrDefault(x => x.User.Username == name);
            if (orders == null) { return NotFound(); };
            return Ok(orders);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id) {
            if (id <= 0) { return BadRequest(); }
            var orders = _db.Orders.Find( id);
            if (orders == null) { return NotFound(); }
            _db.Orders.Remove(orders);
            _db.SaveChanges();
            return Ok(orders);
        
        }

    }
}
