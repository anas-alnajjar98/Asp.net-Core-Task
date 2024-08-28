using asp.netwebApiTask_8_24_2024_.DTOs;
using asp.netwebApiTask_8_24_2024_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.netwebApiTask_8_24_2024_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _UsersController : ControllerBase
    {
        private MyDbContext _context;
        public _UsersController(MyDbContext context) { _context = context; }
        [HttpGet]
        public IActionResult getAllUsers() {
            return Ok(_context.Users.ToList());
        }
        [HttpGet("{id:int}")]
        public IActionResult GetUserById(int id) {
            if (id <= 0) { return BadRequest(); }
            var user = _context.Users.Find(id);
            if (user == null) { return NotFound(); }
            return Ok(user);
        
        }
        [HttpGet("name/{name}")]
        public IActionResult GetUserByName(string name) {
            if (name == null) {return BadRequest(); }
            var user = _context.Users.Where(x=>x.Username==name).ToList();
            if (user == null) { return NotFound(); };
            return Ok(user);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id) { if (id <= 0) { return BadRequest(); }

            var user = _context.Users.Find(id);
            if (user == null) { return NotFound(); }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost]
        public IActionResult PostUser([FromForm] UserReqestDTO user) {
            if (user == null) { throw new ArgumentNullException("user"); }
            var newuser = new User();
            newuser.Username = user.Username;
            newuser.Password = user.Password;
            newuser.Email = user.Email;
            _context.Users.Add(newuser);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult EditUser(int id,[FromForm] UserReqestDTO user) {
            if (user == null) { throw new ArgumentNullException("user"); }
            var edituser=_context.Users.Find(id);
            if (edituser == null) { return NotFound(); }
            edituser.Username = user.Username;
            edituser.Password = user.Password;
            edituser.Email = user.Email;
            _context.SaveChanges();
            return Ok();

        }
    }
}
