using System.Linq.Expressions;

namespace GYM.DAL.Repositories.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(int id);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        Task Create(T item);
        Task Update(T item);
        Task<bool> Delete(int id);
    }
}
