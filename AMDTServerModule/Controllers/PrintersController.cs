using AMDTServerModule.Entities;
using AMDTServerModule.GenericRepository;
using AMDTServerModule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            return await base.Create(entity);
        }
        [Authorize]
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<Printers>>> Get()
        {
            return await base.Get();
        }
        [Authorize]
        [HttpGet("{id}")]
        public override async Task<ActionResult<Printers>> Get(int id)
        {
            return await base.Get(id);
        }
        [Authorize]
        [HttpPut]
        public override async Task<ActionResult> Update(Printers entity)
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
