using System.Linq.Expressions;
namespace GYM.BLL.Abstractions;

public interface IGymService<TModel> where TModel : class
{
    Task<IEnumerable<TModel>> GetAll();
    Task<TModel?> Get(int id);
    Task<IEnumerable<TModel>> Get(Expression<Func<TModel, bool>> predicate);
    Task Create(TModel item);
    Task Update(TModel item);
    Task<bool> Delete(int id);
}