
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.DTOs;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RaceDayContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(RaceDayContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string role = request.Role.Trim();

            if (role != "Participant" && role != "Organiser")
            {
                return BadRequest(
                    "Role must be Participant or Organiser.");
            }

            string email = request.Email.Trim().ToLowerInvariant();

            bool exists = await _context.Users
                .AnyAsync(u => u.Email == email);

            if (exists)
                return Conflict("Email is already registered.");

            var user = new User
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = email,
                Role = role
            };

            user.PasswordHash = _passwordHasher.HashPassword(
                user, request.Password);

            _context.Users.Add(user);

            if (role == "Organiser")
            {
                _context.Organisers.Add(new Organiser
                {
                    User = user,
                    OrganisationName =
                        $"{user.FirstName}'s Organisation"
                });
            }
            else
            {
                _context.Participants.Add(new Participant
                {
                    User = user
                });
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Registration successful.",
                user.UserID,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Role
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string email = request.Email.Trim().ToLowerInvariant();

            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return Unauthorized("Invalid email or password.");

            var result = _passwordHasher.VerifyHashedPassword(
                user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid email or password.");

            return Ok(new
            {
                message = "Login successful.",
                user.UserID,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Role
            });
        }
    }
}
