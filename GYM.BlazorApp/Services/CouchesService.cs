using GYM.BlazorApp.Data.ViewModels;

namespace GYM.BlazorApp.Services
{
    public class CouchesService : GenericService<CouchViewModel>
    {
        private const string RouteForCouches = "https://localhost:7163/api/Couches";
        public CouchesService(IHttpClientFactory clientFactory) : base(clientFactory, RouteForCouches)
        {

        }
    }
}
