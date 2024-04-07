using Core.Crud.Data;
using Core.Crud.Filters;

namespace Core.Crud.Services
{
    public interface ICrudService<TEntity, TEntityDTO, TKey>
         where TEntity : class, IEntity<TKey>
         where TEntityDTO : class
         where TKey : IEquatable<TKey>
    {
        Task<bool> Delete(TKey id, CancellationToken cancellationToken = default);
        Task<TEntity> Get(TKey id, List<string> includes = null);
        IQueryable<TEntity> GetAll();
        Task<PageResult<TEntityDTO>> GetPagedData(FilterCommand command = null, CancellationToken cancellationToken = default);
        Task<TEntityDTO> Insert(TEntityDTO dto);
        Task<TEntityDTO> Update(TEntity entity);
    }


}
