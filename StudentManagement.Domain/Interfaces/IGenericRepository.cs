using System.Linq.Expressions;

namespace StudentManagement.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int? pageIndex = null, int? pageSize = null, CancellationToken cancellationToken = default);

        Task<T> GetByIDAsync(object id, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

        Task InsertAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(object id);

        Task DeleteAsync(T entityToDelete);

        Task UpdateAsync(T entityToUpdate);

        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
    }
}
