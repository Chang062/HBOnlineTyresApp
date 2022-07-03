using System.Linq.Expressions;

namespace HBOnlineTyresApp.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(params Expression<Func<T, object>>[] IncludeProperties);
        Task<T> GetIdAync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);

    }
}
