using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly NbaContext _context;

        public AuthController(NbaContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            // Check if the email is already registered
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest("Email already registered");
            }

            // Hash the password (you should use a proper hashing algorithm in production)
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Add the user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            // Find the user by email
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (dbUser == null)
            {
                return BadRequest("Invalid email or password");
            }

            // Verify the password
            if (!BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
            {
                return BadRequest("Invalid email or password");
            }

            return Ok("Login successful");
        }
    }
}