using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace AMDTServerModule.GenericRepository
{
    public interface IControllerRepository<TEntity> where TEntity : IEntity
    {
        public Task<ActionResult<IEnumerable<TEntity>>> Get();
        public Task<ActionResult<TEntity>> Get(int id);
        public Task<ActionResult> Create(TEntity entity);
        public Task<ActionResult> Update(TEntity entity);
        public Task<ActionResult> Delete(int id);

    }
}
