using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly UserDbContext _context;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        private IConfiguration _config;
        public AuthController(UserDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet]
        public User GetUser(User valuser)
        {
            var user = _context.Users.FirstOrDefault(c => c.Username == valuser.Username && c.Password == valuser.Password);

            if (user == null)
            {

                return null;
            }

            return user;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User login)
        {

            _log4net.Info(" login method is run");

            IActionResult response = Unauthorized();
            User user = GetUser(login);
            if (user == null)
            {
                return NotFound();
            }
            else
            {

                _log4net.Info("Login credential matched");
                return Ok(new
                {
                    token = GenerateJSONWebToken(user)
                });



            }
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
