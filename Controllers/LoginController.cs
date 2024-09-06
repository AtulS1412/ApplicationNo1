using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using System.IdentityModel.Tokens.Jwt;
  using Microsoft.IdentityModel.Tokens;
  using System.Security.Claims;
  using System.Text;
  using FirstApplication.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.AspNetCore.Authorization;

[AllowAnonymous]
public class LoginController : Controller
  {
      private readonly AppDbContext _context;
      private readonly IConfiguration _config;

      public LoginController(AppDbContext context, IConfiguration config)
      {
          _context = context;
          _config = config;
      }

      [HttpGet]
      public IActionResult Index() => View();

      [HttpPost]
      public async Task<IActionResult> Index(LoginModel model)
      {
          if (ModelState.IsValid)
          {
              var user = await _context.Users
                                       .FirstOrDefaultAsync(u => u.Username == model.Username);
              if (user != null && VerifyPassword(model.Password, user.Password))
              {
                  var token = GenerateJwtToken(user);
                  HttpContext.Session.SetString("JWToken", token);
                  HttpContext.Session.SetString("Username", model.Username);
                  return RedirectToAction("Index", "Home");
              }
              
              ModelState.AddModelError("", "Invalid login attempt.");
          }
          return View(model);
      }

      private bool VerifyPassword(string password, string passwordHash)
      {
          // Implement hashing verification, for example, using BCrypt
          return BCrypt.Net.BCrypt.Verify(password, passwordHash);
      }

      private string GenerateJwtToken(User user)
      {
          var tokenHandler = new JwtSecurityTokenHandler();
          var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
          var tokenDescriptor = new SecurityTokenDescriptor
          {
              Subject = new ClaimsIdentity(new[]
              {
                  new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                  new Claim(JwtRegisteredClaimNames.Email, user.EmailId.ToString())
              }),
              Expires = DateTime.UtcNow.AddMinutes(30),
              SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
          };
          var token = tokenHandler.CreateToken(tokenDescriptor);
          return tokenHandler.WriteToken(token);
      }
  }
