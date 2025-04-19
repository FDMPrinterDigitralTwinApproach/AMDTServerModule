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
    public class PrinterFarmsController : ControllerRepository<PrinterFarms, AmDbContext>
    {
        AmDbContext _context;
        public PrinterFarmsController(AmDbContext context) : base(context)
        {
            _context = context;
        }
        [Authorize]
        [HttpPost]
        public override async Task<ActionResult> Create(PrinterFarms entity)
        {
            string? token = HttpContext.GetTokenAsync("access_token").Result;
            if (token != null)
            {

                var handler = new JwtSecurityTokenHandler();

                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userID = Convert.ToInt32(jsonToken.Claims.First(claim => claim.Type == "sub").Value);
                entity.CreatedBy = userID;

                return await base.Create(entity);
            }
            return StatusCode(401);
        }
        [Authorize]
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<PrinterFarms>>> Get()
        {
            string? token = HttpContext.GetTokenAsync("access_token").Result;
            if (token != null)
            {
                
                var handler = new JwtSecurityTokenHandler();

                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userID = Convert.ToInt32(jsonToken.Claims.First(claim => claim.Type == "sub").Value);

                return await _context.Set<PrinterFarms>().Where(x => x.CreatedBy == userID).ToListAsync();
            }
            return StatusCode(401);
        }
        [Authorize]
        [HttpGet("{id}")]
        public override async Task<ActionResult<PrinterFarms>> Get(int id)
        {
            return StatusCode(400);
            return await base.Get(id);
        }
        [Authorize]
        [HttpPut]
        public override async Task<ActionResult> Update(PrinterFarms entity)
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
