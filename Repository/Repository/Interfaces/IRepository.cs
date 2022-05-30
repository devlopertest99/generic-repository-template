using Microsoft.EntityFrameworkCore.Query;
using Repository.Results;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    /// <summary>
    /// Generic repository interface for <typeparamref name="TEntity"/> entity.
    /// This includes all CRUD operations in the database.
    /// </summary>
    /// <typeparam name="TEntity">Name of DBSet or Table in the database.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get total data from <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="filter">A list of fields you want to filter from the <typeparamref name="TEntity"/>.</param>
        /// <param name="allowNoTracking">Returns a query where the change tracker will not track any of the entities that are returned.
        /// <br>If set to true, all modified changes of the entity will not be detected by the change tracker.</br></param>
        /// <returns>An entity result from <typeparamref name="TEntity"/> after the process.</returns>
        Task<RepositoryResult<TEntity>> Count(Expression<Func<TEntity, bool>>? filter = null, bool allowNoTracking = false);

        /// <summary>
        /// Create a new data for <typeparamref name="TEntity"/> into the database.
        /// </summary>
        /// <param name="entity"><typeparamref name="TEntity"/> object to be inserted or created.</param>
        /// <returns>An entity result from <typeparamref name="TEntity"/> after the process.</returns>
        Task<RepositoryResult<TEntity>> Create(TEntity entity);

        /// <summary>
        /// Get a single data from the <typeparamref name="TEntity"/> list.
        /// </summary>
        /// <param name="filter">A list of fields you want to filter from the <typeparamref name="TEntity"/>.</param>
        /// <returns>A single object from the <typeparamref name="TEntity"/> list.</returns>
        Task<RepositoryResult<TEntity>> Find(Expression<Func<TEntity, bool>>? filter = null);

        /// <summary>
        /// Get the list of <typeparamref name="TEntity"/> from the database.
        /// </summary>
        /// <param name="filter">A list of fields you want to filter from the <typeparamref name="TEntity"/>.</param>
        /// <param name="orderBy">Data direction of the list.</param>
        /// <param name="includeRelatedEntity">Related entity data of the <typeparamref name="TEntity"/>.</param>
        /// <param name="allowNoTracking">Returns a query where the change tracker will not track any of the entities that are returned.
        /// <br>If set to true, all modified changes of the entity will not be detected by the change tracker.</br></param>
        /// <param name="page">A number you want to start to look in a paginated list.</param>
        /// <param name="pageSize">Total size per page you want to display.</param>
        /// <returns>An entity result from <typeparamref name="TEntity"/> after the process.</returns>
        Task<RepositoryResult<TEntity>> List(Expression<Func<TEntity, bool>>? filter = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includeRelatedEntity = null,
                                        bool allowNoTracking = false, int? page = null, int? pageSize = null);
        /// <summary>
        /// Get the list of <typeparamref name="TEntity"/> from the database.
        /// </summary>
        /// <param name="filter">A list of fields you want to filter from the <typeparamref name="TEntity"/>.</param>
        /// <param name="orderBy">Data direction of the list.</param>
        /// <param name="allowNoTracking">Returns a query where the change tracker will not track any of the entities that are returned.
        /// <br>If set to true, all modified changes of the entity will not be detected by the change tracker.</br></param>
        /// <param name="page">A number you want to start to look in a paginated list.</param>
        /// <param name="pageSize">Total size per page you want to display.</param>
        /// <returns>An entity result from <typeparamref name="TEntity"/> after the process.</returns>
        Task<RepositoryResult<TEntity>> List(Expression<Func<TEntity, bool>>? filter = null,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                       bool allowNoTracking = false, int? page = null, int? pageSize = null);
        /// <summary>
        /// Get the list of <typeparamref name="TEntity"/> from the database.
        /// </summary>
        /// <param name="filter">A list of fields you want to filter from the <typeparamref name="TEntity"/>.</param>
        /// <param name="allowNoTracking">Returns a query where the change tracker will not track any of the entities that are returned.
        /// <br>If set to true, all modified changes of the entity will not be detected by the change tracker.</br></param>
        /// <param name="page">A number you want to start to look in a paginated list.</param>
        /// <param name="pageSize">Total size per page you want to display.</param>
        /// <returns>An entity result from <typeparamref name="TEntity"/> after the process.</returns>
        Task<RepositoryResult<TEntity>> List(Expression<Func<TEntity, bool>>? filter = null,
                                       bool allowNoTracking = false, int? page = null, int? pageSize = null);
        /// <summary>
        /// Get the list of <typeparamref name="TEntity"/> from the database.
        /// </summary>
        /// <returns>An entity result from <typeparamref name="TEntity"/> after the process.</returns>
        Task<RepositoryResult<TEntity>> List();

        /// <summary>
        /// Update the information of the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">Updated copy of <typeparamref name="TEntity"/> to be changed in the database.</param>
        /// <returns>An entity result from <typeparamref name="TEntity"/> after the process.</returns>
        Task<RepositoryResult<TEntity>> Update(TEntity entity);


        Task<RepositoryResult<TEntity>> SaveChanges();
    }
}
