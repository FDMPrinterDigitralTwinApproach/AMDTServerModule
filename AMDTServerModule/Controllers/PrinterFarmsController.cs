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
         
            return await base.Create(entity);
        }
        [Authorize]
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<PrinterFarms>>> Get()
        {
            return await base.Get();
        }
        [Authorize]
        [HttpGet("{id}")]
        public override async Task<ActionResult<PrinterFarms>> Get(int id)
        {
            return await base.Get(id);
        }
        [Authorize]
        [HttpPut]
        public override async Task<ActionResult> Update(PrinterFarms entity)
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
