using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NguyenNgocPhuongNguyen_2122110413.Model;
using NguyenNgocPhuongNguyen_2122110413.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using NguyenNgocPhuongNguyen_2122110413.DTOs.User;

namespace NguyenNgocPhuongNguyen_2122110413.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return BadRequest("Email already exists");

            var user = new User
            {
                Username = request.Username,
                Password = ComputeSha256Hash(request.Password),
                Email = request.Email,
                Role =  "User" 
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registration successful");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || user.Password != ComputeSha256Hash(request.Password))
                return Unauthorized("Invalid username or password");

            // Tạo list claims
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role),
    };

            // Key để ký token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this-is-a-very-strong-secret-key-123!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tạo token
            var token = new JwtSecurityToken(
                issuer: "yourapp",
                audience: "yourapp",
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = jwt,
                user = new
                {
                    id = user.Id,
                    username = user.Username,
                    email = user.Email,
                    role = user.Role,
                }
            });
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new { u.Id, u.Username, u.Role })
                .ToListAsync();

            return Ok(users);
        }
        // Thêm User (Chỉ Admin được thêm)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] User request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return BadRequest("Username already exists");

            var user = new User
            {
                Username = request.Username,
                Password = ComputeSha256Hash(request.Password),
                Role = string.IsNullOrEmpty(request.Role) ? "User" : request.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User created successfully" });
        }

        // Sửa User (Chỉ Admin)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found");

            user.Username = updatedUser.Username ?? user.Username;
            if (!string.IsNullOrEmpty(updatedUser.Password))
                user.Password = ComputeSha256Hash(updatedUser.Password); // Nếu có đổi mật khẩu

            user.Role = updatedUser.Role ?? user.Role;

            await _context.SaveChangesAsync();
            return Ok(new { message = "User updated successfully" });
        }

        // Xóa User (Chỉ Admin)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User deleted successfully" });
        }

        private string ComputeSha256Hash(string rawData)
        {
            using var sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            var builder = new StringBuilder();
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));

            return builder.ToString();
        }
    }
}
