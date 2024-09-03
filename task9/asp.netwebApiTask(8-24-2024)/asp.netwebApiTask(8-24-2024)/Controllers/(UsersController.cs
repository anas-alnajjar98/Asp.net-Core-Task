using asp.netwebApiTask_8_24_2024_.DTOs;
using asp.netwebApiTask_8_24_2024_.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Data;
using System.Net;

namespace asp.netwebApiTask_8_24_2024_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _UsersController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<_UsersController> _logger;
        private readonly TokenGenerator _tokenGenerator;


        public _UsersController(MyDbContext context, ILogger<_UsersController> logger, TokenGenerator tokenGenerator)
        {
            _context = context;
            _logger = logger;
            _tokenGenerator = tokenGenerator;   
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
            var users = _context.Users.Where(x => x.Username == name).FirstOrDefault();
            if (users==null) { return NotFound(); }
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


        [HttpPost("addnewuser")]
        public IActionResult addUser([FromForm] UserReqestDTO userDto)
        {
            byte[] Passwordhash;
            byte[] passwordSalt;
            HashingAlgoratim.CreatePasswordHash(userDto.Password, out Passwordhash, out passwordSalt);
            var user = new User
            {

                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                PasswordHash = Passwordhash,
                PasswordSalt = passwordSalt
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok();

        }

        [HttpGet("test")]
        public IActionResult Test1(string password)
        {
            byte[] Passwordhash;
            byte[] passwordSalt;
            HashingAlgoratim.CreatePasswordHash(password, out Passwordhash, out passwordSalt);

            byte[][] results = { Passwordhash, passwordSalt };
            return Ok(results);

        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser([FromForm] UserReqestDTO User) {

        var user=_context.Users.FirstOrDefault(x => x.Email == User.Email);



            if (user == null || !HashingAlgoratim.VerifyPasswordHash(User.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }
            var roles=_context.UserRoles.Where(x=>x.UserId==user.UserId).Select(x=>x.Role).ToList();
            var token = _tokenGenerator.GenerateToken(user.Username, roles);
            return Ok(new{token=token });
        }
    }
}
