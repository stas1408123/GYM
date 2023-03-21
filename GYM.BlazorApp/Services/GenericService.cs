using GYM.BlazorApp.Interfaces;

namespace GYM.BlazorApp.Services
{
    public class GenericService<TModel> : IGenericService<TModel>
        where TModel : class
    {
        private readonly string _defaultRoute;
        private readonly HttpClient _client;
        public GenericService(IHttpClientFactory clientFactory, string route)
        {
            _client = clientFactory.CreateClient();
            _defaultRoute = route;
        }
        public async Task<IEnumerable<TModel>> GetAll()
        {
            var response = await _client.GetAsync(_defaultRoute);
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<TModel>>();

            return result!;
        }

        public async Task<TModel?> Get(string route)
        {
            var response = await _client.GetAsync(_defaultRoute + route);
            return await response.Content.ReadFromJsonAsync<TModel>();
        }

        public async Task Create(TModel item)
        {
            JsonContent content = JsonContent.Create(item);
            await _client.PostAsync(_defaultRoute, content);
        }

        public async Task Update(TModel item)
        {
            JsonContent content = JsonContent.Create(item);
            await _client.PutAsync(_defaultRoute, content);
        }

        public async Task Delete(string route)
        {
            await _client.DeleteAsync(_defaultRoute + route);
        }
    }
}
