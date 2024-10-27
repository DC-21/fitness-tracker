using Microsoft.AspNetCore.Mvc;
using Fit.Data;
using Fit.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Fit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly FitDbContext _context;

        public AuthController(FitDbContext context)
        {
            _context = context;
        }

        // Registration endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            // Validate the user input (add more validations as needed)

            // Hash the password before saving (ensure you install bcrypt)
            using (var hmac = new HMACSHA512())
            {
                user.Password = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)));
            }

            // Save user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully" });
        }

        // Login endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return Unauthorized("Invalid email or password");

            // Hash the incoming password and compare it with the stored password
            using (var hmac = new HMACSHA512())
            {
                var hashedPassword = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                if (user.Password != hashedPassword) return Unauthorized("Invalid email or password");
            }

            return Ok(new { Message = "Login successful" });
        }
    }
}
