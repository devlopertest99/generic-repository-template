using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Repository.Exceptions;
using Repository.Interfaces;
using Repository.Results;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new RepositoryException("Missing dbContext", new ArgumentNullException(nameof(dbContext)));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<RepositoryResult<TEntity>> Count(Expression<Func<TEntity, bool>> filter = null, bool allowNoTracking = false)
        {
            RepositoryResult<TEntity> result;

            IQueryable<TEntity> query = _dbSet;

            try
            {
                if (allowNoTracking) query = query.AsNoTracking();

                var data = filter != null ? await query.CountAsync(filter) : await query.CountAsync();
                result = new RepositoryResult<TEntity> { Result = data };
            }
            catch (Exception ex)
            {
                await Task.FromResult(result = new RepositoryResult<TEntity>
                {
                    RepositoryException = new RepositoryException(ex.Message, ex),
                    Result = null
                });
            }

            return result;
        }

        public async Task<RepositoryResult<TEntity>> Create(TEntity entity)
        {
            RepositoryResult<TEntity> result;

            try
            {
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                result = new RepositoryResult<TEntity> { Result = entity };
            }
            catch (Exception ex)
            {
                await Task.FromResult(result = new RepositoryResult<TEntity>
                {
                    RepositoryException = new RepositoryException(ex.Message, ex),
                    Result = null
                });
            }

            return result;
        }

        public async Task<RepositoryResult<TEntity>> Find(Expression<Func<TEntity, bool>> filter = null)
        {
            RepositoryResult<TEntity> result;

            IQueryable<TEntity> query = _dbSet;

            try
            {
                if (filter != null) query = query.Where(filter);

                var data = await query.FirstOrDefaultAsync();

                result = new RepositoryResult<TEntity> { Result = data };
            }
            catch (Exception ex)
            {
                await Task.FromResult(result = new RepositoryResult<TEntity>
                {
                    RepositoryException = new RepositoryException(ex.Message, ex),
                    Result = null
                });
            }

            return result;
        }

        public async Task<RepositoryResult<TEntity>> List(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeRelatedEntity = null,
            bool allowNoTracking = false, int? page = null, int? pageSize = null)
        {
            RepositoryResult<TEntity> result;

            IQueryable<TEntity> query = _dbSet;

            try
            {
                if (allowNoTracking) query = query.AsNoTracking();

                if (filter != null) query = query.Where(filter);

                if (includeRelatedEntity != null) query = includeRelatedEntity(query);

                if (orderBy != null) orderBy.Invoke(query);

                if (page.HasValue && pageSize.HasValue)
                {
                    page = page.Value == 0 ? 0 : (page.Value - 1) * pageSize.Value;
                    query = query.Skip(page.Value).Take(pageSize.Value);
                }

                var data = await query.ToListAsync();

                result = new RepositoryResult<TEntity> { Result = data };
            }
            catch (Exception ex)
            {
                await Task.FromResult(result = new RepositoryResult<TEntity>
                {
                    RepositoryException = new RepositoryException(ex.Message, ex),
                    Result = null
                });
            }

            return result;
        }

        public async Task<RepositoryResult<TEntity>> List(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool allowNoTracking = false, int? page = null, int? pageSize = null)
        {
            RepositoryResult<TEntity> result;

            IQueryable<TEntity> query = _dbSet;

            try
            {
                if (allowNoTracking) query = query.AsNoTracking();

                if (filter != null) query = query.Where(filter);

                if (orderBy != null) orderBy.Invoke(query);

                if (page.HasValue && pageSize.HasValue)
                {
                    page = page.Value == 0 ? 0 : (page.Value - 1) * pageSize.Value;
                    query = query.Skip(page.Value).Take(pageSize.Value);
                }

                var data = await query.ToListAsync();
                result = new RepositoryResult<TEntity> { Result = data };
            }
            catch (Exception ex)
            {
                await Task.FromResult(result = new RepositoryResult<TEntity>
                {
                    RepositoryException = new RepositoryException(ex.Message, ex),
                    Result = null
                });
            }

            return result;
        }

        public async Task<RepositoryResult<TEntity>> List(Expression<Func<TEntity, bool>> filter = null,
            bool allowNoTracking = false, int? page = null, int? pageSize = null)
        {
            RepositoryResult<TEntity> result;

            IQueryable<TEntity> query = _dbSet;

            try
            {
                if (allowNoTracking) query = query.AsNoTracking();

                if (filter != null) query = query.Where(filter);

                if (page.HasValue && pageSize.HasValue)
                {
                    page = page.Value == 0 ? 0 : (page.Value - 1) * pageSize.Value;
                    query = query.Skip(page.Value).Take(pageSize.Value);
                }

                var data = await query.ToListAsync();
                result = new RepositoryResult<TEntity> { Result = data };
            }
            catch (Exception ex)
            {
                await Task.FromResult(result = new RepositoryResult<TEntity>
                {
                    RepositoryException = new RepositoryException(ex.Message, ex),
                    Result = null
                });
            }

            return result;
        }

        public async Task<RepositoryResult<TEntity>> List()
        {
            RepositoryResult<TEntity> result;

            IQueryable<TEntity> query = _dbSet;

            try
            {
                var data = await query.ToListAsync();
                result = new RepositoryResult<TEntity> { Result = data };
            }
            catch (Exception ex)
            {
                await Task.FromResult(result = new RepositoryResult<TEntity>
                {
                    RepositoryException = new RepositoryException(ex.Message, ex),
                    Result = null
                });
            }

            return result;
        }

        public async Task<RepositoryResult<TEntity>> SaveChanges()
        {
            var result = await _dbContext.SaveChangesAsync();
            return new RepositoryResult<TEntity> { Result = result };
        }

        public async Task<RepositoryResult<TEntity>> Update(TEntity entity)
        {
            RepositoryResult<TEntity> result;

            try
            {
                _dbSet.Update(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                result = new RepositoryResult<TEntity> { Result = entity };
            }
            catch (Exception ex)
            {
                await Task.FromResult(result = new RepositoryResult<TEntity>
                {
                    RepositoryException = new RepositoryException(ex.Message, ex),
                    Result = null
                });
            }

            return result;
        }

    }
}
