using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly string _jwtKey;

    public AuthController(IUserService userService, IConfiguration config)
    {
        _userService = userService;
        _jwtKey = config["Jwt:Key"];
    }

    [Route("v1/Auth/Login")]
    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var user = _userService.GetUserByEmailAndPassword(email, password);
        if (user == null) return Unauthorized("Invalid credentials");
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }

    [Route("v1/User/CreateUser")]
    [HttpPost]
    public IActionResult CreateAccessUser(User user)
    {
        try
        {
            _userService.CreateUser(user);
            return Ok("User created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
