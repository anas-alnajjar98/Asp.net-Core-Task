using asp.netwebApiTask_8_24_2024_.DTOs;
using asp.netwebApiTask_8_24_2024_.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace asp.netwebApiTask_8_24_2024_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _UsersController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<_UsersController> _logger;

        
        public _UsersController(MyDbContext context, ILogger<_UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetAllUsers()
        {
            _logger.LogInformation("Success");
            return Ok(_context.Users.ToList());
        }

        [HttpGet("{id:int}")]
        
        public IActionResult GetUserById(int id)
        {
            if (id <= 0) { return BadRequest(); }
            var user = _context.Users.Find(id);
            if (user == null) { return NotFound(); }
            return Ok(user);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetUserByName(string name)
        {
            if (string.IsNullOrEmpty(name)) { return BadRequest(); }
            var users = _context.Users.Where(x => x.Username == name).ToList();
            if (!users.Any()) { return NotFound(); }
            return Ok(users);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            if (id <= 0) { return BadRequest(); }

            var user = _context.Users.Find(id);
            if (user == null) { return NotFound(); }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult PostUser([FromForm] UserReqestDTO user)
        {
            if (user == null) { throw new ArgumentNullException(nameof(user)); }
            var newUser = new User
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult EditUser(int id, [FromForm] UserReqestDTO user)
        {
            if (user == null) { throw new ArgumentNullException(nameof(user)); }
            var editUser = _context.Users.Find(id);
            if (editUser == null) { return NotFound(); }
            editUser.Username = user.Username;
            editUser.Password = user.Password;
            editUser.Email = user.Email;
            _context.SaveChanges();
            return Ok();
        }
    }
}
