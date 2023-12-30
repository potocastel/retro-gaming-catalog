using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RetroGamingCatalog.Dao;

namespace RetroGamingCatalog.Api;


[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;
    private readonly CatalogDb _db;

    public AccountController(CatalogDb db, JwtSettings jwtSettings)
    {
        _db = db;
        _jwtSettings = jwtSettings;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == model.UserName);
            if (user == null)
                return NotFound();

            if (user.PasswordHash == model.Password)
            {
                user.LastConnectionDate = DateTime.Now;
                await _db.SaveChangesAsync();
                var token = GenerateJwtToken(user);

                return Ok(new { Token = token, Message = "Login successful" });
            }
            else
            {
                return Unauthorized(new { Message = "Invalid login attempt" });
            }
        }

        return BadRequest(new { Message = "Invalid model state" });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            // Ajoutez d'autres revendications en fonction des besoins de votre application
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

