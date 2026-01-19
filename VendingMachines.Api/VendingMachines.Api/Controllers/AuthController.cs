using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VendingMachines.Api.Data;
using VendingMachines.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace VendingMachines.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _cfg;

        public AuthController(AppDbContext db, IConfiguration cfg)
        {
            _db = db;
            _cfg = cfg;
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var role = User.FindFirstValue(ClaimTypes.Role);

            return Ok(new { userId, email, role });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password))
                return BadRequest(new { message = "Email и Password обязательны" });

            // ⚠️ Минимально. Правильно хранить ХЭШ пароля. Но ты просишь максимально просто.
            // Найдём пользователя в БД:
            var user = _db.Users
    .Include(u => u.Role)
    .FirstOrDefault(u => u.Email == req.Email);
            if (user == null)
                return Unauthorized(new { message = "Неверный логин или пароль" });

            // Тут зависит от твоей таблицы Users: как называется поле пароля.
            // Я предположу PasswordHash/Password. Замени под свою модель.
            // Пример:
            if (user.Password != req.Password) // <-- заменишь на правильное поле
                return Unauthorized(new { message = "Неверный логин или пароль" });

            // Роль (если есть)
            // user.RoleId есть, и есть навигация Role.RoleName — зависит от scaffold.
          
            var roleName = user.Role?.RoleName ?? "User";

            var token = CreateToken(user.UserId.ToString(), user.Email, roleName);

            return Ok(new
            {
                accessToken = token,
                tokenType = "Bearer"
            });
        }

        private string CreateToken(string userId, string email, string role)
        {
            var jwt = _cfg.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["Minutes"]!)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
