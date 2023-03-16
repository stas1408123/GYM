namespace GYM.BlazorApp.Interfaces
{
    public interface IGenericService<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel?> Get(string route);
        Task Create(TModel item);
        Task Update(TModel item);
        Task Delete(int id);
    }
}
