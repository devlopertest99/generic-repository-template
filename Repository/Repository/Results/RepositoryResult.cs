using Repository.Exceptions;

namespace Repository.Results
{
    public class RepositoryResult<TEntity> where TEntity : class
    {
        /// <summary>
        /// A result from <typeparamref name="TEntity"/>.
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// An exception error from the <typeparamref name="TEntity"/> when doing some process.
        /// </summary>
        public RepositoryException? RepositoryException { get; set; }

    }
}
