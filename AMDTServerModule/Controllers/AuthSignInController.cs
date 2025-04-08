using AMDTServerModule.Entities;
using AMDTServerModule.Helpers;
using AMDTServerModule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AMDTServerModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthSignInController : ControllerBase
    {
        private IConfiguration _config;
        private AmDbContext _context;
        public AuthSignInController(AmDbContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }
        [AllowAnonymous]

        [HttpPost(Name = "PostAuthSignIn")]
        public async Task<ActionResult> Add(LoginEntity loginEntity)
        {

            try
            {
                var user = await _context.Set<Users>().Where(x => x.Username.Equals(loginEntity.Username)).ToListAsync();

                if (user.Count < 1)
                {
                    return StatusCode(404, "User Not Found");
                }
                var hashedPassword = user[0].UserPassword;

                // Now verify the password entered by the user
                bool isPasswordCorrect = BCrypt.Net.BCrypt.EnhancedVerify(loginEntity.Pass, hashedPassword);

                if (isPasswordCorrect)
                {
                  
                        var issuer = _config["Jwt:Issuer"];
                        var audience = _config["Jwt:Audience"];
                        var key = Encoding.UTF8.GetBytes
                        (_config["Jwt:Key"]);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[]
                            {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user[0].ID.ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, loginEntity.Username),
                            new Claim(JwtRegisteredClaimNames.Jti,
                             Guid.NewGuid().ToString())
                        }),
                            Expires = DateTime.UtcNow.AddMinutes(30),
                            Issuer = issuer,
                            Audience = audience,

                            SigningCredentials = new SigningCredentials
                            (new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha512Signature)
                        };
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var jwtToken = tokenHandler.WriteToken(token);
                        var stringToken = tokenHandler.WriteToken(token);
                        return Ok(new { UserID = user[0].ID, Username = user[0].Username, AuthAPI = stringToken });
                    }
                    else
                    {
                        return StatusCode(401, "Password is Invalid");
                    }
            }
            catch (Exception ex)
            {
                CustomLogger.SendErrorToText(ex, this.HttpContext);
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
