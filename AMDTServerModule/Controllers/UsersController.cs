using AMDTServerModule.Entities;
using AMDTServerModule.GenericRepository;
using AMDTServerModule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMDTServerModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerRepository<Users, AmDbContext>
    {
        AmDbContext _context;
        public UsersController(AmDbContext context) : base(context)
        {
            _context = context;
        }
        [HttpPost]
        public override async Task<ActionResult> Create(Users entity)
        {
            var userList = await _context.Set<Users>().Where(x => x.Username.Equals(entity.Username)).ToListAsync();
            if(userList.Count > 0)
            {
                return StatusCode(400, "Var Olan Kullanıcı Adı ile Kayıt Olunamaz");
            }
            entity.UserPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(entity.UserPassword, 13);
            return await base.Create(entity);
        }
        [Authorize]
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<Users>>> Get()
        {
            return await base.Get();
        }
        [Authorize]
        [HttpGet("{id}")]
        public override async Task<ActionResult<Users>> Get(int id)
        {
            return await base.Get(id);
        }
        [Authorize]
        [HttpPut]
        public override async Task<ActionResult> Update(Users entity)
        {
            return await base.Update(entity);
        }
        [Authorize]
        [HttpDelete]
        public override async Task<ActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
    }
}
