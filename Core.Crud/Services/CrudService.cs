using AutoMapper;
using Core.Crud.Data;
using Core.Crud.Filters;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Core.Crud.Services
{
    public class CrudService<TEntity, TEntityDTO, TKey> : ICrudService<TEntity, TEntityDTO, TKey>
         where TEntity : class, IEntity<TKey>
         where TEntityDTO : class
         where TKey : IEquatable<TKey>
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public readonly IMapper _mapper;
        public CrudService(DbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<TEntity>();
        }


        public async Task<PageResult<TEntityDTO>> GetPagedData(FilterCommand command = null, CancellationToken cancellationToken = default)
        {
            var query = GetAll();
            int total;

            if (command != null)
            {
                //query = ApplyFilter(query, command);
                //query = ApplySorting(query, command);
                total = await query.CountAsync(cancellationToken);

                if (command.IsPaginator)
                {
                    var maxSkip = (total / command.Take) * command.Take;
                    if (total % command.Take == 0 && total != 0) maxSkip -= command.Take;

                    query = query.Skip(Math.Min(command.Skip, maxSkip)).Take(command.Take);
                }
            }
            else
            {
                total = await query.CountAsync(cancellationToken);
            }

            var x = query.ToList();
            return new PageResult<TEntityDTO>
            {
                Items = _mapper.Map<IEnumerable<TEntityDTO>>(query.ToList()),
                Total = total
            };
        }

        public async Task<bool> Delete(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await Get(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await Save();
                return true;
            }
            return false;
        }

        public async Task<TEntityDTO> Insert(TEntityDTO dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var addedEntity = await _dbSet.AddAsync(entity);
            await Save();
            return _mapper.Map<TEntity, TEntityDTO>(addedEntity.Entity);
        }

        public async Task<TEntityDTO> Update(TEntity entity)
        {
            var updateTEntity = _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await Save();
            return _mapper.Map<TEntity, TEntityDTO>(updateTEntity.Entity);
        }

        public async Task<TEntity> Get(TKey id, List<string> includes = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (includes != null)
            {
                includes.ForEach(include =>
                query = query.Include(include));
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id.Equals(id));


        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
