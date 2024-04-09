using AutoMapper;
using Core.Crud.Data;
using Core.Crud.Exceptions;
using Core.Crud.Extensions;
using Core.Crud.Filters;
using Core.Crud.Filters.Handlers;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                query = ApplyFilter(query, command);
                query = ApplySorting(query, command);
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
            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                var addedEntity = await _dbSet.AddAsync(entity);
                await Save();
                return _mapper.Map<TEntity, TEntityDTO>(addedEntity.Entity);
            }
            catch (Exception ex)
            {

                throw;
            }
            
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
            var query = _dbSet.AsNoTracking().AsQueryable();
            var excludedTypes = new[] { typeof(Decimal), typeof(DateTime), typeof(DateTimeOffset), typeof(String) };
            var propertiesKey = typeof(TEntity).GetProperties()
                .Where(p => !p.PropertyType.IsPrimitive
             && !p.PropertyType.IsGenericType
             && !excludedTypes.Contains(p.PropertyType)).Select(p=> p.Name);

            foreach (var path in propertiesKey)
            {
                query = query.Include(path);
            }
            return query;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, ISorter command)
        {
            if (command.SortField.EndsWith("_original")) command.SortField = command.SortField.Remove(command.SortField.Length - 9);
            var sortPropertyInfo = typeof(TEntity).GetProperty(command.SortField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (sortPropertyInfo == null)
            {
                return query;
            }

            var item = Expression.Parameter(typeof(TEntity), "item");
            var property = Expression.Property(item, sortPropertyInfo);
            var lambda = Expression.Lambda(property, item);

            var method = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(a => a.Name == $"OrderBy{(command.IsAsc ? string.Empty : "Descending")}")
                .Single(a => a.GetParameters().Length == 2);
            method = method.MakeGenericMethod(typeof(TEntity), property.Type);
            return (IQueryable<TEntity>)method.Invoke(method, new object[] { query, lambda });
        }

        public static IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, Filter filter)
        {
            if (filter.Filters == null || !filter.Filters.Any()) return query;

            foreach (var groupedFilters in filter.Filters.GroupBy(a => a.PropertyName.ToLowerInvariant()))
            {
                Expression<Func<TEntity, bool>> orExpression = null;
                foreach (var filterInfo in groupedFilters)
                {
                    CheckFilter(filterInfo);
                    var expr = FilterHandlersProvider.ProvideFilter<TEntity>(filterInfo);
                    if (expr != null)
                    {
                        orExpression = orExpression == null ? expr : orExpression.Or(expr);
                    }
                }

                if (orExpression != null)
                {
                    query = query.Where(orExpression);
                }
            }

            return query;
        }

        private static void CheckFilter(FilterInfoBase filter)
        {
            var propertyName = filter.PropertyName.Split('.');
            PropertyInfo entityPropertyInfo = null;
            var type = typeof(TEntity);

            foreach (var part in propertyName)
            {
                entityPropertyInfo = type.GetProperty(part, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (entityPropertyInfo != null)
                {
                    type = entityPropertyInfo.PropertyType;
                }
                else
                {
                    break;
                }
            }

            if (entityPropertyInfo == null)
            {
                throw new BusinessException($"Can not find '{filter.PropertyName}' property " + $"in '{typeof(TEntity).FullName}' entity type");
            }
        }
    }
}
