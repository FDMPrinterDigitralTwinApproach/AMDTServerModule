using AMDTServerModule.Entities;
using AMDTServerModule.GenericRepository;
using AMDTServerModule.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace AMDTServerModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintersController : ControllerRepository<Printers, AmDbContext>
    {
        AmDbContext _context;
        public PrintersController(AmDbContext context) : base(context)
        {
            _context = context;
        }
        [Authorize]
        [HttpPost]
        public override async Task<ActionResult> Create(Printers entity)
        {
            string? token = HttpContext.GetTokenAsync("access_token").Result;
            if (token != null)
            {

                var handler = new JwtSecurityTokenHandler();

                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userID = Convert.ToInt32(jsonToken.Claims.First(claim => claim.Type == "sub").Value);

                entity.CreatedAt = DateTime.Now;
                entity.CreatedID = userID;
                entity.SpesificID = Guid.NewGuid();
                return await base.Create(entity);
            }
            return StatusCode(401);
        }
        [Authorize]
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<Printers>>> Get()
        {
            string? token = HttpContext.GetTokenAsync("access_token").Result;
            if (token != null)
            {

                var handler = new JwtSecurityTokenHandler();

                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userID = Convert.ToInt32(jsonToken.Claims.First(claim => claim.Type == "sub").Value);

                return await _context.Set<Printers>().Where(x => x.CreatedID == userID).ToListAsync();
            }
            return StatusCode(401);
        }
        [Authorize]
        [HttpGet("{id}")]
        public override async Task<ActionResult<Printers>> Get(int id)
        {
            return StatusCode(400);
            return await base.Get(id);
        }
        [Authorize]
        [HttpPut]
        public override async Task<ActionResult> Update(Printers entity)
        {
            return StatusCode(400);
            return await base.Update(entity);
        }
        [Authorize]
        [HttpDelete]
        public override async Task<ActionResult> Delete(int id)
        {
            return StatusCode(400);
            return await base.Delete(id);
        }
    }
}
