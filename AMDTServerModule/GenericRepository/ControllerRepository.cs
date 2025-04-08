using AMDTServerModule.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMDTServerModule.GenericRepository
{
    public class ControllerRepository<TEntity, TContext> : ControllerBase, IControllerRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        private readonly TContext _context;

        public ControllerRepository(TContext context)
        {
            _context = context;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            try
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {
                CustomLogger.SendErrorToTextForDAL(ex);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(int id)
        {
            try
            {
                var content = await _context.Set<TEntity>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (content == null)
                {
                    return NotFound();
                }
                return content;
            }
            catch (Exception ex)
            {
                CustomLogger.SendErrorToTextForDAL(ex);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult> Create(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Get", new { id = entity.ID }, entity);
            }
            catch (Exception ex)
            {
                CustomLogger.SendErrorToTextForDAL(ex);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        public virtual async Task<ActionResult> Update(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                CustomLogger.SendErrorToTextForDAL(ex);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        public virtual async Task<ActionResult> Delete(int id)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FindAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                CustomLogger.SendErrorToTextForDAL(ex);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}